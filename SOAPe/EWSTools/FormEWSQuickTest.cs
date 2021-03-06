﻿/*
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
