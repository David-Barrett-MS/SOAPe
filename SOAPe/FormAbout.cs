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
