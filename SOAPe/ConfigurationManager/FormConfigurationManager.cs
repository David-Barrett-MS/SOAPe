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
    public partial class FormConfigurationManager : Form
    {
        public FormConfigurationManager()
        {
            InitializeComponent();
            ShowConfigurations();
            UpdateButtons();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowConfigurations(string selectConfiguration = "")
        {
            listBoxConfigs.BeginUpdate();
            string selectedConfig = selectConfiguration;
            if (String.IsNullOrEmpty(selectConfiguration))
            {
                selectedConfig = ClassFormConfig.ActiveConfiguration;
                if (listBoxConfigs.SelectedIndex >= 0)
                    selectedConfig = (string)listBoxConfigs.SelectedItem;
            }
            listBoxConfigs.Items.Clear();
            foreach (string configName in ClassFormConfig.ConfigurationSetNames())
            {
                int index = listBoxConfigs.Items.Add(configName);
                if (configName.Equals(selectedConfig))
                    listBoxConfigs.SelectedIndex = index;
            }
            listBoxConfigs.EndUpdate();
            textBoxActiveConfiguration.Text = ClassFormConfig.ActiveConfiguration;
        }

        private void UpdateButtons()
        {
            bool itemIsSelected = (listBoxConfigs.SelectedIndex > -1);
            buttonApply.Enabled = itemIsSelected;
            buttonRename.Enabled = itemIsSelected;
            buttonDelete.Enabled = itemIsSelected;
        }

        private void listBoxConfigs_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            ClassFormConfig.ApplyConfiguration((string)listBoxConfigs.SelectedItem);
            textBoxActiveConfiguration.Text = ClassFormConfig.ActiveConfiguration;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormConfigurationName formGetName = new FormConfigurationName();
            string configName = formGetName.GetConfigName(this);
            if (String.IsNullOrEmpty(configName))
                return;

            ClassFormConfig.SaveNewConfiguration(configName);
            ShowConfigurations();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ClassFormConfig.SaveActiveConfiguration();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty((string)listBoxConfigs.SelectedItem))
                return;
            if (ClassFormConfig.DeleteConfiguration((string)listBoxConfigs.SelectedItem))
                ShowConfigurations();
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            if (listBoxConfigs.SelectedIndex < 0)
                return;
            FormConfigurationName formGetName = new FormConfigurationName();
            string configName = formGetName.GetConfigName(this,(string)listBoxConfigs.SelectedItem);
            if (String.IsNullOrEmpty(configName))
                return;
            if (ClassFormConfig.RenameConfiguration((string)listBoxConfigs.SelectedItem, configName))
                ShowConfigurations(configName);
        }

        private bool FormIsOnScreen(Form form)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                if (screen.WorkingArea.Contains(new Rectangle(form.Left, form.Top, form.Width, form.Height)))
                    return true;
            }
            return false;
        }

        public void SideLoadToForm(Form FormToSideLoadTo)
        {
            // Move the window so that it appears to the side of the specified window
            try
            {
                // Try placing on the right first of all
                this.Location = new Point(FormToSideLoadTo.Left + FormToSideLoadTo.Width, FormToSideLoadTo.Top);
                if (FormIsOnScreen(this))
                    return;

                // Form was off the screen, so try to the left
                this.Location = new Point(FormToSideLoadTo.Left - this.Width, FormToSideLoadTo.Top);
                if (FormIsOnScreen(this))
                    return;

                // Still off the screen, so we'll just overlay the parent window
                this.Location = FormToSideLoadTo.Location;
            }
            catch { }
        }
    }
}
