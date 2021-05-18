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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPe
{
    public partial class FormUserControlTest : Form
    {
        public FormUserControlTest()
        {
            InitializeComponent();
        }

        private void FormUserControlTest_Load(object sender, EventArgs e)
        {

        }

        private void dateTimeEdit1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = String.Format("{0} {1}", dateTimeEdit1.Value.ToShortDateString(), dateTimeEdit1.Value.ToLongTimeString());
        }

        private void tabPageAuth_Click(object sender, EventArgs e)
        {

        }
    }
}
