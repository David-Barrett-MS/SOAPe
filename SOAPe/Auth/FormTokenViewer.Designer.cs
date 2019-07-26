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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTokenViewer));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxTokens = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonViewAsJSON = new System.Windows.Forms.RadioButton();
            this.radioButtonViewRaw = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBoxTokenInfo = new System.Windows.Forms.RichTextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxTokenElementToShow = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.listBoxTokens);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(512, 161);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tokens currently in cache";
            // 
            // listBoxTokens
            // 
            this.listBoxTokens.DisplayMember = "DisplayableId";
            this.listBoxTokens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTokens.FormattingEnabled = true;
            this.listBoxTokens.Location = new System.Drawing.Point(2, 15);
            this.listBoxTokens.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBoxTokens.Name = "listBoxTokens";
            this.listBoxTokens.Size = new System.Drawing.Size(508, 144);
            this.listBoxTokens.TabIndex = 0;
            this.listBoxTokens.ValueMember = "DisplayableId";
            this.listBoxTokens.SelectedIndexChanged += new System.EventHandler(this.listBoxTokens_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.splitContainer1);
            this.groupBox2.Location = new System.Drawing.Point(8, 172);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(512, 250);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected token";
            // 
            // radioButtonViewAsJSON
            // 
            this.radioButtonViewAsJSON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonViewAsJSON.AutoSize = true;
            this.radioButtonViewAsJSON.Location = new System.Drawing.Point(280, 214);
            this.radioButtonViewAsJSON.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonViewAsJSON.Name = "radioButtonViewAsJSON";
            this.radioButtonViewAsJSON.Size = new System.Drawing.Size(53, 17);
            this.radioButtonViewAsJSON.TabIndex = 5;
            this.radioButtonViewAsJSON.Text = "JSON";
            this.radioButtonViewAsJSON.UseVisualStyleBackColor = true;
            this.radioButtonViewAsJSON.CheckedChanged += new System.EventHandler(this.radioButtonViewAsJSON_CheckedChanged);
            // 
            // radioButtonViewRaw
            // 
            this.radioButtonViewRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonViewRaw.AutoSize = true;
            this.radioButtonViewRaw.Checked = true;
            this.radioButtonViewRaw.Location = new System.Drawing.Point(236, 214);
            this.radioButtonViewRaw.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonViewRaw.Name = "radioButtonViewRaw";
            this.radioButtonViewRaw.Size = new System.Drawing.Size(42, 17);
            this.radioButtonViewRaw.TabIndex = 4;
            this.radioButtonViewRaw.TabStop = true;
            this.radioButtonViewRaw.Text = "raw";
            this.radioButtonViewRaw.UseVisualStyleBackColor = true;
            this.radioButtonViewRaw.CheckedChanged += new System.EventHandler(this.radioButtonViewRaw_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 215);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "View as:";
            // 
            // richTextBoxTokenInfo
            // 
            this.richTextBoxTokenInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxTokenInfo.Location = new System.Drawing.Point(2, 2);
            this.richTextBoxTokenInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBoxTokenInfo.Name = "richTextBoxTokenInfo";
            this.richTextBoxTokenInfo.Size = new System.Drawing.Size(331, 208);
            this.richTextBoxTokenInfo.TabIndex = 0;
            this.richTextBoxTokenInfo.Text = "";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonClose.Location = new System.Drawing.Point(241, 426);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(50, 22);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(2, 15);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxTokenElementToShow);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBoxTokenInfo);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonViewAsJSON);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonViewRaw);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(508, 233);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.TabIndex = 6;
            // 
            // listBoxTokenElementToShow
            // 
            this.listBoxTokenElementToShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTokenElementToShow.FormattingEnabled = true;
            this.listBoxTokenElementToShow.Items.AddRange(new object[] {
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
            this.listBoxTokenElementToShow.Location = new System.Drawing.Point(0, 0);
            this.listBoxTokenElementToShow.Name = "listBoxTokenElementToShow";
            this.listBoxTokenElementToShow.Size = new System.Drawing.Size(169, 233);
            this.listBoxTokenElementToShow.TabIndex = 0;
            this.listBoxTokenElementToShow.SelectedIndexChanged += new System.EventHandler(this.ListBoxTokenElementToShow_SelectedIndexChanged);
            // 
            // FormTokenViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 454);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormTokenViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Token Viewer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxTokens;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBoxTokenInfo;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.RadioButton radioButtonViewAsJSON;
        private System.Windows.Forms.RadioButton radioButtonViewRaw;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxTokenElementToShow;
    }
}