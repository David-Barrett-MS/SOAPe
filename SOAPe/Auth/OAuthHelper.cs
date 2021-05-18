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

namespace SOAPe.Auth
{
    public class OAuthHelper
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
                .CreateWithApplicationOptions(pcaOptions);

            if (ClientId.Equals("4a03b746-45be-488c-bfe5-0ffdac557d68"))
                pca = pca.WithRedirectUri("http://localhost/SOAPe");

            var app = pca.Build();

            var ewsScopes = new string[] { $"https://outlook.office.com/{Scope}" };

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
