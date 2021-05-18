/*
 * By David Barrett, Microsoft Ltd. 2018-2021. Use at your own risk.  No warranties are given.
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

namespace SOAPe.ConfigurationManager
{
    public partial class FormConfigurationName : Form
    {
        public FormConfigurationName(string CurrentName="")
        {
            InitializeComponent();
            textBoxName.Text = CurrentName;
            textBoxName.SelectAll();
        }

        public string GetConfigName(IWin32Window Owner, string CurrentName = "")
        {
            textBoxName.Text = CurrentName;
            textBoxName.SelectAll();
            if (this.ShowDialog(Owner)==DialogResult.OK)
                return textBoxName.Text;
            return CurrentName;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
