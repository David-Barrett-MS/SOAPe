using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
