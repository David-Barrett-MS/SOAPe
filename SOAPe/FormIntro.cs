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
