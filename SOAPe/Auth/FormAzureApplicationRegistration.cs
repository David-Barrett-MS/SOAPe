/*
 * By David Barrett, Microsoft Ltd. 2019. Use at your own risk.  No warranties are given.
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
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace SOAPe.Auth
{
    public partial class FormAzureApplicationRegistration : Form
    {
        private ClassFormConfig _formConfig = null;
        private AuthenticationResult _lastAuthResult = null;
        private ClassOAuthHelper _oAuthHelper = new ClassOAuthHelper();

        public FormAzureApplicationRegistration()
        {
            InitializeComponent();
            _formConfig = new ClassFormConfig(this);
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
            get { return comboBoxResourceUrl.Text; }
        }

        public string RedirectUrl
        {
            get { return textBoxRedirectUrl.Text; }
        }

        public string AuthenticationUrl
        {
            get { return comboBoxAuthenticationUrl.Text; }
        }

        public string ApplicationId
        {
            get { return textBoxApplicationId.Text; }
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
            if (String.IsNullOrEmpty(comboBoxResourceUrl.Text)) { sAppInfoErrors.AppendLine("Resource Url must be specified (e.g. https://outlook.office365.com)"); }
            if (String.IsNullOrEmpty(textBoxRedirectUrl.Text)) { sAppInfoErrors.AppendLine("Redirect Url must be specified (e.g. http://localhost/code)"); }
            if (String.IsNullOrEmpty(comboBoxAuthenticationUrl.Text)) { sAppInfoErrors.AppendLine("Authentication Url must be specified (e.g. https://login.microsoftonline.com/common)"); }
            if (String.IsNullOrEmpty(textBoxApplicationId.Text)) { sAppInfoErrors.AppendLine("Application Id must be specified (as registered in Azure AD)"); }

            if (sAppInfoErrors.Length<1)
                return true;

            if (this.Visible)
            {
                sAppInfoErrors.AppendLine("Please fix invalid configuration and try again.");
                System.Windows.Forms.MessageBox.Show(this, sAppInfoErrors.ToString(), "Application Configuration Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public bool AcquireNativeAppToken()
        {
            if (!HaveValidAppConfig())
            {
                if (!this.Visible)
                { 
                    this.ShowDialog();
                    HaveValidAppConfig(); // Do this to show the invalid configuration to the user
                }
                return false;
            }

            try
            {
                AuthenticationContext authenticationContext = new AuthenticationContext(comboBoxAuthenticationUrl.Text, _oAuthHelper.TokenCache);
                _lastAuthResult = authenticationContext.AcquireTokenAsync(comboBoxResourceUrl.Text, textBoxApplicationId.Text, new Uri(textBoxRedirectUrl.Text), new PlatformParameters(PromptBehavior.Always)).Result;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Unable to acquire token", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void buttonAcquireToken_Click(object sender, EventArgs e)
        {
            if (radioButtonAuthAsNativeApp.Checked)
            {
                AcquireNativeAppToken();
                return;
            }
        }

        private void textBoxTenantId_TextChanged(object sender, EventArgs e)
        {
            comboBoxAuthenticationUrl.Items[1] = "https://login.microsoftonline.com/" + textBoxTenantId.Text;
            if (comboBoxAuthenticationUrl.SelectedIndex == 1)
            {
                comboBoxAuthenticationUrl.Text = (String)comboBoxAuthenticationUrl.Items[1];
            }
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

        private void buttonManageTokens_Click(object sender, EventArgs e)
        {
            FormTokenViewer formTokenViewer = new FormTokenViewer(_oAuthHelper.TokenCache);
            formTokenViewer.Show();
        }

        private void buttonRegisterApplication_Click(object sender, EventArgs e)
        {

        }
    }
}
