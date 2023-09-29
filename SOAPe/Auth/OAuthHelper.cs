/*
 * By David Barrett, Microsoft Ltd. 2018. Use at your own risk.  No warranties are given.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * */

using System;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Broker;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Identity.Client.Extensions.Msal;

namespace SOAPe.Auth
{
    public class OAuthHelper
    {
        private static Exception _lastError = null;
        private static string _resourceUrl = "https://outlook.office.com/";
        private static string _redirectUrl = "http://localhost/SOAPe";
        private static IntPtr _WAMParentWindow;
        private static bool _windowsAuthInProgress = false;
        private static AuthenticationResult _windowsAuthResult = null;

        public static Exception LastError
        {
            get { return _lastError; }
        }

        public static string ResourceUrl
        {
            get { return _resourceUrl; }
            set
            {
                _resourceUrl = value;
                if (!_resourceUrl.EndsWith("/"))
                    _resourceUrl = $"{_resourceUrl}/";
            }
        }

        public static string RedirectUrl
        {
            get { return _redirectUrl; }
            set { _redirectUrl = value; }
        }

        public static IntPtr GetWindowHandle()
        {
            return _WAMParentWindow;
        }

        public static async Task<AuthenticationResult> IntegratedWindowsAuth(string ClientId, System.Windows.Forms.Form ParentWindow, string Scope = "EWS.AccessAsUser.All")
        {
            // https://learn.microsoft.com/en-us/azure/active-directory/develop/scenario-desktop-acquire-token-wam#wam-calling-pattern

            Action getHandleAction = new Action(() =>
            {
                _WAMParentWindow = ParentWindow.Handle;
            });
            if (ParentWindow.InvokeRequired)
                ParentWindow.Invoke(getHandleAction);
            else
                getHandleAction();

            BrokerOptions options = new BrokerOptions(BrokerOptions.OperatingSystems.Windows);

            options.Title = "SOAPe";
            options.ListOperatingSystemAccounts = true;
            _windowsAuthInProgress = true;
            _windowsAuthResult = null;

            var storageProperties =
                 new StorageCreationPropertiesBuilder("SOAPe_msal_cache.txt", MsalCacheHelper.UserRootDirectory)
                 .Build();

            Action action = new Action(async () =>
            {
                IPublicClientApplication app =
                PublicClientApplicationBuilder.Create(ClientId)
                .WithRedirectUri(_redirectUrl)
                .WithBroker(options)
                .Build();

                var cacheHelper = await MsalCacheHelper.CreateAsync(storageProperties);
                cacheHelper.RegisterCache(app.UserTokenCache);



                // Is there an account in the cache?
                IAccount accountToLogin = (await app.GetAccountsAsync()).FirstOrDefault();
                if (accountToLogin == null)
                {
                    // 3. No account in the cache; try to log in with the OS account
                    accountToLogin = PublicClientApplication.OperatingSystemAccount;
                }

                try
                {
                    // 4. Silent authentication 
                    _windowsAuthResult = await app.AcquireTokenSilent(new[] { Scope }, accountToLogin)
                                                .ExecuteAsync();
                }
                // Cannot log in silently - most likely Azure AD would show a consent dialog or the user needs to re-enter credentials
                catch (MsalUiRequiredException)
                {
                    // 5. Interactive authentication
                    try
                    {
                        _windowsAuthResult = await app.AcquireTokenInteractive(new[] { Scope })
                                                    .WithAccount(accountToLogin)
                                                    // This is mandatory so that WAM is correctly parented to your app; read on for more guidance
                                                    .WithParentActivityOrWindow(GetWindowHandle())
                                                    .ExecuteAsync();
                    }
                    catch { }
                    // Consider allowing the user to re-authenticate with a different account, by calling AcquireTokenInteractive again                                  
                }

                _windowsAuthInProgress = false;
            });

            if (ParentWindow.InvokeRequired)
                ParentWindow.Invoke(action);
            else
                action.Invoke();

            while (_windowsAuthInProgress)
                System.Threading.Thread.Yield();
            return _windowsAuthResult;
        }


        public static async Task<AuthenticationResult> GetDelegateToken(string ClientId, string TenantId, string Scope = "EWS.AccessAsUser.All")
        {
            var pcaOptions = new PublicClientApplicationOptions
            {
                ClientId = ClientId,
                TenantId = TenantId
            };

            var pca = PublicClientApplicationBuilder
                .CreateWithApplicationOptions(pcaOptions);

            pca = pca.WithRedirectUri(_redirectUrl);

            var app = pca.Build();

            var ewsScopes = new string[] { $"{_resourceUrl}{Scope}" };

            try
            {
                // Make the interactive token request
                AuthenticationResult authResult = await app.AcquireTokenInteractive(ewsScopes).ExecuteAsync();
                return authResult;
            }
            catch (Exception ex)
            {
                _lastError = ex;
            }
            return null;
        }

        public static async Task<AuthenticationResult> GetApplicationToken(string ClientId, string TenantId, string ClientSecret)
        {
            // Configure the MSAL client to get tokens
            var ewsScopes = new string[] { $"{_resourceUrl}.default" };

            var app = ConfidentialClientApplicationBuilder.Create(ClientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, TenantId)
                .WithClientSecret(ClientSecret)
                .Build();

            AuthenticationResult result = null;
            try
            {
                // Make the token request (should not be interactive, unless Consent required)
                result = await app.AcquireTokenForClient(ewsScopes)
                    .ExecuteAsync();
            }
            catch (Exception ex)
            {
                _lastError = ex;
            }
            return result;
        }

        public static async Task<AuthenticationResult> GetApplicationToken(string ClientId, string TenantId, X509Certificate2 ClientCertificate)
        {
            // Configure the MSAL client to get tokens
            var ewsScopes = new string[] { $"{_resourceUrl}.default" };

            var app = ConfidentialClientApplicationBuilder.Create(ClientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, TenantId)
                .WithCertificate(ClientCertificate)
                .Build();

            AuthenticationResult result = null;
            try
            {
                // Make the token request (should not be interactive, unless Consent required)
                result = await app.AcquireTokenForClient(ewsScopes)
                    .ExecuteAsync();
            }
            catch (Exception ex)
            {
                _lastError = ex;
            }
            return result;
        }
    }
}
