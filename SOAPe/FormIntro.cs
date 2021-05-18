/*
 * By David Barrett, Microsoft Ltd.  Use at your own risk.  No warranties are given.
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

namespace SOAPe
{
    public partial class FormIntro : Form
    {
        public FormIntro()
        {
            InitializeComponent();
            labelApplicationName.Text = Application.ProductName;
            textBoxVersion.Text = String.Format("v{0}", Application.ProductVersion);
            textBoxVersion.Select(0,0);
            ShowReleaseNotes();
            textBoxVersion.ReadOnly = true;
            buttonClose.Focus();
        }

        private void ShowReleaseNotes()
        {
            // Load the ReleaseNotes.txt file and display

            textBoxNotes.Text = "ReleaseNotes.txt not found.";

            string releaseNotesFile = "ReleaseNotes.txt";
            if (!System.IO.File.Exists(releaseNotesFile))
                return;

            try
            {
                textBoxNotes.Text = System.IO.File.ReadAllText(releaseNotesFile);
            }
            catch { }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void labelApplicationName_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/David-Barrett-MS/SOAPe");
            }
            catch { }
        }
    }
}
