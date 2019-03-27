/*
 * By David Barrett, Microsoft Ltd. 2018. Use at your own risk.  No warranties are given.
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
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace SOAPe.Auth
{
    public struct OAuthContext
    {
        public string tenantName;
        public string authUrl;
        public string clientId;
        public string secretKey;
        public X509Certificate2 cert;
        public string resource;
        public string userId;
        public string redirectUrl;
        public bool adminConsent;
        public bool appConsent;
        public bool isV2Endpoint;
        public bool isNativeApplication;
        public string UserAccessToken;
        public bool ObtainUserConsent;
    }

    public class ClassOAuthHelper
    {
        private string _authenticationUrl = String.Empty;
        private string _resourceUrl = String.Empty;
        private string _applicationId = String.Empty;
        private string _tenantId = String.Empty;
        private string _redirectUrl = String.Empty;
        private bool _isNativeApplication = false;
        private static Exception _lastError = null;
        private X509Certificate2 _authCertificate = null;
        private string _clientSecret = String.Empty;
        private AuthenticationResult _authenticationResult = null;
        private static List<string> _acquireTokenInProgress;
        private bool _obtainUserConsent = false;
        private static Auth.EncryptedTokenCache _tokenCache = null;

        private static Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext _authenticationContext = null;

        public ClassOAuthHelper()
        {
            _acquireTokenInProgress = new List<string>();
            if (_tokenCache == null)
                _tokenCache = new Auth.EncryptedTokenCache();
        }

        public Auth.EncryptedTokenCache TokenCache
        {
            get { return _tokenCache; }
        }

        public string AuthenticationUrl
        {
            get { return _authenticationUrl; }
            set { _authenticationUrl = value; }
        }

        public bool IsNativeApplication
        {
            get { return _isNativeApplication; }
            set { _isNativeApplication = value; }
        }

        public string ResourceUrl
        {
            get { return _resourceUrl; }
            set { _resourceUrl = value; }
        }

        public string ApplicationId
        {
            get { return _applicationId; }
            set { _applicationId = value; }
        }

        public string TenantId
        {
            get { return _tenantId; }
            set { _tenantId = value; }
        }

        public string RedirectUrl
        {
            get { return _redirectUrl; }
            set { _redirectUrl = value; }
        }

        public X509Certificate2 AuthCertificate
        {
            get { return _authCertificate; }
            set { _authCertificate = value; }
        }

        public string AuthClientSecret
        {
            get { return _clientSecret; }
            set { _clientSecret = value; }
        }

        public AuthenticationContext AuthenticationContext
        {
            get
            {
                if (_authenticationContext == null)
                    return new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(_authenticationUrl + "/" + _tenantId, _tokenCache);
                return _authenticationContext;
            }
        }

        public string Token
        {
            get
            {
                try
                {
                    return _authenticationResult.AccessToken;
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool TokenHasExpired
        {
            get
            {
                try
                {
                    return _authenticationResult.ExpiresOn < DateTime.Now;
                }
                catch { }
                return true;
            }
        }

        public bool ObtainUserConsent
        {
            get { return _obtainUserConsent; }
            set { _obtainUserConsent = value; }
        }

        public bool RenewToken()
        {
            if (!TokenHasExpired) return true;
            if (_authenticationResult == null) return false;

            try
            {
                AcquireDelegateToken(_authenticationResult.UserInfo.UniqueId).Wait(10000);
            }
            catch
            {
                return false;
            }
            return (_authenticationResult.ExpiresOn < DateTime.Now);
        }

        public AuthenticationResult AuthenticationResult
        {
            get { return _authenticationResult; }
        }

        public Exception LastError
        {
            get { return _lastError; }
        }

        public void SaveAuthenticationResult(string ToFile)
        {

        }

        public async Task AcquireDelegateToken(string UserId = "")
        {
            string configError = String.Empty;
            if ((_authenticationUrl == String.Empty)) configError = "Authentication Url must be set.";
            if ((_resourceUrl == String.Empty)) configError = "Resource Url must be set.";
            if ((_tenantId == String.Empty)) configError = "Tenant Id must be set.";
            if ((_applicationId == String.Empty)) configError = "ApplicationId must be set.";
            if (_redirectUrl == String.Empty) configError = "Redirect Url cannot be empty when acquiring a delegate token.";
            if (!_isNativeApplication)
                if ((_authCertificate == null) && String.IsNullOrEmpty(_clientSecret))
                    configError = "Client (application) authentication must be specified.";
            if (!String.IsNullOrEmpty(configError))
            {
                _lastError = new Exception("Invalid configuration: " + configError);
                throw _lastError;
            }

            if (!String.IsNullOrEmpty(UserId))
            {
                if (_acquireTokenInProgress.Contains(UserId))
                {
                    // We're already requesting a token for this user
                    return;
                }
                _acquireTokenInProgress.Add(UserId);
            }

            OAuthContext oAuthContext = new OAuthContext();
            oAuthContext.adminConsent = false;
            oAuthContext.authUrl = _authenticationUrl;
            oAuthContext.clientId = _applicationId;
            oAuthContext.resource = _resourceUrl;
            oAuthContext.redirectUrl = _redirectUrl;
            oAuthContext.tenantName = _tenantId;
            oAuthContext.secretKey = _clientSecret;
            oAuthContext.cert = _authCertificate;
            oAuthContext.isV2Endpoint = false;
            oAuthContext.isNativeApplication = _isNativeApplication;
            oAuthContext.ObtainUserConsent = _obtainUserConsent;
            if (!String.IsNullOrEmpty(UserId))
                oAuthContext.userId = UserId;

            try
            {
                _authenticationResult = await GetToken(oAuthContext);
            }
            catch (Exception ex)
            {
                _lastError = ex;
                _authenticationResult = null;
            }

            if (!String.IsNullOrEmpty(UserId))
                _acquireTokenInProgress.Remove(UserId);
        }

        public static async Task<AuthenticationResult> GetToken(OAuthContext oAuthContext)
        {
            // Get OAuth token using client credentials 
            string tenantName = oAuthContext.tenantName;

            if (_authenticationContext == null)
                _authenticationContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(oAuthContext.authUrl + "/" + tenantName, _tokenCache);

            AuthenticationResult authenticationResult = null;

            if (oAuthContext.ObtainUserConsent)
            {
                // We need to get user consent

                FormGetUserPermission formGetPermission = new FormGetUserPermission(oAuthContext);
                if (formGetPermission.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string code = formGetPermission.Code;
                    // When we get our token, it will be cached in the TokenCache, so next time the silent calls will work
                    if (oAuthContext.cert == null)
                    {

                        ClientCredential clientCred = new ClientCredential(oAuthContext.clientId, oAuthContext.secretKey);
                        authenticationResult = await _authenticationContext.AcquireTokenByAuthorizationCodeAsync(code, new Uri(oAuthContext.redirectUrl), clientCred);
                    }
                    else
                    {
                        ClientAssertionCertificate clientCert = new ClientAssertionCertificate(oAuthContext.clientId, oAuthContext.cert);
                        authenticationResult = await _authenticationContext.AcquireTokenByAuthorizationCodeAsync(code, new Uri(oAuthContext.redirectUrl), clientCert);
                    }
                }
                return authenticationResult;
            }

            if (oAuthContext.isNativeApplication)
            {
                if (oAuthContext.adminConsent)
                {
                    authenticationResult = await _authenticationContext.AcquireTokenAsync(oAuthContext.resource,
                        oAuthContext.clientId,
                        new Uri(oAuthContext.redirectUrl),
                        new PlatformParameters(PromptBehavior.Always),
                        UserIdentifier.AnyUser,
                        "prompt=admin_consent");
                }
                else
                {
                    authenticationResult = await _authenticationContext.AcquireTokenAsync(oAuthContext.resource, oAuthContext.clientId, new Uri(oAuthContext.redirectUrl), new PlatformParameters(PromptBehavior.Always));
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(oAuthContext.userId))
                {
                    // We have the UserId for the mailbox we want to access, so we'll try to get a token silently (we should have a cached token)
                    try
                    {
                        if (oAuthContext.cert == null)
                        {
                            ClientCredential clientCred = new ClientCredential(oAuthContext.clientId, oAuthContext.secretKey);
                            authenticationResult = await _authenticationContext.AcquireTokenSilentAsync(oAuthContext.resource, clientCred, new UserIdentifier(oAuthContext.userId, UserIdentifierType.UniqueId));
                        }
                        else
                        {
                            ClientAssertionCertificate clientCert = new ClientAssertionCertificate(oAuthContext.clientId, oAuthContext.cert);
                            authenticationResult = await _authenticationContext.AcquireTokenSilentAsync(oAuthContext.resource, clientCert, new UserIdentifier(oAuthContext.userId, UserIdentifierType.UniqueId));
                        }
                        return authenticationResult;
                    }
                    catch (Exception ex)
                    {
                        _lastError = ex;
                    }
                }


            }
            return authenticationResult;
        }
    }

}
