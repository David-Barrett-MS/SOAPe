/*
 * By David Barrett, Microsoft Ltd. 2023. Use at your own risk.  No warranties are given.
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
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;

namespace SOAPe.Auth
{
    public partial class FormROPCAuth : Form
    {
        private TextBox _tokenTextBox = null;
        private HttpClient _httpClient = new HttpClient();
        private FormAzureApplicationRegistration _appRegForm = null;

        public FormROPCAuth(TextBox tokenTextBox, FormAzureApplicationRegistration appRegForm)
        {
            InitializeComponent();
            _tokenTextBox = tokenTextBox;
            _appRegForm = appRegForm;

            _httpClient.BaseAddress = new Uri("https://login.microsoftonline.com/");
        }

        private bool TextboxContainsData(TextBox textbox)
        {
            if (String.IsNullOrEmpty(textbox.Text))
            {
                textBoxUsername.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Attempt to acquire a token using the ROPC flow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAcquireToken_Click(object sender, EventArgs e)
        {
            if (!TextboxContainsData(textBoxUsername))
                return;
            if (!TextboxContainsData(textBoxPassword))
                return;
            if (!TextboxContainsData(textBoxScope))
                return;

            // Build the request
            string authURL = $"{_appRegForm.TenantId}/oauth2/v2.0/token";
            string body = $"grant_type=password&client_id={_appRegForm.ApplicationId}&scope={textBoxScope.Text}&username={textBoxUsername.Text}&password={textBoxPassword.Text}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, authURL);
            request.Content = new StringContent(body, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            // Send the request
            HttpResponseMessage response = _httpClient.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                ROPCResponse token = JsonSerializer.Deserialize<ROPCResponse>(responseString);
                Action action = new Action(() => { _tokenTextBox.Text = token.access_token; });
                if (_tokenTextBox.InvokeRequired)
                    _tokenTextBox.Invoke(action);
                else
                    action();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show($"Error acquiring token: {response.ReasonPhrase}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    internal class ROPCResponse
    {
        public string token_type { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public int ext_expires_in { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}
