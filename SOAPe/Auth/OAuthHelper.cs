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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Identity.Client;

namespace SOAPe.Auth
{
    public class ClassOAuthHelper
    {
        private static Exception _lastError = null;

        public static Exception LastError
        {
            get { return _lastError; }
        }

        public static async Task<AuthenticationResult> GetDelegateToken(string ClientId, string TenantId, string Scope = "EWS.AccessAsUser.All")
        {
            var pcaOptions = new PublicClientApplicationOptions
            {
                ClientId = ClientId,
                TenantId = TenantId
            };

            var pca = PublicClientApplicationBuilder
                .CreateWithApplicationOptions(pcaOptions).Build();

            var ewsScopes = new string[] { $"https://outlook.office.com/{Scope}" };

            try
            {
                // Make the interactive token request
                AuthenticationResult authResult = await pca.AcquireTokenInteractive(ewsScopes).ExecuteAsync();
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
            var ewsScopes = new string[] { "https://outlook.office.com/.default" };

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
            var ewsScopes = new string[] { "https://outlook.office.com/.default" };

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
