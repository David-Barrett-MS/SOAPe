/*
 * By David Barrett, Microsoft Ltd. 2012. Use at your own risk.  No warranties are given.
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
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace SOAPe.EWSTools
{
    public partial class FormEWSAutodiscover : Form
    {
        string _EWSUrl = "";
        private ICredentials _credentials = null;
        private ClassLogger _logger = null;
        private Control _targetControl = null; // The control into which any autodiscovered URL will be put

        public FormEWSAutodiscover()
        {
            InitializeComponent();
        }

        public FormEWSAutodiscover(ICredentials Credentials)
        {
            InitializeComponent();
            _credentials = Credentials;
            PopulateCredentials();
        }

        public FormEWSAutodiscover(ICredentials Credentials, Control TargetControl)
            : this(Credentials)
        {
            _targetControl = TargetControl;
        }

        public FormEWSAutodiscover(ICredentials Credentials, Control TargetControl, ClassLogger Logger)
            : this(Credentials, TargetControl)
        {
            _logger = Logger;
        }

        public string AutodiscoverSMTPAddress
        {
            get
            {
                return textBoxSMTP.Text;
            }
            set
            {
                textBoxSMTP.Text = value;
            }
        }

        public ICredentials Credentials
        {
            get
            {
                if (_credentials == null)
                {
                    // Need to rebuild credentials
                    if (String.IsNullOrEmpty(textBoxDomain.Text))
                    {
                        _credentials = new NetworkCredential(textBoxUsername.Text, textBoxPassword.Text);
                    }
                    else
                        _credentials = new NetworkCredential(textBoxUsername.Text, textBoxPassword.Text, textBoxDomain.Text);
                }
                return _credentials;
            }
            set
            {
                _credentials = value;
                PopulateCredentials();
            }
        }

        private void PopulateCredentials()
        {
            try
            {
                ICredentials cred = _credentials;
                if (cred == CredentialCache.DefaultCredentials)
                {
                    // Using default credentials, we won't be able to pull details back
                    textBoxUsername.Text = "";
                    textBoxPassword.Text = "";
                    textBoxDomain.Text = "";
                    SetUserBackColour(this.BackColor);
                }
                else
                {
                    textBoxUsername.Text = (cred as NetworkCredential).UserName;
                    textBoxPassword.Text = (cred as NetworkCredential).Password;
                    textBoxDomain.Text = (cred as NetworkCredential).Domain;
                }

                _credentials = cred;
            }
            catch { }
        }

        public string GetEWSUrl(string SMTPAddress = "", IWin32Window Parent = null)
        {
            if (!String.IsNullOrEmpty(SMTPAddress))
                textBoxSMTP.Text = SMTPAddress;
            if (Parent == null)
            {
                this.ShowDialog();
            }
            else
                this.ShowDialog(Parent);
            return _EWSUrl;
        }

        private void buttonAutodiscover_Click(object sender, EventArgs e)
        {
            string sDomain = "";
            try
            {
                sDomain = textBoxSMTP.Text.Substring(textBoxSMTP.Text.IndexOf('@') + 1);
            }
            catch { }
            if (String.IsNullOrEmpty(sDomain))
            {
                System.Windows.Forms.MessageBox.Show("Invalid email address", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SetFormControls(false);
            Application.UseWaitCursor = true;
            Application.DoEvents();

            ClassEWSAutodiscover oAutoDiscover = new ClassEWSAutodiscover(textBoxSMTP.Text, Credentials, listBoxLog,_logger);
            oAutoDiscover.IgnoreCertificateErrors = checkBoxIgnoreCertificateErrors.Checked;
            if (oAutoDiscover.Autodiscover(checkBoxSkipSCPAutodiscover.Checked))
            {
                // Successfully retrieved settings
                _EWSUrl = oAutoDiscover.EWSUrl;

                // Set the target control (if we have one... ignore any errors)
                try
                {
                    Type t = _targetControl.GetType();
                    t.InvokeMember("Text", System.Reflection.BindingFlags.SetProperty, null, _targetControl, new object[] { oAutoDiscover.EWSUrl });
                }
                catch { }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Unable to determine EWS URL", "AutoDiscover failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            SetFormControls(true); 
            Application.UseWaitCursor = false;
            buttonClose.Enabled=true;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            SetFormControls(true);
            this.Hide();
        }

        private void SetFormControls(bool Enabled)
        {
            buttonAutodiscover.Enabled = Enabled;
            buttonClose.Enabled = Enabled;
            textBoxSMTP.Enabled = Enabled;
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            listBoxLog.Items.Clear();
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {
            _credentials = null;
            if (textBoxUsername.BackColor == this.BackColor)
                SetUserBackColour(textBoxSMTP.BackColor);
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            _credentials = null;
            if (textBoxPassword.BackColor == this.BackColor)
                SetUserBackColour(textBoxSMTP.BackColor);
        }

        private void textBoxDomain_TextChanged(object sender, EventArgs e)
        {
            _credentials = null;
            if (textBoxDomain.BackColor == this.BackColor)
                SetUserBackColour(textBoxSMTP.BackColor);
        }

        private void SetUserBackColour(Color backColour)
        {
            textBoxUsername.BackColor = backColour;
            textBoxPassword.BackColor = backColour;
            textBoxDomain.BackColor = backColour;
        }

        private void FormEWSAutodiscover_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

    }
}
