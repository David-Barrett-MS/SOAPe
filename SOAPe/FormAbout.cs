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
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            labelApplicationName.Text = Application.ProductName;
            textBoxVersion.Text = String.Format("v{0}", Application.ProductVersion);
            textBoxVersion.Select(0,0);
            textBoxVersion.ReadOnly = true;
            buttonClose.Focus();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void labelDeveloper_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start((string)labelDeveloper.Tag);
            }
            catch { }
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
