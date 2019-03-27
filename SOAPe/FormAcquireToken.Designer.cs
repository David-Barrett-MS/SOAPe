namespace SOAPe
{
    partial class FormAcquireToken
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
            this.textBoxAuthCertificate = new System.Windows.Forms.TextBox();
            this.buttonLoadCertificate = new System.Windows.Forms.Button();
            this.radioButtonAuthWithCertificate = new System.Windows.Forms.RadioButton();
            this.radioButtonAuthWithClientSecret = new System.Windows.Forms.RadioButton();
            this.textBoxClientSecret = new System.Windows.Forms.TextBox();
            this.radioButtonNativeApp = new System.Windows.Forms.RadioButton();
            this.buttonAcquireToken = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
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
            this.groupBox2.Size = new System.Drawing.Size(574, 214);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application Information";
            // 
            // comboBoxAuthenticationUrl
            // 
            this.comboBoxAuthenticationUrl.FormattingEnabled = true;
            this.comboBoxAuthenticationUrl.Items.AddRange(new object[] {
            "https://login.microsoftonline.com/common",
            "https://login.microsoftonline.com/<tenant>",
            "https://login.windows.net/common"});
            this.comboBoxAuthenticationUrl.Location = new System.Drawing.Point(161, 139);
            this.comboBoxAuthenticationUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxAuthenticationUrl.Name = "comboBoxAuthenticationUrl";
            this.comboBoxAuthenticationUrl.Size = new System.Drawing.Size(403, 28);
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
            this.comboBoxResourceUrl.Location = new System.Drawing.Point(161, 66);
            this.comboBoxResourceUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxResourceUrl.Name = "comboBoxResourceUrl";
            this.comboBoxResourceUrl.Size = new System.Drawing.Size(403, 28);
            this.comboBoxResourceUrl.TabIndex = 25;
            this.comboBoxResourceUrl.Text = "https://outlook.office365.com";
            // 
            // textBoxTenantId
            // 
            this.textBoxTenantId.Location = new System.Drawing.Point(161, 32);
            this.textBoxTenantId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxTenantId.Name = "textBoxTenantId";
            this.textBoxTenantId.Size = new System.Drawing.Size(403, 26);
            this.textBoxTenantId.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 180);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Application ID*:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Tenant ID*:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Resource Url*:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 105);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Redirect Url:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 141);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Authentication Url*:";
            // 
            // textBoxApplicationId
            // 
            this.textBoxApplicationId.Location = new System.Drawing.Point(161, 176);
            this.textBoxApplicationId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxApplicationId.Name = "textBoxApplicationId";
            this.textBoxApplicationId.Size = new System.Drawing.Size(403, 26);
            this.textBoxApplicationId.TabIndex = 21;
            this.textBoxApplicationId.Text = "4a03b746-45be-488c-bfe5-0ffdac557d68";
            // 
            // textBoxRedirectUrl
            // 
            this.textBoxRedirectUrl.Location = new System.Drawing.Point(161, 102);
            this.textBoxRedirectUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxRedirectUrl.Name = "textBoxRedirectUrl";
            this.textBoxRedirectUrl.Size = new System.Drawing.Size(403, 26);
            this.textBoxRedirectUrl.TabIndex = 23;
            this.textBoxRedirectUrl.Text = "http://localhost/SOAPe";
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Controls.Add(this.radioButtonNativeApp);
            this.groupBoxAuth.Controls.Add(this.textBoxAuthCertificate);
            this.groupBoxAuth.Controls.Add(this.buttonLoadCertificate);
            this.groupBoxAuth.Controls.Add(this.radioButtonAuthWithCertificate);
            this.groupBoxAuth.Controls.Add(this.radioButtonAuthWithClientSecret);
            this.groupBoxAuth.Controls.Add(this.textBoxClientSecret);
            this.groupBoxAuth.Location = new System.Drawing.Point(12, 229);
            this.groupBoxAuth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxAuth.Size = new System.Drawing.Size(574, 130);
            this.groupBoxAuth.TabIndex = 40;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Text = "Authentication";
            // 
            // textBoxAuthCertificate
            // 
            this.textBoxAuthCertificate.Location = new System.Drawing.Point(144, 58);
            this.textBoxAuthCertificate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAuthCertificate.Name = "textBoxAuthCertificate";
            this.textBoxAuthCertificate.Size = new System.Drawing.Size(339, 26);
            this.textBoxAuthCertificate.TabIndex = 31;
            // 
            // buttonLoadCertificate
            // 
            this.buttonLoadCertificate.Location = new System.Drawing.Point(489, 58);
            this.buttonLoadCertificate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLoadCertificate.Name = "buttonLoadCertificate";
            this.buttonLoadCertificate.Size = new System.Drawing.Size(75, 32);
            this.buttonLoadCertificate.TabIndex = 30;
            this.buttonLoadCertificate.Text = "Select...";
            this.buttonLoadCertificate.UseVisualStyleBackColor = true;
            // 
            // radioButtonAuthWithCertificate
            // 
            this.radioButtonAuthWithCertificate.AutoSize = true;
            this.radioButtonAuthWithCertificate.Location = new System.Drawing.Point(11, 59);
            this.radioButtonAuthWithCertificate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButtonAuthWithCertificate.Name = "radioButtonAuthWithCertificate";
            this.radioButtonAuthWithCertificate.Size = new System.Drawing.Size(110, 24);
            this.radioButtonAuthWithCertificate.TabIndex = 28;
            this.radioButtonAuthWithCertificate.Text = "Certificate:";
            this.radioButtonAuthWithCertificate.UseVisualStyleBackColor = true;
            // 
            // radioButtonAuthWithClientSecret
            // 
            this.radioButtonAuthWithClientSecret.AutoSize = true;
            this.radioButtonAuthWithClientSecret.Location = new System.Drawing.Point(11, 25);
            this.radioButtonAuthWithClientSecret.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButtonAuthWithClientSecret.Name = "radioButtonAuthWithClientSecret";
            this.radioButtonAuthWithClientSecret.Size = new System.Drawing.Size(126, 24);
            this.radioButtonAuthWithClientSecret.TabIndex = 0;
            this.radioButtonAuthWithClientSecret.Text = "Client secret:";
            this.radioButtonAuthWithClientSecret.UseVisualStyleBackColor = true;
            // 
            // textBoxClientSecret
            // 
            this.textBoxClientSecret.Location = new System.Drawing.Point(144, 24);
            this.textBoxClientSecret.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxClientSecret.Name = "textBoxClientSecret";
            this.textBoxClientSecret.Size = new System.Drawing.Size(420, 26);
            this.textBoxClientSecret.TabIndex = 27;
            this.textBoxClientSecret.UseSystemPasswordChar = true;
            // 
            // radioButtonNativeApp
            // 
            this.radioButtonNativeApp.AutoSize = true;
            this.radioButtonNativeApp.Checked = true;
            this.radioButtonNativeApp.Location = new System.Drawing.Point(11, 93);
            this.radioButtonNativeApp.Name = "radioButtonNativeApp";
            this.radioButtonNativeApp.Size = new System.Drawing.Size(362, 24);
            this.radioButtonNativeApp.TabIndex = 32;
            this.radioButtonNativeApp.TabStop = true;
            this.radioButtonNativeApp.Text = "Native application (do not authenticate as app)";
            this.radioButtonNativeApp.UseVisualStyleBackColor = true;
            // 
            // buttonAcquireToken
            // 
            this.buttonAcquireToken.Location = new System.Drawing.Point(592, 285);
            this.buttonAcquireToken.Name = "buttonAcquireToken";
            this.buttonAcquireToken.Size = new System.Drawing.Size(138, 34);
            this.buttonAcquireToken.TabIndex = 41;
            this.buttonAcquireToken.Text = "Acquire token";
            this.buttonAcquireToken.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(592, 325);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(138, 34);
            this.buttonClose.TabIndex = 42;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // FormAcquireToken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 373);
            this.ControlBox = false;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonAcquireToken);
            this.Controls.Add(this.groupBoxAuth);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormAcquireToken";
            this.Text = "Acquire OAuth Token";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxAuth.ResumeLayout(false);
            this.groupBoxAuth.PerformLayout();
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
        private System.Windows.Forms.RadioButton radioButtonNativeApp;
        private System.Windows.Forms.TextBox textBoxAuthCertificate;
        private System.Windows.Forms.Button buttonLoadCertificate;
        private System.Windows.Forms.RadioButton radioButtonAuthWithCertificate;
        private System.Windows.Forms.RadioButton radioButtonAuthWithClientSecret;
        private System.Windows.Forms.TextBox textBoxClientSecret;
        private System.Windows.Forms.Button buttonAcquireToken;
        private System.Windows.Forms.Button buttonClose;
    }
}