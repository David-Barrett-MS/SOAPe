namespace SOAPe
{
    partial class FormListener
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormListener));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonEditListenUrl = new System.Windows.Forms.Button();
            this.textBoxListenerURi = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBoxReceivedShowing = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.hScrollBarReceived = new System.Windows.Forms.HScrollBar();
            this.buttonClearEvents = new System.Windows.Forms.Button();
            this.xmlEditor1 = new SOAPe.XmlEditor();
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
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.buttonEditListenUrl);
            this.groupBox1.Controls.Add(this.textBoxListenerURi);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(608, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listener URL";
            // 
            // buttonEditListenUrl
            // 
            this.buttonEditListenUrl.Enabled = false;
            this.buttonEditListenUrl.Location = new System.Drawing.Point(527, 20);
            this.buttonEditListenUrl.Name = "buttonEditListenUrl";
            this.buttonEditListenUrl.Size = new System.Drawing.Size(75, 24);
            this.buttonEditListenUrl.TabIndex = 1;
            this.buttonEditListenUrl.Text = "Edit...";
            this.buttonEditListenUrl.UseVisualStyleBackColor = true;
            // 
            // textBoxListenerURi
            // 
            this.textBoxListenerURi.Location = new System.Drawing.Point(6, 22);
            this.textBoxListenerURi.Name = "textBoxListenerURi";
            this.textBoxListenerURi.ReadOnly = true;
            this.textBoxListenerURi.Size = new System.Drawing.Size(520, 20);
            this.textBoxListenerURi.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.splitContainer1);
            this.groupBox2.Location = new System.Drawing.Point(12, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(609, 266);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Received";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBoxReceivedShowing);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.hScrollBarReceived);
            this.splitContainer1.Panel1.Controls.Add(this.buttonClearEvents);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.xmlEditor1);
            this.splitContainer1.Size = new System.Drawing.Size(603, 247);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 6;
            // 
            // textBoxReceivedShowing
            // 
            this.textBoxReceivedShowing.Location = new System.Drawing.Point(3, 0);
            this.textBoxReceivedShowing.Name = "textBoxReceivedShowing";
            this.textBoxReceivedShowing.Size = new System.Drawing.Size(104, 20);
            this.textBoxReceivedShowing.TabIndex = 12;
            this.textBoxReceivedShowing.Text = "0 of 0";
            this.textBoxReceivedShowing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(532, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 20);
            this.button1.TabIndex = 11;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonClearEvents_Click);
            // 
            // hScrollBarReceived
            // 
            this.hScrollBarReceived.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBarReceived.LargeChange = 1;
            this.hScrollBarReceived.Location = new System.Drawing.Point(108, -1);
            this.hScrollBarReceived.Maximum = 10;
            this.hScrollBarReceived.Name = "hScrollBarReceived";
            this.hScrollBarReceived.Size = new System.Drawing.Size(424, 20);
            this.hScrollBarReceived.TabIndex = 10;
            this.hScrollBarReceived.ValueChanged += new System.EventHandler(this.hScrollBarReceived_ValueChanged);
            // 
            // buttonClearEvents
            // 
            this.buttonClearEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearEvents.Location = new System.Drawing.Point(758, 15);
            this.buttonClearEvents.Name = "buttonClearEvents";
            this.buttonClearEvents.Size = new System.Drawing.Size(67, 20);
            this.buttonClearEvents.TabIndex = 8;
            this.buttonClearEvents.Text = "Clear";
            this.buttonClearEvents.UseVisualStyleBackColor = true;
            // 
            // xmlEditor1
            // 
            this.xmlEditor1.BackColor = System.Drawing.SystemColors.Window;
            this.xmlEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmlEditor1.IndentXml = true;
            this.xmlEditor1.Location = new System.Drawing.Point(0, 0);
            this.xmlEditor1.Name = "xmlEditor1";
            this.xmlEditor1.ReadOnly = false;
            this.xmlEditor1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2057{\\fonttbl{\\f0\\fnil\\fcharset0 " +
    "Microsoft Sans Serif;}}\r\n{\\*\\generator Riched20 10.0.18362}\\viewkind4\\uc1 \r\n\\par" +
    "d\\f0\\fs17\\par\r\n}\r\n";
            this.xmlEditor1.SelectionLength = 0;
            this.xmlEditor1.SelectionStart = 0;
            this.xmlEditor1.SendItemIdToTemplateEnabled = false;
            this.xmlEditor1.Size = new System.Drawing.Size(603, 218);
            this.xmlEditor1.SyntaxHighlight = true;
            this.xmlEditor1.TabIndex = 0;
            // 
            // FormListener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 351);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormListener";
            this.Text = "HTTP Listener";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormListener_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxListenerURi;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.HScrollBar hScrollBarReceived;
        private System.Windows.Forms.Button buttonClearEvents;
        private XmlEditor xmlEditor1;
        private System.Windows.Forms.TextBox textBoxReceivedShowing;
        private System.Windows.Forms.Button buttonEditListenUrl;
    }
}