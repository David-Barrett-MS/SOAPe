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
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace SOAPe.EWSTools
{
    public partial class FormEWSQuickTest : Form
    {
        private ClassLogger _logger;
        private FormMain _mainform;

        public FormEWSQuickTest(FormMain MainForm)
        {
            InitializeComponent();
            _mainform = MainForm;
        }

        public FormEWSQuickTest(FormMain MainForm, ClassLogger Logger): this(MainForm)
        {
            _logger = Logger;
        }

        private void EnableUrlTextbox()
        {
            textBoxEWSUrl.Enabled = radioButtonUseEWSUrl.Checked;
            textBoxAutodiscoverEmail.Enabled = radioButtonAutodiscover.Checked;
        }

        private ICredentials CurrentCredentials
        {
            get
            {
                ICredentials oCredential;
                oCredential = new NetworkCredential(textBoxUsername.Text, textBoxPassword.Text);
                if (!String.IsNullOrEmpty(textBoxDomain.Text))
                    (oCredential as NetworkCredential).Domain = textBoxDomain.Text;
                return oCredential;
            }
        }

        private void radioButtonAutodiscover_CheckedChanged(object sender, EventArgs e)
        {
            EnableUrlTextbox();
        }

        private void radioButtonUseEWSUrl_CheckedChanged(object sender, EventArgs e)
        {
            EnableUrlTextbox();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            if (radioButtonAutodiscover.Checked)
            {
                // Perform autodiscover
                ClassEWSAutodiscover autodiscover = new ClassEWSAutodiscover(textBoxAutodiscoverEmail.Text, _mainform.CredentialHandler(), listBoxLog, _logger);
                if (autodiscover.Autodiscover())
                {
                    textBoxEWSUrl.Text = autodiscover.EWSUrl;
                    textBoxEWSUrl.Refresh();
                }
                else
                {
                    // Autodiscover failed
                    System.Windows.Forms.MessageBox.Show("Failed to obtain EWS Url.", "Autodiscover failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // We now have an EWS Url, let's do some basic EWS tasks
            if (checkBoxInboxFindItems.Checked)
            {

            }
        }

        private void textBoxAutodiscoverEmail_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAutodiscoverEmail.Text.StartsWith(textBoxUsername.Text) || String.IsNullOrEmpty(textBoxUsername.Text))
                textBoxUsername.Text = textBoxAutodiscoverEmail.Text;
        }
    }
}
