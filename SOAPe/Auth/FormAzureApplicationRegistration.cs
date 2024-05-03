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
        private TextBox _tokenTextBox = null;

        // Events
        public delegate void AuthCompleteEventHandler(object sender, EventArgs e);
        public event AuthCompleteEventHandler AuthComplete;

        public FormAzureApplicationRegistration()
        {
            InitializeComponent();
            _formConfig = new ConfigurationManager.ClassFormConfig(this,true);
            _formConfig.ExcludedControls.Add(textBoxAuthCertificate);
            ClassFormConfig.ApplyConfiguration();
            UpdateAuthUI();

            // SOAPe is no longer registered in the Microsoft tenant, so if that app Id is being used then update it
            if (textBoxApplicationId.Text== "4a03b746-45be-488c-bfe5-0ffdac557d68")
                textBoxApplicationId.Text = "00d8c1e0-fe3c-40d3-8791-0f1132fed50b"; // Default SOAPe App Id
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
            bool appAuthEnabled = !radioButtonAuthAsNativeApp.Checked && !radioButtonIntegratedWindowsAuth.Checked && !radioButtonROPCAuth.Checked;

            if (appAuthEnabled)
            {
                bool bUsingClientSecret = radioButtonAuthWithClientSecret.Checked;
                textBoxAuthCertificate.Enabled = !bUsingClientSecret;
                buttonLoadCertificate.Enabled = !bUsingClientSecret;
                textBoxClientSecret.Enabled = bUsingClientSecret;
                labelRedirectURL.Enabled = false;
                textBoxRedirectUrl.Enabled = false;
            }
            else
            {
                labelRedirectURL.Enabled = true;
                textBoxRedirectUrl.Enabled = true;
                textBoxAuthCertificate.Enabled = false;
                buttonLoadCertificate.Enabled = false;
                textBoxClientSecret.Enabled = false;
            }
        }

        public bool HaveValidAppConfig()
        {
            // Return true if all the application information is present and valid

            StringBuilder sAppInfoErrors = new StringBuilder();

            if (String.IsNullOrEmpty(textBoxTenantId.Text)) { sAppInfoErrors.AppendLine("Tenant Id must be specified (e.g. tenant.onmicrosoft.com)."); }
            if (String.IsNullOrEmpty(textBoxResourceUrl.Text))
                sAppInfoErrors.AppendLine("Resource Url must be specified (e.g. https://outlook.office365.com).");
            else
                OAuthHelper.ResourceUrl = textBoxResourceUrl.Text;
            if (String.IsNullOrEmpty(textBoxApplicationId.Text)) { sAppInfoErrors.AppendLine("Application Id must be specified (as registered in Azure AD)."); }

            // Flow specific checks
            if (radioButtonAuthWithClientSecret.Checked && String.IsNullOrEmpty(textBoxClientSecret.Text)) { sAppInfoErrors.AppendLine("Client secret cannot be empty."); }
            if (radioButtonAuthWithCertificate.Checked && String.IsNullOrEmpty(textBoxAuthCertificate.Text)) { sAppInfoErrors.AppendLine("Certificate required."); }
            if (radioButtonAuthAsNativeApp.Checked && String.IsNullOrEmpty(textBoxRedirectUrl.Text)) { sAppInfoErrors.AppendLine("Redirect URL required."); }

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
            {
                if (radioButtonIntegratedWindowsAuth.Checked)
                    tokenText = "Authentication failed";
                else
                    tokenText = "Auth result is null";
            }

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
                if (AuthComplete != null)
                    AuthComplete(this, new EventArgs());
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
                if (AuthComplete != null)
                    AuthComplete(this, new EventArgs());
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
                if (AuthComplete != null)
                    AuthComplete(this, new EventArgs());
            });
            Task.Run(action);
        }

        public void AcquireIntegratedWindowsAuthToken()
        {
            Action action = new Action(async () =>
            {
                try
                {
                    Form parentForm = this;
                    if (!this.Visible)
                        parentForm = (Form)_tokenTextBox.TopLevelControl;
                    AuthenticationResult authenticationResult = await OAuthHelper.IntegratedWindowsAuth(textBoxApplicationId.Text, parentForm);
                    _lastAuthResult = authenticationResult;
                    UpdateTokenTextbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to acquire token", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (AuthComplete != null)
                    AuthComplete(this, new EventArgs());
            });
            Task.Run(action);
        }

        public void AcquireROPCToken()
        {
            Form parentForm = this;
            if (!this.Visible)
                parentForm = (Form)_tokenTextBox.TopLevelControl;
            FormROPCAuth formROPCAuth = new FormROPCAuth(_tokenTextBox, this);
            if (formROPCAuth.ShowDialog(parentForm) != DialogResult.Cancel)
                Hide();
            if (AuthComplete != null)
                AuthComplete(this, new EventArgs());
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
            else if (radioButtonIntegratedWindowsAuth.Checked)
                AcquireIntegratedWindowsAuthToken();
            else if (radioButtonROPCAuth.Checked)
                AcquireROPCToken();
        }

        private void buttonAcquireToken_Click(object sender, EventArgs e)
        {
            AcquireToken();
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
                if (!String.IsNullOrEmpty(formChooseCert.Certificate.FriendlyName))
                    textBoxAuthCertificate.Text = formChooseCert.Certificate.FriendlyName;
                else
                    textBoxAuthCertificate.Text = formChooseCert.Certificate.Thumbprint;
            }
            formChooseCert.Dispose();
        }

        private void buttonROPCAuth_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxTenantId.Text) || String.IsNullOrEmpty(textBoxApplicationId.Text))
            {
                MessageBox.Show("Set Tenant Id and Application Id before attempting ROPC.",
                    "Information Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FormROPCAuth formROPCAuth = new FormROPCAuth(_tokenTextBox, this);
            if (formROPCAuth.ShowDialog(this) != DialogResult.Cancel)
            {
                Hide();
                if (AuthComplete != null)
                    AuthComplete(this, new EventArgs());
            }
        }

        private void textBoxRedirectUrl_Validated(object sender, EventArgs e)
        {
            OAuthHelper.RedirectUrl = textBoxRedirectUrl.Text;
        }

        private void textBoxResourceUrl_Validated(object sender, EventArgs e)
        {
            OAuthHelper.ResourceUrl = textBoxResourceUrl.Text;
        }

        private void FormAzureApplicationRegistration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // Don't close the form, just hide it
                this.Hide();
            }
        }
    }
}
