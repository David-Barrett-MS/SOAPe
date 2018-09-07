namespace SOAPe.EWSTools
{
    partial class FormEWSQuickTest
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
            this.textBoxAutodiscoverEmail = new System.Windows.Forms.TextBox();
            this.textBoxEWSUrl = new System.Windows.Forms.TextBox();
            this.radioButtonUseEWSUrl = new System.Windows.Forms.RadioButton();
            this.radioButtonAutodiscover = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDomain = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.checkBoxInboxFindItems = new System.Windows.Forms.CheckBox();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxAutodiscoverEmail);
            this.groupBox1.Controls.Add(this.textBoxEWSUrl);
            this.groupBox1.Controls.Add(this.radioButtonUseEWSUrl);
            this.groupBox1.Controls.Add(this.radioButtonAutodiscover);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // textBoxAutodiscoverEmail
            // 
            this.textBoxAutodiscoverEmail.Location = new System.Drawing.Point(117, 18);
            this.textBoxAutodiscoverEmail.Name = "textBoxAutodiscoverEmail";
            this.textBoxAutodiscoverEmail.Size = new System.Drawing.Size(390, 20);
            this.textBoxAutodiscoverEmail.TabIndex = 3;
            this.textBoxAutodiscoverEmail.TextChanged += new System.EventHandler(this.textBoxAutodiscoverEmail_TextChanged);
            // 
            // textBoxEWSUrl
            // 
            this.textBoxEWSUrl.Location = new System.Drawing.Point(103, 41);
            this.textBoxEWSUrl.Name = "textBoxEWSUrl";
            this.textBoxEWSUrl.Size = new System.Drawing.Size(404, 20);
            this.textBoxEWSUrl.TabIndex = 2;
            // 
            // radioButtonUseEWSUrl
            // 
            this.radioButtonUseEWSUrl.AutoSize = true;
            this.radioButtonUseEWSUrl.Location = new System.Drawing.Point(6, 42);
            this.radioButtonUseEWSUrl.Name = "radioButtonUseEWSUrl";
            this.radioButtonUseEWSUrl.Size = new System.Drawing.Size(91, 17);
            this.radioButtonUseEWSUrl.TabIndex = 1;
            this.radioButtonUseEWSUrl.TabStop = true;
            this.radioButtonUseEWSUrl.Text = "Use this URL:";
            this.radioButtonUseEWSUrl.UseVisualStyleBackColor = true;
            this.radioButtonUseEWSUrl.CheckedChanged += new System.EventHandler(this.radioButtonUseEWSUrl_CheckedChanged);
            // 
            // radioButtonAutodiscover
            // 
            this.radioButtonAutodiscover.AutoSize = true;
            this.radioButtonAutodiscover.Location = new System.Drawing.Point(6, 19);
            this.radioButtonAutodiscover.Name = "radioButtonAutodiscover";
            this.radioButtonAutodiscover.Size = new System.Drawing.Size(105, 17);
            this.radioButtonAutodiscover.TabIndex = 0;
            this.radioButtonAutodiscover.TabStop = true;
            this.radioButtonAutodiscover.Text = "Autodiscover for:";
            this.radioButtonAutodiscover.UseVisualStyleBackColor = true;
            this.radioButtonAutodiscover.CheckedChanged += new System.EventHandler(this.radioButtonAutodiscover_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxDomain);
            this.groupBox2.Controls.Add(this.textBoxPassword);
            this.groupBox2.Controls.Add(this.textBoxUsername);
            this.groupBox2.Location = new System.Drawing.Point(12, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(513, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Authentication";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Domain:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username/email address:";
            // 
            // textBoxDomain
            // 
            this.textBoxDomain.Location = new System.Drawing.Point(139, 71);
            this.textBoxDomain.Name = "textBoxDomain";
            this.textBoxDomain.Size = new System.Drawing.Size(294, 20);
            this.textBoxDomain.TabIndex = 2;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(139, 45);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(294, 20);
            this.textBoxPassword.TabIndex = 1;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(139, 19);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(368, 20);
            this.textBoxUsername.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonTest);
            this.groupBox3.Controls.Add(this.checkBoxInboxFindItems);
            this.groupBox3.Location = new System.Drawing.Point(12, 197);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(513, 64);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tests";
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(432, 35);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 1;
            this.buttonTest.Text = "Start Test(s)";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // checkBoxInboxFindItems
            // 
            this.checkBoxInboxFindItems.AutoSize = true;
            this.checkBoxInboxFindItems.Location = new System.Drawing.Point(9, 19);
            this.checkBoxInboxFindItems.Name = "checkBoxInboxFindItems";
            this.checkBoxInboxFindItems.Size = new System.Drawing.Size(121, 17);
            this.checkBoxInboxFindItems.TabIndex = 0;
            this.checkBoxInboxFindItems.Text = "Retrieve inbox items";
            this.checkBoxInboxFindItems.UseVisualStyleBackColor = true;
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(12, 267);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(513, 173);
            this.listBoxLog.TabIndex = 3;
            // 
            // FormEWSQuickTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 453);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormEWSQuickTest";
            this.Text = "FormEWSQuickTest";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxEWSUrl;
        private System.Windows.Forms.RadioButton radioButtonUseEWSUrl;
        private System.Windows.Forms.RadioButton radioButtonAutodiscover;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDomain;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.CheckBox checkBoxInboxFindItems;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.TextBox textBoxAutodiscoverEmail;
    }
}