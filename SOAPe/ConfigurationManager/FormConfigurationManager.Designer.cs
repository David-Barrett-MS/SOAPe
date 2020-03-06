namespace SOAPe.ConfigurationManager
{
    partial class FormConfigurationManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxConfigs = new System.Windows.Forms.ListBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxActiveConfiguration = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxConfigs);
            this.groupBox1.Location = new System.Drawing.Point(18, 88);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(488, 560);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Saved configurations";
            // 
            // listBoxConfigs
            // 
            this.listBoxConfigs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxConfigs.FormattingEnabled = true;
            this.listBoxConfigs.ItemHeight = 20;
            this.listBoxConfigs.Items.AddRange(new object[] {
            "Default"});
            this.listBoxConfigs.Location = new System.Drawing.Point(4, 24);
            this.listBoxConfigs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxConfigs.Name = "listBoxConfigs";
            this.listBoxConfigs.Size = new System.Drawing.Size(480, 531);
            this.listBoxConfigs.Sorted = true;
            this.listBoxConfigs.TabIndex = 0;
            this.listBoxConfigs.SelectedIndexChanged += new System.EventHandler(this.listBoxConfigs_SelectedIndexChanged);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(514, 97);
            this.buttonApply.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(112, 35);
            this.buttonApply.TabIndex = 1;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonRename
            // 
            this.buttonRename.Location = new System.Drawing.Point(514, 142);
            this.buttonRename.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(112, 35);
            this.buttonRename.TabIndex = 2;
            this.buttonRename.Text = "Rename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(514, 186);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(112, 35);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(514, 612);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(112, 35);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(514, 231);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(112, 35);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSave);
            this.groupBox2.Controls.Add(this.textBoxActiveConfiguration);
            this.groupBox2.Location = new System.Drawing.Point(22, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(604, 62);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Active configuration";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(484, 22);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(112, 35);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxActiveConfiguration
            // 
            this.textBoxActiveConfiguration.Location = new System.Drawing.Point(6, 25);
            this.textBoxActiveConfiguration.Name = "textBoxActiveConfiguration";
            this.textBoxActiveConfiguration.ReadOnly = true;
            this.textBoxActiveConfiguration.Size = new System.Drawing.Size(469, 26);
            this.textBoxActiveConfiguration.TabIndex = 0;
            this.textBoxActiveConfiguration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormConfigurationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 668);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfigurationManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Configuration Manager";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxConfigs;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxActiveConfiguration;
        private System.Windows.Forms.Button buttonSave;
    }
}