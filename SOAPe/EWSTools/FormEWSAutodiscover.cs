/*
 * By David Barrett, Microsoft Ltd. 2012. Use at your own risk.  No warranties are given.
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
using System.Windows.Forms;

namespace SOAPe.EWSTools
{
    public partial class FormEWSAutodiscover : Form
    {
        string _EWSUrl = "";
        //private ICredentials _credentials = null;
        private FormMain _authForm; // Reference to the main form so that we can apply authentication
        private ClassLogger _logger = null;
        private Control _targetControl = null; // The control into which any autodiscovered URL will be put

        public FormEWSAutodiscover()
        {
            InitializeComponent();
        }

        public FormEWSAutodiscover(FormMain AuthForm)
        {
            InitializeComponent();
            _authForm = AuthForm;
        }

        public FormEWSAutodiscover(FormMain AuthForm, Control TargetControl)
            : this(AuthForm)
        {
            _targetControl = TargetControl;
        }

        public FormEWSAutodiscover(FormMain AuthForm, Control TargetControl, ClassLogger Logger)
            : this(AuthForm, TargetControl)
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
            ClassEWSAutodiscover oAutoDiscover = new ClassEWSAutodiscover(textBoxSMTP.Text, _authForm.CredentialHandler(), listBoxLog,_logger);
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
