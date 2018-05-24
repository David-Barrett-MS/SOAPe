namespace SOAPe.EWSTools
{
    partial class FormEWSConvertID
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
            this.comboBoxSourceFormat = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSourceID = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.textBoxConvertedID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTargetFormat = new System.Windows.Forms.ComboBox();
            this.textBoxMailbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxSourceFormat
            // 
            this.comboBoxSourceFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSourceFormat.FormattingEnabled = true;
            this.comboBoxSourceFormat.Location = new System.Drawing.Point(327, 45);
            this.comboBoxSourceFormat.Name = "comboBoxSourceFormat";
            this.comboBoxSourceFormat.Size = new System.Drawing.Size(137, 21);
            this.comboBoxSourceFormat.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxMailbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxSourceID);
            this.groupBox1.Controls.Add(this.comboBoxSourceFormat);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(279, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Format:";
            // 
            // textBoxSourceID
            // 
            this.textBoxSourceID.Location = new System.Drawing.Point(6, 19);
            this.textBoxSourceID.Name = "textBoxSourceID";
            this.textBoxSourceID.Size = new System.Drawing.Size(458, 20);
            this.textBoxSourceID.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonConvert);
            this.groupBox2.Controls.Add(this.textBoxConvertedID);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.comboBoxTargetFormat);
            this.groupBox2.Location = new System.Drawing.Point(12, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Converted ID";
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(206, 17);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(75, 23);
            this.buttonConvert.TabIndex = 1;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // textBoxConvertedID
            // 
            this.textBoxConvertedID.Location = new System.Drawing.Point(6, 46);
            this.textBoxConvertedID.Name = "textBoxConvertedID";
            this.textBoxConvertedID.Size = new System.Drawing.Size(456, 20);
            this.textBoxConvertedID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Format:";
            // 
            // comboBoxTargetFormat
            // 
            this.comboBoxTargetFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetFormat.FormattingEnabled = true;
            this.comboBoxTargetFormat.Location = new System.Drawing.Point(54, 19);
            this.comboBoxTargetFormat.Name = "comboBoxTargetFormat";
            this.comboBoxTargetFormat.Size = new System.Drawing.Size(137, 21);
            this.comboBoxTargetFormat.TabIndex = 0;
            // 
            // textBoxMailbox
            // 
            this.textBoxMailbox.Location = new System.Drawing.Point(58, 72);
            this.textBoxMailbox.Name = "textBoxMailbox";
            this.textBoxMailbox.Size = new System.Drawing.Size(406, 20);
            this.textBoxMailbox.TabIndex = 3;
            this.textBoxMailbox.Text = "me@me.local";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Mailbox:";
            // 
            // FormEWSConvertID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 238);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEWSConvertID";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exchange Web Services: ConvertID";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSourceFormat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSourceID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.TextBox textBoxConvertedID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxTargetFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMailbox;
    }
}