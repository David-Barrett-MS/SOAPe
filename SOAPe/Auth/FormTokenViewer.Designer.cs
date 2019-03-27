namespace SOAPe.Auth
{
    partial class FormTokenViewer
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
            this.listBoxTokens = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBoxTokenInfo = new System.Windows.Forms.RichTextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxTokenElementToShow = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonViewRaw = new System.Windows.Forms.RadioButton();
            this.radioButtonViewAsJSON = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.listBoxTokens);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(757, 247);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tokens currently in cache";
            // 
            // listBoxTokens
            // 
            this.listBoxTokens.DisplayMember = "DisplayableId";
            this.listBoxTokens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTokens.FormattingEnabled = true;
            this.listBoxTokens.ItemHeight = 20;
            this.listBoxTokens.Location = new System.Drawing.Point(3, 22);
            this.listBoxTokens.Name = "listBoxTokens";
            this.listBoxTokens.Size = new System.Drawing.Size(751, 222);
            this.listBoxTokens.TabIndex = 0;
            this.listBoxTokens.ValueMember = "DisplayableId";
            this.listBoxTokens.SelectedIndexChanged += new System.EventHandler(this.listBoxTokens_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.radioButtonViewAsJSON);
            this.groupBox2.Controls.Add(this.radioButtonViewRaw);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.comboBoxTokenElementToShow);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.richTextBoxTokenInfo);
            this.groupBox2.Location = new System.Drawing.Point(12, 265);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(757, 367);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected token";
            // 
            // richTextBoxTokenInfo
            // 
            this.richTextBoxTokenInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxTokenInfo.Location = new System.Drawing.Point(3, 67);
            this.richTextBoxTokenInfo.Name = "richTextBoxTokenInfo";
            this.richTextBoxTokenInfo.Size = new System.Drawing.Size(748, 294);
            this.richTextBoxTokenInfo.TabIndex = 0;
            this.richTextBoxTokenInfo.Text = "";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonClose.Location = new System.Drawing.Point(355, 638);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 34);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Show:";
            // 
            // comboBoxTokenElementToShow
            // 
            this.comboBoxTokenElementToShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTokenElementToShow.FormattingEnabled = true;
            this.comboBoxTokenElementToShow.Items.AddRange(new object[] {
            "Access Token",
            "Authority",
            "Client Id",
            "Displayable Id",
            "Expires On",
            "Family Name",
            "Given Name",
            "Identity Provider",
            "IdToken",
            "Resource",
            "Tenant Id",
            "Unique Id"});
            this.comboBoxTokenElementToShow.Location = new System.Drawing.Point(65, 25);
            this.comboBoxTokenElementToShow.Name = "comboBoxTokenElementToShow";
            this.comboBoxTokenElementToShow.Size = new System.Drawing.Size(204, 28);
            this.comboBoxTokenElementToShow.TabIndex = 2;
            this.comboBoxTokenElementToShow.SelectedIndexChanged += new System.EventHandler(this.comboBoxTokenElementToShow_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "View as:";
            // 
            // radioButtonViewRaw
            // 
            this.radioButtonViewRaw.AutoSize = true;
            this.radioButtonViewRaw.Checked = true;
            this.radioButtonViewRaw.Location = new System.Drawing.Point(374, 26);
            this.radioButtonViewRaw.Name = "radioButtonViewRaw";
            this.radioButtonViewRaw.Size = new System.Drawing.Size(59, 24);
            this.radioButtonViewRaw.TabIndex = 4;
            this.radioButtonViewRaw.TabStop = true;
            this.radioButtonViewRaw.Text = "raw";
            this.radioButtonViewRaw.UseVisualStyleBackColor = true;
            this.radioButtonViewRaw.CheckedChanged += new System.EventHandler(this.radioButtonViewRaw_CheckedChanged);
            // 
            // radioButtonViewAsJSON
            // 
            this.radioButtonViewAsJSON.AutoSize = true;
            this.radioButtonViewAsJSON.Location = new System.Drawing.Point(439, 26);
            this.radioButtonViewAsJSON.Name = "radioButtonViewAsJSON";
            this.radioButtonViewAsJSON.Size = new System.Drawing.Size(76, 24);
            this.radioButtonViewAsJSON.TabIndex = 5;
            this.radioButtonViewAsJSON.Text = "JSON";
            this.radioButtonViewAsJSON.UseVisualStyleBackColor = true;
            this.radioButtonViewAsJSON.CheckedChanged += new System.EventHandler(this.radioButtonViewAsJSON_CheckedChanged);
            // 
            // FormTokenViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 682);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormTokenViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Token Viewer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxTokens;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBoxTokenInfo;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ComboBox comboBoxTokenElementToShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonViewAsJSON;
        private System.Windows.Forms.RadioButton radioButtonViewRaw;
        private System.Windows.Forms.Label label2;
    }
}