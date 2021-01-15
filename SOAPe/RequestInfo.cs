/*
 * By David Barrett, Dark Bytes Ltd. 2016-2021. Use at your own risk.  No warranties are given.
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

namespace SOAPe
{
    public class RequestInfo
    {
        public Dictionary<DateTime, string> Traffic;
        public string Name { get; set; }
        public bool ErrorPresent
        {
            get
            {
                // Check if we have response headers...
                foreach (string sData in Traffic.Values)
                {
                    if (sData.StartsWith("EwsResponseHttpHeaders") || sData.StartsWith("Response Headers"))
                    {
                        // Check for server error
                        string sHTTPResponse = sData.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[1];
                        if (!(sHTTPResponse.Contains("200 OK")))
                            return true;
                    }
                    else if (sData.Contains("EwsResponse"))
                    {
                        if (sData.Contains("ResponseClass=\"Error\""))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public RequestInfo(string name)
        {
            Name = name;
            Traffic = new Dictionary<DateTime, string>();
        }
    }

    public class RequestEventArgs : EventArgs
    {
        private RequestInfo _info;

        public RequestEventArgs(RequestInfo Info)
        {
            _info = Info;
        }

        public RequestInfo Info
        {
            get { return _info; }
        }
    }
}
