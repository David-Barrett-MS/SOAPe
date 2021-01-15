/*
 * By David Barrett, Microsoft Ltd. 2020. Use at your own risk.  No warranties are given.
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
using System.Net;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SOAPe.Auth
{
    public enum AuthType
    {
        None,
        Default,
        Basic,
        Certificate,
        OAuth
    }

    public class CredentialHandler
    {
        String _userName = String.Empty;
        String _domain = String.Empty;
        String _password = String.Empty;
        String _token = String.Empty;
        X509Certificate2 _certificate = null;
        AuthType _authType = AuthType.Basic;

        public CredentialHandler(AuthType authType)
        {
            _authType = authType;
        }

        public string Username
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Domain
        {
            get { return _domain; }
            set { _domain = value; }
        }

        public string Password
        {
            set { _password = value; }
        }

        public string OAuthToken
        {
            get { return _token; }
            set { _token = value; }
        }

        public X509Certificate2 Certificate
        {
            get { return _certificate; }
            set { _certificate = value; }
        }

        private bool HaveValidCredentials()
        {
            switch (_authType)
            {
                case AuthType.Default:
                    return true;

                case AuthType.Basic:
                    if (!String.IsNullOrEmpty(_userName)) return true;
                    return false;

                case AuthType.Certificate:
                    return (_certificate != null);

                case AuthType.OAuth:
                    if (!String.IsNullOrEmpty(_token)) return true;
                    return false;

                default: return false;
            }
        }

        public void LogCredentials(ClassLogger Logger)
        {
            StringBuilder sCredentialInfo = new StringBuilder();
            switch (_authType)
            {
                case AuthType.Default:
                    sCredentialInfo.AppendLine("Using default credentials");
                    sCredentialInfo.Append("Username: ");
                    sCredentialInfo.AppendLine(Environment.UserName);
                    sCredentialInfo.Append("Domain: ");
                    sCredentialInfo.AppendLine(Environment.UserDomainName);
                    break;

                case AuthType.Basic:
                    sCredentialInfo.AppendLine("Using specific credentials");
                    sCredentialInfo.Append("Username: ");
                    sCredentialInfo.AppendLine(_userName);
                    sCredentialInfo.Append("Domain: ");
                    sCredentialInfo.AppendLine(_domain);
                    break;

                case AuthType.Certificate:
                    sCredentialInfo.AppendLine("Using certificate");
                    if (_certificate != null)
                    {
                        sCredentialInfo.Append("Subject: ");
                        sCredentialInfo.AppendLine(_certificate.Subject);
                    }
                    else
                        sCredentialInfo.AppendLine("NO CERTIFICATE SPECIFIED");
                    break;

                case AuthType.OAuth:
                    sCredentialInfo.AppendLine("Using OAuth");
                    sCredentialInfo.AppendLine($"Current access token: {_token}");
                    break;

            }

            Logger.Log(sCredentialInfo.ToString(), "Request Credentials");
        }

        public bool ApplyCredentialsToHttpWebRequest(HttpWebRequest Request)
        {
            if (!HaveValidCredentials())
                return false;

            switch (_authType)
            {
                case AuthType.Default:
                    Request.UseDefaultCredentials = true;
                    return true;

                case AuthType.Basic:
                    Request.Credentials = new NetworkCredential(_userName, _password);
                    return true;

                case AuthType.Certificate:
                    Request.ClientCertificates = new X509CertificateCollection();
                    Request.ClientCertificates.Add(_certificate);
                    return true;

                case AuthType.OAuth:
                    Request.Headers["Authorization"] = $"Bearer {_token}";
                    return true;
            }

            return false;
        }
    }
}
