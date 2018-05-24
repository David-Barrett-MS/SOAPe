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
