namespace SOAPe.Auth
{
    partial class FormAzureApplicationRegistration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAzureApplicationRegistration));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxAuthenticationUrl = new System.Windows.Forms.ComboBox();
            this.comboBoxResourceUrl = new System.Windows.Forms.ComboBox();
            this.textBoxTenantId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxApplicationId = new System.Windows.Forms.TextBox();
            this.textBoxRedirectUrl = new System.Windows.Forms.TextBox();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.radioButtonAuthAsNativeApp = new System.Windows.Forms.RadioButton();
            this.textBoxAuthCertificate = new System.Windows.Forms.TextBox();
            this.buttonLoadCertificate = new System.Windows.Forms.Button();
            this.radioButtonAuthWithCertificate = new System.Windows.Forms.RadioButton();
            this.radioButtonAuthWithClientSecret = new System.Windows.Forms.RadioButton();
            this.textBoxClientSecret = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonManageTokens = new System.Windows.Forms.Button();
            this.buttonAcquireToken = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonAppConsent = new System.Windows.Forms.Button();
            this.buttonUserConsent = new System.Windows.Forms.Button();
            this.buttonRegisterApplication = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxAuthenticationUrl);
            this.groupBox2.Controls.Add(this.comboBoxResourceUrl);
            this.groupBox2.Controls.Add(this.textBoxTenantId);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxApplicationId);
            this.groupBox2.Controls.Add(this.textBoxRedirectUrl);
            this.groupBox2.Location = new System.Drawing.Point(12, 11);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(535, 159);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "NoConfigSave";
            this.groupBox2.Text = "Application Information";
            // 
            // comboBoxAuthenticationUrl
            // 
            this.comboBoxAuthenticationUrl.FormattingEnabled = true;
            this.comboBoxAuthenticationUrl.Items.AddRange(new object[] {
            "https://login.microsoftonline.com/common",
            "https://login.microsoftonline.com/<tenant>",
            "https://login.windows.net/common"});
            this.comboBoxAuthenticationUrl.Location = new System.Drawing.Point(123, 102);
            this.comboBoxAuthenticationUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxAuthenticationUrl.Name = "comboBoxAuthenticationUrl";
            this.comboBoxAuthenticationUrl.Size = new System.Drawing.Size(403, 21);
            this.comboBoxAuthenticationUrl.TabIndex = 26;
            // 
            // comboBoxResourceUrl
            // 
            this.comboBoxResourceUrl.FormattingEnabled = true;
            this.comboBoxResourceUrl.Items.AddRange(new object[] {
            "https://outlook.office365.com",
            "https://graph.microsoft.com",
            "https://manage.office.com",
            "https://graph.windowsazure.net"});
            this.comboBoxResourceUrl.Location = new System.Drawing.Point(123, 47);
            this.comboBoxResourceUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxResourceUrl.Name = "comboBoxResourceUrl";
            this.comboBoxResourceUrl.Size = new System.Drawing.Size(403, 21);
            this.comboBoxResourceUrl.TabIndex = 25;
            this.comboBoxResourceUrl.Text = "https://outlook.office365.com";
            // 
            // textBoxTenantId
            // 
            this.textBoxTenantId.Location = new System.Drawing.Point(123, 20);
            this.textBoxTenantId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxTenantId.Name = "textBoxTenantId";
            this.textBoxTenantId.Size = new System.Drawing.Size(403, 20);
            this.textBoxTenantId.TabIndex = 20;
            this.textBoxTenantId.TextChanged += new System.EventHandler(this.textBoxTenantId_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 133);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Application ID*:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 15;
            this.label2.Tag = "";
            this.label2.Text = "Tenant ID*:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Resource Url*:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 78);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Redirect Url:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 105);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Authentication Url*:";
            // 
            // textBoxApplicationId
            // 
            this.textBoxApplicationId.Location = new System.Drawing.Point(123, 130);
            this.textBoxApplicationId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxApplicationId.Name = "textBoxApplicationId";
            this.textBoxApplicationId.Size = new System.Drawing.Size(403, 20);
            this.textBoxApplicationId.TabIndex = 21;
            this.textBoxApplicationId.Text = "4a03b746-45be-488c-bfe5-0ffdac557d68";
            // 
            // textBoxRedirectUrl
            // 
            this.textBoxRedirectUrl.Location = new System.Drawing.Point(123, 75);
            this.textBoxRedirectUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxRedirectUrl.Name = "textBoxRedirectUrl";
            this.textBoxRedirectUrl.Size = new System.Drawing.Size(403, 20);
            this.textBoxRedirectUrl.TabIndex = 23;
            this.textBoxRedirectUrl.Text = "http://localhost/SOAPe";
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Controls.Add(this.radioButtonAuthAsNativeApp);
            this.groupBoxAuth.Controls.Add(this.textBoxAuthCertificate);
            this.groupBoxAuth.Controls.Add(this.buttonLoadCertificate);
            this.groupBoxAuth.Controls.Add(this.radioButtonAuthWithCertificate);
            this.groupBoxAuth.Controls.Add(this.radioButtonAuthWithClientSecret);
            this.groupBoxAuth.Controls.Add(this.textBoxClientSecret);
            this.groupBoxAuth.Location = new System.Drawing.Point(12, 173);
            this.groupBoxAuth.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.groupBoxAuth.Size = new System.Drawing.Size(535, 87);
            this.groupBoxAuth.TabIndex = 40;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Tag = "NoConfigSave";
            this.groupBoxAuth.Text = "Authentication";
            // 
            // radioButtonAuthAsNativeApp
            // 
            this.radioButtonAuthAsNativeApp.AutoSize = true;
            this.radioButtonAuthAsNativeApp.Checked = true;
            this.radioButtonAuthAsNativeApp.Location = new System.Drawing.Point(7, 16);
            this.radioButtonAuthAsNativeApp.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonAuthAsNativeApp.Name = "radioButtonAuthAsNativeApp";
            this.radioButtonAuthAsNativeApp.Size = new System.Drawing.Size(384, 17);
            this.radioButtonAuthAsNativeApp.TabIndex = 32;
            this.radioButtonAuthAsNativeApp.TabStop = true;
            this.radioButtonAuthAsNativeApp.Tag = "NoTextSave";
            this.radioButtonAuthAsNativeApp.Text = "As native application (user prompted to log-in, will trigger consent if required)" +
    ")";
            this.radioButtonAuthAsNativeApp.UseVisualStyleBackColor = true;
            this.radioButtonAuthAsNativeApp.CheckedChanged += new System.EventHandler(this.radioButtonAuthAsNativeApp_CheckedChanged);
            // 
            // textBoxAuthCertificate
            // 
            this.textBoxAuthCertificate.Location = new System.Drawing.Point(96, 58);
            this.textBoxAuthCertificate.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.textBoxAuthCertificate.Name = "textBoxAuthCertificate";
            this.textBoxAuthCertificate.Size = new System.Drawing.Size(375, 20);
            this.textBoxAuthCertificate.TabIndex = 31;
            // 
            // buttonLoadCertificate
            // 
            this.buttonLoadCertificate.Location = new System.Drawing.Point(474, 56);
            this.buttonLoadCertificate.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.buttonLoadCertificate.Name = "buttonLoadCertificate";
            this.buttonLoadCertificate.Size = new System.Drawing.Size(50, 21);
            this.buttonLoadCertificate.TabIndex = 30;
            this.buttonLoadCertificate.Text = "Select...";
            this.buttonLoadCertificate.UseVisualStyleBackColor = true;
            // 
            // radioButtonAuthWithCertificate
            // 
            this.radioButtonAuthWithCertificate.AutoSize = true;
            this.radioButtonAuthWithCertificate.Enabled = false;
            this.radioButtonAuthWithCertificate.Location = new System.Drawing.Point(7, 58);
            this.radioButtonAuthWithCertificate.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.radioButtonAuthWithCertificate.Name = "radioButtonAuthWithCertificate";
            this.radioButtonAuthWithCertificate.Size = new System.Drawing.Size(75, 17);
            this.radioButtonAuthWithCertificate.TabIndex = 28;
            this.radioButtonAuthWithCertificate.Tag = "NoTextSave";
            this.radioButtonAuthWithCertificate.Text = "Certificate:";
            this.radioButtonAuthWithCertificate.UseVisualStyleBackColor = true;
            this.radioButtonAuthWithCertificate.CheckedChanged += new System.EventHandler(this.radioButtonAuthWithCertificate_CheckedChanged);
            // 
            // radioButtonAuthWithClientSecret
            // 
            this.radioButtonAuthWithClientSecret.AutoSize = true;
            this.radioButtonAuthWithClientSecret.Enabled = false;
            this.radioButtonAuthWithClientSecret.Location = new System.Drawing.Point(7, 37);
            this.radioButtonAuthWithClientSecret.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.radioButtonAuthWithClientSecret.Name = "radioButtonAuthWithClientSecret";
            this.radioButtonAuthWithClientSecret.Size = new System.Drawing.Size(86, 17);
            this.radioButtonAuthWithClientSecret.TabIndex = 0;
            this.radioButtonAuthWithClientSecret.Tag = "NoTextSave";
            this.radioButtonAuthWithClientSecret.Text = "Client secret:";
            this.radioButtonAuthWithClientSecret.UseVisualStyleBackColor = true;
            this.radioButtonAuthWithClientSecret.CheckedChanged += new System.EventHandler(this.radioButtonAuthWithClientSecret_CheckedChanged);
            // 
            // textBoxClientSecret
            // 
            this.textBoxClientSecret.Location = new System.Drawing.Point(96, 36);
            this.textBoxClientSecret.Name = "textBoxClientSecret";
            this.textBoxClientSecret.Size = new System.Drawing.Size(429, 20);
            this.textBoxClientSecret.TabIndex = 27;
            this.textBoxClientSecret.UseSystemPasswordChar = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonManageTokens);
            this.groupBox5.Controls.Add(this.buttonAcquireToken);
            this.groupBox5.Location = new System.Drawing.Point(12, 309);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(238, 42);
            this.groupBox5.TabIndex = 48;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tokens";
            // 
            // buttonManageTokens
            // 
            this.buttonManageTokens.Location = new System.Drawing.Point(104, 16);
            this.buttonManageTokens.Margin = new System.Windows.Forms.Padding(2);
            this.buttonManageTokens.Name = "buttonManageTokens";
            this.buttonManageTokens.Size = new System.Drawing.Size(119, 22);
            this.buttonManageTokens.TabIndex = 38;
            this.buttonManageTokens.Text = "Manage tokens...";
            this.buttonManageTokens.UseVisualStyleBackColor = true;
            this.buttonManageTokens.Click += new System.EventHandler(this.buttonManageTokens_Click);
            // 
            // buttonAcquireToken
            // 
            this.buttonAcquireToken.Location = new System.Drawing.Point(7, 16);
            this.buttonAcquireToken.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAcquireToken.Name = "buttonAcquireToken";
            this.buttonAcquireToken.Size = new System.Drawing.Size(92, 22);
            this.buttonAcquireToken.TabIndex = 37;
            this.buttonAcquireToken.Text = "Acquire token";
            this.buttonAcquireToken.UseVisualStyleBackColor = true;
            this.buttonAcquireToken.Click += new System.EventHandler(this.buttonAcquireToken_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonAppConsent);
            this.groupBox4.Controls.Add(this.buttonUserConsent);
            this.groupBox4.Location = new System.Drawing.Point(12, 263);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(238, 42);
            this.groupBox4.TabIndex = 47;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Consent";
            // 
            // buttonAppConsent
            // 
            this.buttonAppConsent.Enabled = false;
            this.buttonAppConsent.Location = new System.Drawing.Point(7, 16);
            this.buttonAppConsent.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAppConsent.Name = "buttonAppConsent";
            this.buttonAppConsent.Size = new System.Drawing.Size(124, 22);
            this.buttonAppConsent.TabIndex = 36;
            this.buttonAppConsent.Text = "Application Consent...";
            this.buttonAppConsent.UseVisualStyleBackColor = true;
            // 
            // buttonUserConsent
            // 
            this.buttonUserConsent.Enabled = false;
            this.buttonUserConsent.Location = new System.Drawing.Point(135, 16);
            this.buttonUserConsent.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUserConsent.Name = "buttonUserConsent";
            this.buttonUserConsent.Size = new System.Drawing.Size(92, 22);
            this.buttonUserConsent.TabIndex = 35;
            this.buttonUserConsent.Text = "User Consent...";
            this.buttonUserConsent.UseVisualStyleBackColor = true;
            // 
            // buttonRegisterApplication
            // 
            this.buttonRegisterApplication.Enabled = false;
            this.buttonRegisterApplication.Location = new System.Drawing.Point(392, 279);
            this.buttonRegisterApplication.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRegisterApplication.Name = "buttonRegisterApplication";
            this.buttonRegisterApplication.Size = new System.Drawing.Size(155, 21);
            this.buttonRegisterApplication.TabIndex = 49;
            this.buttonRegisterApplication.Text = "Register Application...";
            this.buttonRegisterApplication.UseVisualStyleBackColor = true;
            this.buttonRegisterApplication.Click += new System.EventHandler(this.buttonRegisterApplication_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(497, 331);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(50, 21);
            this.buttonClose.TabIndex = 50;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // FormAzureApplicationRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 360);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonRegisterApplication);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBoxAuth);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAzureApplicationRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Azure Application Registration";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxAuth.ResumeLayout(false);
            this.groupBoxAuth.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxAuthenticationUrl;
        private System.Windows.Forms.ComboBox comboBoxResourceUrl;
        private System.Windows.Forms.TextBox textBoxTenantId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxApplicationId;
        private System.Windows.Forms.TextBox textBoxRedirectUrl;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private System.Windows.Forms.TextBox textBoxAuthCertificate;
        private System.Windows.Forms.Button buttonLoadCertificate;
        private System.Windows.Forms.RadioButton radioButtonAuthWithCertificate;
        private System.Windows.Forms.RadioButton radioButtonAuthWithClientSecret;
        private System.Windows.Forms.TextBox textBoxClientSecret;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonManageTokens;
        private System.Windows.Forms.Button buttonAcquireToken;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonAppConsent;
        private System.Windows.Forms.Button buttonUserConsent;
        private System.Windows.Forms.Button buttonRegisterApplication;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.RadioButton radioButtonAuthAsNativeApp;
    }
}