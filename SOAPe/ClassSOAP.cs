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

namespace SOAPe
{
    class ClassSOAP
    {
        private string _targetURL="";
        private ICredentials _credentials = null;
        private X509Certificate2 _authCertificate = null;
        private bool _noAuth = false;
        private ClassLogger _logger = null;
        private string _authHeader = "";
        private bool _bypassWebProxy = false;
        private WebHeaderCollection _lastResponseHeaders = null;
        private CookieCollection _responseCookies = null;
        private string _requestName = null;
        private List<string[]> _httpHeaders = new List<string[]>();
        private SecurityProtocolType _securityProtocol = ServicePointManager.SecurityProtocol;

        public ClassSOAP(string TargetURL, ClassLogger Logger)
        {
            // Initialise the class
            _targetURL = TargetURL;
            _logger = Logger;
            _noAuth = true;
        }

        public ClassSOAP(string TargetURL, ICredentials Credential, ClassLogger Logger)
            : this(TargetURL, Logger)
        {
            // Initialise the class
            _credentials = Credential;
            _noAuth = false;
        }

        public ClassSOAP(string TargetURL, string Username, string Password, ClassLogger Logger)
            : this(TargetURL, Logger)
        {
            // Initialise the class for basic authentication
            string sAuthInfo = Username + ":" + Password;
            _authHeader = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(sAuthInfo));
            _noAuth = false;
        }

        public ClassSOAP(string TargetURL, X509Certificate2 AuthenticationCertificate, ClassLogger Logger)
            : this(TargetURL, Logger)
        {
            _authCertificate = AuthenticationCertificate;
            _noAuth = false;
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

        public ICredentials Credentials
        {
            get { return _credentials; }
            set { _credentials = value; }
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

        private void LogHeaders(WebHeaderCollection Headers, string Description, string Url = "", HttpWebResponse Response = null)
        {
            // Log request headers
            string sHeaders = "";
            if (Response!=null)
            {
                sHeaders += String.Format("{0} {1}{2}", (int)Response.StatusCode, Response.StatusDescription, Environment.NewLine);
            }
            if (!String.IsNullOrEmpty(Url))
            {
                sHeaders += String.Format("POST URL: {0}{1}{1}", Url, Environment.NewLine);
            }
            try
            {
                foreach (string sHeader in Headers.AllKeys)
                {
                    sHeaders += sHeader + ": " + Headers[sHeader] + Environment.NewLine;
                }
                Log(sHeaders, Description);
            }
            catch { }
        }

        private void LogCookies(CookieCollection Cookies, string Description)
        {
            // Log cookies
            try
            {
                if (Cookies.Count == 0) return;
                string sCookies = "";
                foreach (Cookie cookie in Cookies)
                {
                    sCookies += cookie.ToString() + Environment.NewLine;
                }
                Log(sCookies, Description);
            }
            catch { }
        }

        private void LogSSLSettings()
        {
            StringBuilder sSSL = new StringBuilder("SSL/TLS requested: ");

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

            sSSL.Append(String.Join("; ", protocols));

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
            LogSSLSettings();
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
            oWebRequest.UseDefaultCredentials = false;
            if (!String.IsNullOrEmpty(_authHeader))
            {
                // Add authorization header
                oWebRequest.Headers["Authorization"] = _authHeader;
            }
            else if (!_noAuth)
            {
                if (_authCertificate != null)
                {
                    oWebRequest.ClientCertificates = new X509CertificateCollection();
                    oWebRequest.ClientCertificates.Add(_authCertificate);
                }
                else
                    oWebRequest.Credentials = _credentials;
            }

            oWebRequest.ContentType = "text/xml;charset=\"utf-8\"";
            oWebRequest.Accept = "text/xml";
            if (_httpHeaders.Count > 0)
            {
                foreach (string[] header in _httpHeaders)
                {
                    try
                    {
                        if (header[0].ToLower() == "content-type")
                        {
                            oWebRequest.ContentType = header[1];
                        }
                        else if (header[0].ToLower() == "accept")
                        {
                            oWebRequest.Accept = header[1];
                        }
                        else
                            oWebRequest.Headers[header[0]] = header[1];
                    }
                    catch { }
                }
            }

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
                    sResponse = "Request was invalid XML: " + ex.Message + "\n\r\n\r" + sRequest;
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
            if (!string.IsNullOrEmpty(sRequest))
                oSOAPRequest.Save(stream);
            stream.Close();
            LogHeaders(oWebRequest.Headers, "Request Headers", _targetURL);
            Log(oSOAPRequest.OuterXml, "Request");

            oWebRequest.Expect = "";

            IAsyncResult asyncResult = oWebRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            WebResponse oWebResponse = null;
            try
            {
                oWebResponse = oWebRequest.EndGetResponse(asyncResult);
                _lastResponseHeaders = oWebResponse.Headers;
                LogHeaders(oWebResponse.Headers, "Response Headers","",(oWebResponse as HttpWebResponse));
                _responseCookies = (oWebResponse as HttpWebResponse).Cookies;
                LogCookies(_responseCookies, "Response Cookies");
            }
            catch (Exception ex)
            {
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
            Log(sResponse, "Response");
            return sResponse;
        }

        private void Log(string Details, string Description = "")
        {
            try
            {
                if (_logger == null) return;
                _logger.Log(Details, Description);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Failed to log information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    
    }
}
