/*
 * By David Barrett, Microsoft Ltd. 2012 - 2014. Use at your own risk.  No warranties are given.
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SOAPe.Auth;

namespace SOAPe
{
    class ClassSOAP
    {
        private string _targetURL="";
        private CredentialHandler _credentialHandler = null;
        private ClassLogger _logger = null;
        private string _authHeader = "";
        private bool _bypassWebProxy = false;
        private WebHeaderCollection _lastResponseHeaders = null;
        private CookieCollection _responseCookies = null;
        private string _requestName = null;
        private List<string[]> _httpHeaders = new List<string[]>();
        private SecurityProtocolType _securityProtocol = ServicePointManager.SecurityProtocol;

        public ClassSOAP(string TargetURL, ClassLogger Logger, CredentialHandler credentialHandler)
        {
            _targetURL = TargetURL;
            _logger = Logger;
            _credentialHandler = credentialHandler;
        }

        public SecurityProtocolType SecurityProtocol
        {
            get { return _securityProtocol;  }
            set { _securityProtocol = value; }
        }

        public string TargetURL
        {
            get { return _targetURL; }
            set { _targetURL = value; }
        }

        public bool BypassWebProxy
        {
            get { return _bypassWebProxy; }
            set { _bypassWebProxy = value; }
        }

        public WebHeaderCollection LastResponseHeaders
        {
            get
            {
                return _lastResponseHeaders;
            }
        }

        public CookieCollection ResponseCookies
        {
            get
            {
                return _responseCookies;
            }
        }

        public List<string[]> HTTPHeaders
        {
            set { _httpHeaders = value; }
        }

        public string AuthorizationHeader
        {
            set { _authHeader = value; }
        }

        private void Log(string Details, string Description = "", string ClientRequestId="")
        {
            try
            {
                if (_logger == null) return;
                _logger.Log(Details, Description, ClientRequestId);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Failed to log information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LogHeaders(WebHeaderCollection Headers, string Description, string Url = "", HttpWebResponse Response = null)
        {
            if (_logger == null) return;
            _logger.LogWebHeaders(Headers, Description, Url, Response);
        }

        private void LogCookies(CookieCollection Cookies, string Description)
        {
            if (_logger == null) return;
            _logger.LogCookies(Cookies, Description);
        }

        private void LogSSLSettings()
        {
            StringBuilder sSSL = new StringBuilder("SSL/TLS protocols available: ");

            // Build list of protocols that are enabled
            List<string> protocols = new List<string>();
            if ((ServicePointManager.SecurityProtocol & SecurityProtocolType.Ssl3) == SecurityProtocolType.Ssl3)
                protocols.Add("SSL 3.0");
            if ((ServicePointManager.SecurityProtocol & SecurityProtocolType.Tls) == SecurityProtocolType.Tls)
                protocols.Add("TLS 1.0");
            if ((ServicePointManager.SecurityProtocol & SecurityProtocolType.Tls11) == SecurityProtocolType.Tls11)
                protocols.Add("TLS 1.1");
            if ((ServicePointManager.SecurityProtocol & SecurityProtocolType.Tls12) == SecurityProtocolType.Tls12)
                protocols.Add("TLS 1.2");

            sSSL.AppendLine(String.Join("; ", protocols));

            _logger.Log(sSSL.ToString(), "Connection Security");
        }

        public string SendRequest(string sRequest, out string sError, CookieCollection oCookies = null)
        {
            // Send the request and return the response
            _requestName = ClassLogger.ReadMethodFromRequest(sRequest);
            sError = "";

            if (String.IsNullOrEmpty(_targetURL))
            {
                sError= "No target server specified";
                return "";
            }

            string sResponse = "";
            SecurityProtocolType currentSecurityProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = _securityProtocol;
            HttpWebRequest oWebRequest = null;
            try
            {
                oWebRequest = (HttpWebRequest)WebRequest.Create(_targetURL);
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                return "";
            }
            oWebRequest.UserAgent = String.Format("{1}/{0}", Application.ProductVersion, Application.ProductName);
            if (_bypassWebProxy)
                oWebRequest.Proxy = null;

            // Set authentication
            _credentialHandler.ApplyCredentialsToHttpWebRequest(oWebRequest);

            oWebRequest.ContentType = "text/xml;charset=\"utf-8\"";
            oWebRequest.Accept = "text/xml";
            if (_httpHeaders.Count > 0)
            {
                foreach (string[] header in _httpHeaders)
                {
                    try
                    {
                        switch (header[0].ToLower())
                        {
                            case "user-agent":
                                oWebRequest.UserAgent = header[1];
                                break;

                            case "content-type":
                                oWebRequest.ContentType = header[1];
                                break;

                            case "accept":
                                oWebRequest.Accept = header[1];
                                break;

                            default:
                                oWebRequest.Headers[header[0]] = header[1];
                                break;
                        }
                    }
                    catch { }
                }
            }

            // We add a client-request-id header so that we can easily match request/response
            string clientRequestId = oWebRequest.Headers.Get("client-request-id");
            if (String.IsNullOrEmpty(clientRequestId))
            {
                clientRequestId = Guid.NewGuid().ToString();
                oWebRequest.Headers["client-request-id"] = clientRequestId;
            }
            StringBuilder sTimings = new StringBuilder();
            sTimings.AppendLine("Latency (latency shown in milliseconds, times are in ticks)").AppendLine();
            sTimings.AppendLine($"client-request-id: {clientRequestId}").AppendLine();
            
            oWebRequest.Headers["return-client-request-id"] = "true";

            oWebRequest.Method = "POST";
            XmlDocument oSOAPRequest = new XmlDocument();
            if (!String.IsNullOrEmpty(sRequest))
            {
                try
                {
                    oSOAPRequest.LoadXml(sRequest);
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                    sResponse = "Request was invalid XML (not sent): " + ex.Message + "\n\r\n\r" + sRequest;
                    Log(sResponse, "Response");
                    return "";
                }
            }

            oWebRequest.CookieContainer = new CookieContainer();
            if (!(oCookies == null))
            {
                // Add cookies to the request
                foreach (Cookie oCookie in oCookies)
                {
                    try
                    {
                        oWebRequest.CookieContainer.Add(oCookie);
                    }
                    catch { }
                }
                LogCookies(oCookies, "Request Cookies");
            }

            LogSSLSettings();

            Stream stream = null;
            try
            {
                stream = oWebRequest.GetRequestStream();
            }
            catch (Exception ex)
            {
                // Failed to send request
                sError = ex.Message;
                sResponse = "Error occurred: " + ex.Message + "\n\r\n\r";
                Log(sResponse, "Response");
                return "";
            }
            DateTime requestSendStartTime = DateTime.Now;
            if (!string.IsNullOrEmpty(sRequest))
                oSOAPRequest.Save(stream);
            stream.Close();
            DateTime requestSendEndTime = DateTime.Now;
            LogHeaders(oWebRequest.Headers, "Request Headers", _targetURL);
            Log(oSOAPRequest.OuterXml, "Request", clientRequestId);

            oWebRequest.Expect = "";

            DateTime responseReceiveEndTime = DateTime.MinValue;
            DateTime responseReceiveStartTime = DateTime.Now;
            IAsyncResult asyncResult = oWebRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            WebResponse oWebResponse = null;
            try
            {
                oWebResponse = oWebRequest.EndGetResponse(asyncResult);
                responseReceiveEndTime = DateTime.Now;
                _lastResponseHeaders = oWebResponse.Headers;
                LogHeaders(oWebResponse.Headers, "Response Headers","",(oWebResponse as HttpWebResponse));
                _responseCookies = (oWebResponse as HttpWebResponse).Cookies;
                LogCookies(_responseCookies, "Response Cookies");
            }
            catch (Exception ex)
            {
                responseReceiveEndTime = DateTime.Now;
                if (ex is WebException)
                {
                    WebException wex = ex as WebException;
                    sError = wex.Message;
                    if ( !(wex.Response == null) )
                    {
                        using (StreamReader oReader = new StreamReader(wex.Response.GetResponseStream()))
                        {
                            sResponse = oReader.ReadToEnd();
                        }
                        _lastResponseHeaders = wex.Response.Headers;
                        LogHeaders(wex.Response.Headers, "Response Headers", "", (wex.Response as HttpWebResponse));
                    }
                }
                else
                    sError = ex.Message;
            }
            try
            {
                using (StreamReader oReader = new StreamReader(oWebResponse.GetResponseStream()))
                {
                    sResponse += oReader.ReadToEnd();
                }
            }
            catch { }

            try
            {
                oWebResponse.Close();
            }
            catch { }
            Log(sResponse, "Response", clientRequestId);

            sTimings.AppendLine($"Request start: {(long)(requestSendStartTime.Ticks/10000)}");
            sTimings.AppendLine($"Request complete: {(long)(requestSendEndTime.Ticks/10000)}");
            sTimings.AppendLine($"Request latency: {(long)((requestSendEndTime.Ticks- requestSendStartTime.Ticks) / 10000)}").AppendLine();
            sTimings.AppendLine($"Response start: {(long)(responseReceiveStartTime.Ticks / 10000)}");
            sTimings.AppendLine($"Response complete: {(long)(responseReceiveEndTime.Ticks / 10000)}");
            sTimings.AppendLine($"Response latency: {(long)((responseReceiveEndTime.Ticks - responseReceiveStartTime.Ticks) / 10000)}").AppendLine();
            sTimings.AppendLine($"Total time taken (includes processing time): {(long)((responseReceiveEndTime.Ticks - requestSendStartTime.Ticks) / 10000)}");
            Log(sTimings.ToString(), "Latency Report");

            return sResponse;
        }


    
    }
}
