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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Web;

namespace SOAPe.Auth
{
    public partial class FormGetUserPermission : Form
    {
        private string _authenticationUrl;
        private OAuthContext _runtimeOptions;
        private string _code = String.Empty;
        private string _id_token = String.Empty;
        private string _nonce = Guid.NewGuid().ToString();
        private HttpListener _listener = null;

        public FormGetUserPermission(OAuthContext runtimeOptions)
        {
            InitializeComponent();
            _runtimeOptions = runtimeOptions;
            textBoxListenAddress.Text = runtimeOptions.redirectUrl;
            textBoxLoginUrl.Text = LoginUrl();
            buttonListen.Enabled = true;
        }

        private string LoginUrl()
        {
            // Build the URL of the sign-in page.

            if (_runtimeOptions.isV2Endpoint)
            {
                _authenticationUrl = _runtimeOptions.authUrl + "/oauth2/v2.0/authorize?" +
                    "response_type=code" +
                    "&client_id=" + _runtimeOptions.clientId +
                    "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(textBoxListenAddress.Text) +
                    "&scope=" + System.Web.HttpUtility.UrlEncode(_runtimeOptions.resource) +
                    "&response_mode=query";
            }
            else if (_runtimeOptions.appConsent)
            {
                // Admin consent for application (app consent always requires administrator):
                // https://login.windows.net/common/oauth2/authorize?
                // response_type=code+id_token&scope=openid&nonce=c328d2df-43d1-4e4d-a884-7cfb492beadc&client_id=514b994f-904c-4aa8-b575-0314edc390e4&redirect_uri=http:%2f%2flocalhost%2fcode&resource=https:%2f%2foutlook.office365.com&prompt=admin_consent&response_mode=form_post
                _authenticationUrl = _runtimeOptions.authUrl + "/oauth2/v2.0/authorize?" +
                    "response_type=code+id_token&scope=openid" +
                    "&nonce=" + _nonce +
                    "&client_id=" + _runtimeOptions.clientId +
                    "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(textBoxListenAddress.Text) +
                    "&resource=" + System.Web.HttpUtility.UrlEncode(_runtimeOptions.resource) +
                    "&prompt=admin_consent&response_mode=form_post";
            }
            else
            {
                _authenticationUrl = _runtimeOptions.authUrl + "/oauth2/authorize?" +
                    "resource=" + System.Web.HttpUtility.UrlEncode(_runtimeOptions.resource) +
                    "&response_type=code" +
                    "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(textBoxListenAddress.Text) +
                    "&client_id=" + _runtimeOptions.clientId;

                if (_runtimeOptions.adminConsent)
                {
                    _authenticationUrl += "&prompt=admin_consent";
                }
                else
                {
                    _authenticationUrl += "&prompt=login";
                }
            }
            return _authenticationUrl;
        }

        private void buttonLaunchURL_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(textBoxLoginUrl.Text);
        }

        private void buttonListen_Click(object sender, EventArgs e)
        {
            // Start our HTTP listener (on our redirect Url)
            string listenUrl = textBoxListenAddress.Text;
            if (!listenUrl.EndsWith("/"))
                listenUrl += "/";
            _listener = new HttpListener();
            _listener.Prefixes.Clear();
            _listener.Prefixes.Add(listenUrl);

            try          {
                _listener.Start();
                _listener.BeginGetContext(new AsyncCallback(ListenerCallback), _listener);
                buttonListen.Enabled = false;
                textBoxListenAddress.ReadOnly = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Unable to start HTTP listener{0}(are you running as administrator?):{0}{1}",Environment.NewLine, ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ListenerCallback(IAsyncResult result)
        {
            // This is called when we have a web request - which should be when user is redirected after auth
            HttpListener listener = (HttpListener)result.AsyncState;
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerRequest request = context.Request; // This is a request, though actually it is a response to the user auth :-)

            string sRequest = String.Empty;
            using (StreamReader reader = new StreamReader(request.InputStream))
            {
                sRequest = reader.ReadToEnd();
            }

            context.Response.OutputStream.Flush();
            context.Response.Close();

            if (request.Url.Query.Contains("code"))
            {
                // We have the code in query string (GET)
                if (textBoxCode.InvokeRequired)
                {
                    textBoxCode.Invoke(new MethodInvoker(delegate ()
                    {
                        textBoxCode.Text = request.Url.ToString();
                    }));
                }
                else
                    textBoxCode.Text = request.Url.ToString();
            }
            else
            {
                // Check payload for code/token (POST)
                NameValueCollection authInfo = HttpUtility.ParseQueryString(sRequest);
                IDictionary<string, string> authParams = authInfo
                    .Cast<string>()
                    .Select(s => new { Key = s, Value = authInfo[s] })
                    .ToDictionary(d => d.Key, d => d.Value);

                if (authParams.ContainsKey("id_token"))
                {
                    // The id_token is our OAuth token.  This is what the application needs to access mailboxes.
                    _id_token = authParams["id_token"];
                }

                if (authParams.ContainsKey("code"))
                {
                    // We don't need the code, we just want the token, but we put it in the textbox as that is what we are watching for the response
                    if (textBoxCode.InvokeRequired)
                    {
                        textBoxCode.Invoke(new MethodInvoker(delegate ()
                        {
                            textBoxCode.Text = "code=" + authParams["code"];
                        }));
                    }
                    else
                        textBoxCode.Text = "code=" + authParams["code"];
                }
            }

            if (buttonListen.InvokeRequired)
            {
                buttonListen.Invoke(new MethodInvoker(delegate ()
                {
                    buttonListen.Enabled = true;
                }));
            }
            else
                buttonListen.Enabled = true;
            if (textBoxListenAddress.InvokeRequired)
            {
                textBoxListenAddress.Invoke(new MethodInvoker(delegate ()
                {
                    textBoxListenAddress.ReadOnly = false;
                }));
            }
            else
                textBoxListenAddress.Enabled = false;
        }

        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCode.Text.Contains("code"))
            {
                // Read the code from the query string
                try
                {
                    NameValueCollection queryElements = System.Web.HttpUtility.ParseQueryString(textBoxCode.Text.Substring(textBoxCode.Text.IndexOf("?")));
                    _code = queryElements["code"];
                }
                catch { }

                if (!String.IsNullOrEmpty(_code))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        public string Code
        {
            get { return _code; }
        }

        public string Token
        {
            get { return _id_token; }
        }

        public string Nonce
        {
            get { return _nonce; }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBoxListenAddress_TextChanged(object sender, EventArgs e)
        {
            textBoxLoginUrl.Text = LoginUrl();
        }
    }
}
