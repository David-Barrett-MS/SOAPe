/*
 * By David Barrett, Microsoft Ltd. 2019. Use at your own risk.  No warranties are given.
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Identity.Client;
using System.Security.Cryptography.X509Certificates;
using SOAPe.ConfigurationManager;

namespace SOAPe.Auth
{
    public partial class FormAzureApplicationRegistration : Form
    {
        private ConfigurationManager.ClassFormConfig _formConfig = null;
        private AuthenticationResult _lastAuthResult = null;
        private OAuthHelper _oAuthHelper = new OAuthHelper();
        private TextBox _tokenTextBox = null;

        public FormAzureApplicationRegistration()
        {
            InitializeComponent();
            _formConfig = new ConfigurationManager.ClassFormConfig(this,true);
            _formConfig.ExcludedControls.Add(textBoxAuthCertificate);
            ClassFormConfig.ApplyConfiguration();
            textBoxTenantId_TextChanged(null, null);
            UpdateAuthUI();
        }

        public string AccessToken
        {
            get
            {
                if (_lastAuthResult == null) return null;
                return _lastAuthResult.AccessToken;
            }
        }

        public string TenantId
        {
            get { return textBoxTenantId.Text; }
        }

        public string ResourceUrl
        {
            get { return textBoxResourceUrl.Text; }
        }

        public string ApplicationId
        {
            get { return textBoxApplicationId.Text; }
        }

        public TextBox TokenTextBox
        {
            get { return _tokenTextBox; }
            set { _tokenTextBox = value; }
        }

        private void UpdateAuthUI()
        {
            // Native application does not authenticate
            bool authEnabled = !radioButtonAuthAsNativeApp.Checked;
            foreach (Control control in groupBoxAuth.Controls)
            {
                if (!(control is RadioButton))
                    control.Enabled = authEnabled;
            }

            if (authEnabled)
            {
                bool bUsingClientSecret = radioButtonAuthWithClientSecret.Checked;
                textBoxAuthCertificate.Enabled = !bUsingClientSecret;
                buttonLoadCertificate.Enabled = !bUsingClientSecret;
                textBoxClientSecret.Enabled = bUsingClientSecret;
            }
        }

        public bool HaveValidAppConfig()
        {
            // Return true if all the application information is present and valid

            StringBuilder sAppInfoErrors = new StringBuilder();

            if (String.IsNullOrEmpty(textBoxTenantId.Text)) { sAppInfoErrors.AppendLine("Tenant Id must be specified (e.g. tenant.onmicrosoft.com)"); }
            if (String.IsNullOrEmpty(textBoxResourceUrl.Text)) { sAppInfoErrors.AppendLine("Resource Url must be specified (e.g. https://outlook.office365.com)"); }
            if (String.IsNullOrEmpty(textBoxApplicationId.Text)) { sAppInfoErrors.AppendLine("Application Id must be specified (as registered in Azure AD)"); }
            if (radioButtonAuthWithClientSecret.Checked && String.IsNullOrEmpty(textBoxClientSecret.Text)) { sAppInfoErrors.AppendLine("Client secret cannot be empty"); }
            if (radioButtonAuthWithCertificate.Checked && String.IsNullOrEmpty(textBoxAuthCertificate.Text)) { sAppInfoErrors.AppendLine("Certificate required"); }

            if (sAppInfoErrors.Length<1)
                return true;

            if (this.Visible)
            {
                sAppInfoErrors.AppendLine("Please fix invalid configuration and try again.");
                System.Windows.Forms.MessageBox.Show(this, sAppInfoErrors.ToString(), "Application Configuration Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void UpdateTokenTextbox()
        {
            // If we have a textbox for the access token, update it with our current token
            string tokenText = "Failed to retrieve token";
            if (_lastAuthResult != null)
                tokenText = _lastAuthResult.AccessToken;
            else if (OAuthHelper.LastError != null)
                tokenText = OAuthHelper.LastError.Message;
            else if (_lastAuthResult == null)
                tokenText = "Auth result is null";

            if (_tokenTextBox == null)
                return;
            try
            {
                if (_tokenTextBox.InvokeRequired)
                {
                    _tokenTextBox.Invoke(new MethodInvoker(delegate ()
                    {
                        _tokenTextBox.Text = tokenText;
                    }));
                }
                else
                    _tokenTextBox.Text = tokenText;
            }
            catch { }
        }

        public void AcquireNativeAppToken()
        {
            Action action = new Action(async () =>
            {
                try
                {
                    AuthenticationResult authenticationResult = await OAuthHelper.GetDelegateToken(textBoxApplicationId.Text, textBoxTenantId.Text);
                    _lastAuthResult = authenticationResult;
                    UpdateTokenTextbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to acquire token", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            Task.Run(action);
        }

        public void AcquireAppTokenWithSecret()
        {
            Action action = new Action(async () =>
            {
                try
                {
                    AuthenticationResult authenticationResult = await OAuthHelper.GetApplicationToken(textBoxApplicationId.Text, textBoxTenantId.Text,textBoxClientSecret.Text);
                    _lastAuthResult = authenticationResult;
                    UpdateTokenTextbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to acquire token", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            Task.Run(action);
        }

        public void AcquireAppTokenWithCertificate()
        {
            Action action = new Action(async () =>
            {
                try
                {
                    AuthenticationResult authenticationResult = await OAuthHelper.GetApplicationToken(textBoxApplicationId.Text, textBoxTenantId.Text, (X509Certificate2)textBoxAuthCertificate.Tag);
                    _lastAuthResult = authenticationResult;
                    UpdateTokenTextbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to acquire token", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            Task.Run(action);
        }

        public void AcquireToken()
        {
            if (!HaveValidAppConfig())
            {
                if (!this.Visible)
                {
                    this.ShowDialog();
                    HaveValidAppConfig(); // Do this to show the invalid configuration to the user
                }
                return;
            }
            if (radioButtonAuthAsNativeApp.Checked)
                AcquireNativeAppToken();
            else if (radioButtonAuthWithClientSecret.Checked)
                AcquireAppTokenWithSecret();
            else if (radioButtonAuthWithCertificate.Checked)
                AcquireAppTokenWithCertificate();
        }

        private void buttonAcquireToken_Click(object sender, EventArgs e)
        {
            AcquireToken();
        }

        private void textBoxTenantId_TextChanged(object sender, EventArgs e)
        {
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void radioButtonAuthAsNativeApp_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthUI();
        }

        private void radioButtonAuthWithClientSecret_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthUI();
        }

        private void radioButtonAuthWithCertificate_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthUI();
        }

        private void buttonLoadCertificate_Click(object sender, EventArgs e)
        {
            FormChooseAuthCertificate formChooseCert = new FormChooseAuthCertificate();
            formChooseCert.ShowDialog(this);
            if (formChooseCert.Certificate != null)
            {
                textBoxAuthCertificate.Tag = formChooseCert.Certificate;
                textBoxAuthCertificate.Text = formChooseCert.Certificate.FriendlyName;
            }
            formChooseCert.Dispose();
        }
    }
}
