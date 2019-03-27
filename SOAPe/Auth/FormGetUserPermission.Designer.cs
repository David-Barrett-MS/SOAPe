namespace SOAPe.Auth
{
    partial class FormGetUserPermission
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
            this.buttonListen = new System.Windows.Forms.Button();
            this.textBoxListenAddress = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonLaunchURL = new System.Windows.Forms.Button();
            this.textBoxLoginUrl = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonListen);
            this.groupBox1.Controls.Add(this.textBoxListenAddress);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reply URL";
            // 
            // buttonListen
            // 
            this.buttonListen.Location = new System.Drawing.Point(468, 22);
            this.buttonListen.Name = "buttonListen";
            this.buttonListen.Size = new System.Drawing.Size(96, 34);
            this.buttonListen.TabIndex = 2;
            this.buttonListen.Text = "Listen";
            this.buttonListen.UseVisualStyleBackColor = true;
            this.buttonListen.Click += new System.EventHandler(this.buttonListen_Click);
            // 
            // textBoxListenAddress
            // 
            this.textBoxListenAddress.Location = new System.Drawing.Point(6, 25);
            this.textBoxListenAddress.Name = "textBoxListenAddress";
            this.textBoxListenAddress.Size = new System.Drawing.Size(456, 26);
            this.textBoxListenAddress.TabIndex = 0;
            this.textBoxListenAddress.TextChanged += new System.EventHandler(this.textBoxListenAddress_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonLaunchURL);
            this.groupBox2.Controls.Add(this.textBoxLoginUrl);
            this.groupBox2.Location = new System.Drawing.Point(12, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(570, 67);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Login URL";
            // 
            // buttonLaunchURL
            // 
            this.buttonLaunchURL.Location = new System.Drawing.Point(489, 22);
            this.buttonLaunchURL.Name = "buttonLaunchURL";
            this.buttonLaunchURL.Size = new System.Drawing.Size(75, 34);
            this.buttonLaunchURL.TabIndex = 1;
            this.buttonLaunchURL.Text = "Log-in";
            this.buttonLaunchURL.UseVisualStyleBackColor = true;
            this.buttonLaunchURL.Click += new System.EventHandler(this.buttonLaunchURL_Click);
            // 
            // textBoxLoginUrl
            // 
            this.textBoxLoginUrl.Location = new System.Drawing.Point(6, 25);
            this.textBoxLoginUrl.Name = "textBoxLoginUrl";
            this.textBoxLoginUrl.ReadOnly = true;
            this.textBoxLoginUrl.Size = new System.Drawing.Size(477, 26);
            this.textBoxLoginUrl.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxCode);
            this.groupBox3.Location = new System.Drawing.Point(11, 158);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(570, 64);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Code (you can paste redirect Url here)";
            // 
            // textBoxCode
            // 
            this.textBoxCode.Location = new System.Drawing.Point(6, 25);
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(558, 26);
            this.textBoxCode.TabIndex = 0;
            this.textBoxCode.TextChanged += new System.EventHandler(this.textBoxCode_TextChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(266, 228);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 34);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormGetUserPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 308);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormGetUserPermission";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Obtain User Consent";
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
        private System.Windows.Forms.Button buttonListen;
        private System.Windows.Forms.TextBox textBoxListenAddress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonLaunchURL;
        private System.Windows.Forms.TextBox textBoxLoginUrl;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.Button buttonCancel;
    }
}