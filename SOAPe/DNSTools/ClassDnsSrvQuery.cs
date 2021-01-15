/*
 * By David Barrett, Microsoft Ltd. 2013. Use at your own risk.  No warranties are given.
 * 
 * DISCLAIMER:
 * THIS CODE IS SAMPLE CODE. THESE SAMPLES ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND.
 * MICROSOFT FURTHER DISCLAIMS ALL IMPLIED WARRANTIES INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OF MERCHANTABILITY OR OF FITNESS FOR
 * A PARTICULAR PURPOSE. THE ENTIRE RISK ARISING OUT OF THE USE OR PERFORMANCE OF THE SAMPLES REMAINS WITH YOU. IN NO EVENT SHALL
 * MICROSOFT OR ITS SUPPLIERS BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS,
 * BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR OTHER PECUNIARY LOSS) ARISING OUT OF THE USE OF OR INABILITY TO USE THE
 * SAMPLES, EVEN IF MICROSOFT HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. BECAUSE SOME STATES DO NOT ALLOW THE EXCLUSION OR LIMITATION
 * OF LIABILITY FOR CONSEQUENTIAL OR INCIDENTAL DAMAGES, THE ABOVE LIMITATION MAY NOT APPLY TO YOU.
 * */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SOAPe.DNSTools
{
    public class SRVRecord
    {
        public ushort Priority;
        public ushort Weight;
        public ushort Port;
        public string HostName;
    }

    public class ClassDnsSrvQuery
    {
        static int _queryId = 0;
        static List<IPAddress> _dnsIPAddresses=new List<IPAddress>();

        public ClassDnsSrvQuery()
        {
            if (_dnsIPAddresses.Count > 0) return;

            // Get a list of DNS servers we can use
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                try
                {
                    if (networkInterface.OperationalStatus == OperationalStatus.Up)
                    {
                        IPInterfaceProperties ipProps = networkInterface.GetIPProperties();
                        if (ipProps.DnsAddresses.Count > 0)
                        {
                            // Add these DNS servers to our list
                            foreach (IPAddress dnsIP in ipProps.DnsAddresses)
                            {
                                _dnsIPAddresses.Add(dnsIP);
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private byte[] DNSQuery(string host)
        {
            // Build and return the DNS query
            MemoryStream msQuery = new MemoryStream();

            // Query Id
            msQuery.WriteByte((byte)(_queryId >> 8));
            msQuery.WriteByte((byte)_queryId++);

            // Status block
            msQuery.WriteByte(0x01);
            msQuery.WriteByte(0x00);

            // Number of queries in the request (one)
            msQuery.WriteByte(0x00);
            msQuery.WriteByte(0x01);

            // We can ignore the next six bytes in our request
            msQuery.WriteByte((byte)0); msQuery.WriteByte((byte)0);
            msQuery.WriteByte((byte)0); msQuery.WriteByte((byte)0);
            msQuery.WriteByte((byte)0); msQuery.WriteByte((byte)0);

            // Now add the domain we are looking up
            int i = 0;
            int j = 0;
            while (j < host.Length)
            {
                i = host.IndexOf(".", j);
                if (i < 0) i = host.Length;
                byte subDomainLength = (byte)(i - j);
                char[] subDomain = host.Substring(j, subDomainLength).ToCharArray();

                msQuery.WriteByte(subDomainLength);
                msQuery.Write(System.Text.Encoding.ASCII.GetBytes(subDomain, 0, subDomain.Length), 0, subDomain.Length);
                j = i + 1;
            }
            msQuery.WriteByte(0x00);

            // We want SRV records
            msQuery.WriteByte(0x00);
            msQuery.WriteByte((byte)33);

            // The DNS Class is internet
            msQuery.WriteByte(0x00);
            msQuery.WriteByte(0x01); 
            return msQuery.ToArray();
        }

        private SRVRecord ReadSRVRecord(ref MemoryStream DNSResponseStream)
        {
            SRVRecord srv = new SRVRecord();
            byte[] temp = new byte[4];
            DNSResponseStream.Read(temp, 0, 4);
            DNSResponseStream.Read(temp, 0, 4);
            //DNSResponseStream.Read(temp, 0, 2);

            byte[] priority = new byte[2];
            DNSResponseStream.Read(priority, 0, 2);
            srv.Priority = (ushort)IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(priority, 0));

            byte[] weight = new byte[2];
            DNSResponseStream.Read(weight, 0, 2);
            srv.Weight = (ushort)IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(weight, 0));

            byte[] port = new byte[2];
            DNSResponseStream.Read(port, 0, 2);
            srv.Port = (ushort)IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(port, 0));

            srv.HostName = ParseHost(ref DNSResponseStream);
            if (String.IsNullOrEmpty(srv.HostName))
                return null;
            return srv;
        }

        private string ParseHost(ref MemoryStream DNSResponseStream)
        {
            StringBuilder sb = new StringBuilder();

            uint next = (uint)DNSResponseStream.ReadByte();
            int bPointer;

            while ((next != 0x00))
            {
                // Isolate 2 most significat bits -> e.g. 11xx xxxx
                // if it's 0xc0 (11000000b} then pointer
                switch (0xc0 & next)
                {
                    // 0xc0 -> Name is a pointer.
                    case 0xc0:
                        {
                            // Isolate Offset
                            int offsetMASK = ~0xc0;

                            // Move offset into the proper position
                            int offset = (int)(offsetMASK & next) << 8;

                            // extract the pointer to the data in the stream
                            bPointer = DNSResponseStream.ReadByte() + offset;
                            // store the position so we can resume later
                            long oldPtr = DNSResponseStream.Position;
                            // Move to the specified position in the stream and 
                            // parse the name (recursive call)
                            DNSResponseStream.Position = bPointer;
                            sb.Append(ParseHost(ref DNSResponseStream));
                            // Move back to original position, and continue
                            DNSResponseStream.Position = oldPtr;
                            next = 0x00;
                            break;
                        }
                    case 0x00:
                        {
                            byte[] buffer = new byte[next];
                            DNSResponseStream.Read(buffer, 0, (int)next);
                            sb.Append(System.Text.Encoding.ASCII.GetString(buffer) + ".");
                            next = (uint)DNSResponseStream.ReadByte();
                            break;
                        }
                    default:
                        throw new InvalidOperationException("There was a problem decompressing the DNS Message.");
                }
            }
            return sb.ToString();
        }

        private List<SRVRecord> ParseResponse(byte[] DNSResponse)
        {
            // Read the raw DNS response
            short answers = (short)(DNSResponse[4] << 8 | DNSResponse[5]);
            if (answers < 1)
                return null;

            List<SRVRecord> records = new List<SRVRecord>();

            // We have some answers, so read them

            // First of all we need to get past the DNS header
            short pointer = (short)(12 + DNSResponse[12] + 1);
            while (DNSResponse[pointer] != 0)
                pointer += (short)(DNSResponse[pointer]+1);
            pointer += 6;

            MemoryStream ms = new MemoryStream(DNSResponse, 0, DNSResponse.Length);
            ms.Position = pointer;
            while (ms.Position < ms.Length)
            {
                SRVRecord record = ReadSRVRecord(ref ms);
                if (record != null)
                    records.Add(record);
            }
            ms.Close();
            return records;
        }

        public List<SRVRecord> SRVRecords(string host)
        {
            // Retrieve SRV records for the given host

            byte[] DNSRequest = DNSQuery(host);
            int attempts = 0;

            while (attempts<3)
            {
                foreach (IPAddress dnsIP in _dnsIPAddresses)
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);

                    if (dnsIP.AddressFamily == AddressFamily.InterNetwork)
                    {
                        socket.SendTo(DNSRequest, DNSRequest.Length, SocketFlags.None, new IPEndPoint(dnsIP, 53));

                        byte[] response = new byte[512];
                        try
                        {
                            socket.Receive(response);
                            if (response[0] == DNSRequest[0] && response[1] == DNSRequest[1])
                            {
                                // We have a response, so parse it and return
                                List<SRVRecord> SRVRecords = ParseResponse(response);
                                if (SRVRecords != null) return SRVRecords;
                            }
                        }
                        catch
                        {
                            attempts++;
                        }
                        finally
                        {
                            socket.Close();
                        }
                    }
                }
            }
            return null;
        }
    }
}
