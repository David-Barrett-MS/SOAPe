namespace SOAPe.EWSTools
{
    partial class FormEWSAutodiscover
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
            this.textBoxSMTP = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxSkipSCPAutodiscover = new System.Windows.Forms.CheckBox();
            this.buttonAutodiscover = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.checkBoxIgnoreCertificateErrors = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxSMTP
            // 
            this.textBoxSMTP.Location = new System.Drawing.Point(90, 19);
            this.textBoxSMTP.Name = "textBoxSMTP";
            this.textBoxSMTP.Size = new System.Drawing.Size(431, 20);
            this.textBoxSMTP.TabIndex = 4;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(458, 424);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBoxSkipSCPAutodiscover);
            this.groupBox1.Controls.Add(this.buttonAutodiscover);
            this.groupBox1.Controls.Add(this.textBoxSMTP);
            this.groupBox1.Controls.Add(this.checkBoxIgnoreCertificateErrors);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 78);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Autodiscover Options";
            // 
            // checkBoxSkipSCPAutodiscover
            // 
            this.checkBoxSkipSCPAutodiscover.AutoSize = true;
            this.checkBoxSkipSCPAutodiscover.Location = new System.Drawing.Point(289, 49);
            this.checkBoxSkipSCPAutodiscover.Name = "checkBoxSkipSCPAutodiscover";
            this.checkBoxSkipSCPAutodiscover.Size = new System.Drawing.Size(136, 17);
            this.checkBoxSkipSCPAutodiscover.TabIndex = 18;
            this.checkBoxSkipSCPAutodiscover.Text = "Skip SCP Autodiscover";
            this.checkBoxSkipSCPAutodiscover.UseVisualStyleBackColor = true;
            // 
            // buttonAutodiscover
            // 
            this.buttonAutodiscover.Location = new System.Drawing.Point(431, 45);
            this.buttonAutodiscover.Name = "buttonAutodiscover";
            this.buttonAutodiscover.Size = new System.Drawing.Size(90, 23);
            this.buttonAutodiscover.TabIndex = 0;
            this.buttonAutodiscover.Text = "Autodiscover";
            this.buttonAutodiscover.UseVisualStyleBackColor = true;
            this.buttonAutodiscover.Click += new System.EventHandler(this.buttonAutodiscover_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(3, 103);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(530, 316);
            this.listBoxLog.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Log:";
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(3, 424);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(75, 23);
            this.buttonClearLog.TabIndex = 5;
            this.buttonClearLog.Text = "Clear Log";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // checkBoxIgnoreCertificateErrors
            // 
            this.checkBoxIgnoreCertificateErrors.AutoSize = true;
            this.checkBoxIgnoreCertificateErrors.Location = new System.Drawing.Point(149, 49);
            this.checkBoxIgnoreCertificateErrors.Name = "checkBoxIgnoreCertificateErrors";
            this.checkBoxIgnoreCertificateErrors.Size = new System.Drawing.Size(134, 17);
            this.checkBoxIgnoreCertificateErrors.TabIndex = 4;
            this.checkBoxIgnoreCertificateErrors.Text = "Ignore certificate errors";
            this.checkBoxIgnoreCertificateErrors.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Email address:";
            // 
            // FormEWSAutodiscover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 452);
            this.Controls.Add(this.buttonClearLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEWSAutodiscover";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exchange Web Services Autodiscover";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEWSAutodiscover_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSMTP;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.CheckBox checkBoxIgnoreCertificateErrors;
        private System.Windows.Forms.Button buttonAutodiscover;
        private System.Windows.Forms.CheckBox checkBoxSkipSCPAutodiscover;
        private System.Windows.Forms.Label label2;
    }
}