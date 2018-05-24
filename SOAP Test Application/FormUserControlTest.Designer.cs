namespace SOAPe
{
    partial class FormUserControlTest
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageUrl = new System.Windows.Forms.TabPage();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.checkBoxBypassProxySettings = new System.Windows.Forms.CheckBox();
            this.tabPageAuth = new System.Windows.Forms.TabPage();
            this.checkBoxForceBasicAuth = new System.Windows.Forms.CheckBox();
            this.textBoxDomain = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.radioButtonSpecificCredentials = new System.Windows.Forms.RadioButton();
            this.radioButtonDefaultCredentials = new System.Windows.Forms.RadioButton();
            this.tabPageEWSHeader = new System.Windows.Forms.TabPage();
            this.checkBoxUpdateEWSHeader = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxImpersonationSID = new System.Windows.Forms.TextBox();
            this.buttonSetImpersonation = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxRequestServerVersion = new System.Windows.Forms.ComboBox();
            this.groupBoxImp = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.statusPercentBar1 = new SOAPe.StatusPercentBar();
            this.dateTimeEdit1 = new SOAPe.DateTimeEdit();
            this.tabControl1.SuspendLayout();
            this.tabPageUrl.SuspendLayout();
            this.tabPageAuth.SuspendLayout();
            this.tabPageEWSHeader.SuspendLayout();
            this.groupBoxImp.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(378, 179);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(247, 20);
            this.textBox1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageUrl);
            this.tabControl1.Controls.Add(this.tabPageAuth);
            this.tabControl1.Controls.Add(this.tabPageEWSHeader);
            this.tabControl1.Location = new System.Drawing.Point(34, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(781, 57);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageUrl
            // 
            this.tabPageUrl.Controls.Add(this.textBoxURL);
            this.tabPageUrl.Controls.Add(this.checkBoxBypassProxySettings);
            this.tabPageUrl.Location = new System.Drawing.Point(4, 22);
            this.tabPageUrl.Name = "tabPageUrl";
            this.tabPageUrl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUrl.Size = new System.Drawing.Size(773, 31);
            this.tabPageUrl.TabIndex = 0;
            this.tabPageUrl.Text = "SOAP URL";
            this.tabPageUrl.UseVisualStyleBackColor = true;
            // 
            // textBoxURL
            // 
            this.textBoxURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURL.Location = new System.Drawing.Point(6, 6);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(653, 20);
            this.textBoxURL.TabIndex = 8;
            // 
            // checkBoxBypassProxySettings
            // 
            this.checkBoxBypassProxySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxBypassProxySettings.AutoSize = true;
            this.checkBoxBypassProxySettings.Location = new System.Drawing.Point(665, 8);
            this.checkBoxBypassProxySettings.Name = "checkBoxBypassProxySettings";
            this.checkBoxBypassProxySettings.Size = new System.Drawing.Size(102, 17);
            this.checkBoxBypassProxySettings.TabIndex = 7;
            this.checkBoxBypassProxySettings.Text = "Bypass IE Proxy";
            this.checkBoxBypassProxySettings.UseVisualStyleBackColor = true;
            // 
            // tabPageAuth
            // 
            this.tabPageAuth.Controls.Add(this.checkBoxForceBasicAuth);
            this.tabPageAuth.Controls.Add(this.textBoxDomain);
            this.tabPageAuth.Controls.Add(this.label1);
            this.tabPageAuth.Controls.Add(this.textBoxPassword);
            this.tabPageAuth.Controls.Add(this.label3);
            this.tabPageAuth.Controls.Add(this.label2);
            this.tabPageAuth.Controls.Add(this.textBoxUsername);
            this.tabPageAuth.Controls.Add(this.radioButtonSpecificCredentials);
            this.tabPageAuth.Controls.Add(this.radioButtonDefaultCredentials);
            this.tabPageAuth.Location = new System.Drawing.Point(4, 22);
            this.tabPageAuth.Name = "tabPageAuth";
            this.tabPageAuth.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAuth.Size = new System.Drawing.Size(773, 31);
            this.tabPageAuth.TabIndex = 1;
            this.tabPageAuth.Text = "Authentication";
            this.tabPageAuth.UseVisualStyleBackColor = true;
            this.tabPageAuth.Click += new System.EventHandler(this.tabPageAuth_Click);
            // 
            // checkBoxForceBasicAuth
            // 
            this.checkBoxForceBasicAuth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxForceBasicAuth.AutoSize = true;
            this.checkBoxForceBasicAuth.Location = new System.Drawing.Point(619, 7);
            this.checkBoxForceBasicAuth.Name = "checkBoxForceBasicAuth";
            this.checkBoxForceBasicAuth.Size = new System.Drawing.Size(151, 17);
            this.checkBoxForceBasicAuth.TabIndex = 18;
            this.checkBoxForceBasicAuth.Text = "Force basic authentication";
            this.checkBoxForceBasicAuth.UseVisualStyleBackColor = true;
            // 
            // textBoxDomain
            // 
            this.textBoxDomain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDomain.Location = new System.Drawing.Point(481, 5);
            this.textBoxDomain.Name = "textBoxDomain";
            this.textBoxDomain.Size = new System.Drawing.Size(125, 20);
            this.textBoxDomain.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(457, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "D:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPassword.Enabled = false;
            this.textBoxPassword.Location = new System.Drawing.Point(345, 5);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(106, 20);
            this.textBoxPassword.TabIndex = 15;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "P:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "U:";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUsername.Enabled = false;
            this.textBoxUsername.Location = new System.Drawing.Point(204, 5);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(112, 20);
            this.textBoxUsername.TabIndex = 7;
            // 
            // radioButtonSpecificCredentials
            // 
            this.radioButtonSpecificCredentials.AutoSize = true;
            this.radioButtonSpecificCredentials.Location = new System.Drawing.Point(94, 6);
            this.radioButtonSpecificCredentials.Name = "radioButtonSpecificCredentials";
            this.radioButtonSpecificCredentials.Size = new System.Drawing.Size(80, 17);
            this.radioButtonSpecificCredentials.TabIndex = 6;
            this.radioButtonSpecificCredentials.Text = "Credentials:";
            this.radioButtonSpecificCredentials.UseVisualStyleBackColor = true;
            // 
            // radioButtonDefaultCredentials
            // 
            this.radioButtonDefaultCredentials.AutoSize = true;
            this.radioButtonDefaultCredentials.Checked = true;
            this.radioButtonDefaultCredentials.Location = new System.Drawing.Point(6, 6);
            this.radioButtonDefaultCredentials.Name = "radioButtonDefaultCredentials";
            this.radioButtonDefaultCredentials.Size = new System.Drawing.Size(82, 17);
            this.radioButtonDefaultCredentials.TabIndex = 5;
            this.radioButtonDefaultCredentials.TabStop = true;
            this.radioButtonDefaultCredentials.Text = "Current user";
            this.radioButtonDefaultCredentials.UseVisualStyleBackColor = true;
            // 
            // tabPageEWSHeader
            // 
            this.tabPageEWSHeader.Controls.Add(this.checkBoxUpdateEWSHeader);
            this.tabPageEWSHeader.Controls.Add(this.label4);
            this.tabPageEWSHeader.Controls.Add(this.textBoxImpersonationSID);
            this.tabPageEWSHeader.Controls.Add(this.buttonSetImpersonation);
            this.tabPageEWSHeader.Controls.Add(this.label6);
            this.tabPageEWSHeader.Controls.Add(this.comboBoxRequestServerVersion);
            this.tabPageEWSHeader.Location = new System.Drawing.Point(4, 22);
            this.tabPageEWSHeader.Name = "tabPageEWSHeader";
            this.tabPageEWSHeader.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEWSHeader.Size = new System.Drawing.Size(773, 31);
            this.tabPageEWSHeader.TabIndex = 2;
            this.tabPageEWSHeader.Text = "EWS Header";
            this.tabPageEWSHeader.UseVisualStyleBackColor = true;
            // 
            // checkBoxUpdateEWSHeader
            // 
            this.checkBoxUpdateEWSHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUpdateEWSHeader.AutoSize = true;
            this.checkBoxUpdateEWSHeader.Checked = true;
            this.checkBoxUpdateEWSHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUpdateEWSHeader.Location = new System.Drawing.Point(597, 8);
            this.checkBoxUpdateEWSHeader.Name = "checkBoxUpdateEWSHeader";
            this.checkBoxUpdateEWSHeader.Size = new System.Drawing.Size(170, 17);
            this.checkBoxUpdateEWSHeader.TabIndex = 16;
            this.checkBoxUpdateEWSHeader.Text = "Update EWS Header on Send";
            this.checkBoxUpdateEWSHeader.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Impersonation:";
            // 
            // textBoxImpersonationSID
            // 
            this.textBoxImpersonationSID.Location = new System.Drawing.Point(357, 6);
            this.textBoxImpersonationSID.Name = "textBoxImpersonationSID";
            this.textBoxImpersonationSID.Size = new System.Drawing.Size(118, 20);
            this.textBoxImpersonationSID.TabIndex = 14;
            // 
            // buttonSetImpersonation
            // 
            this.buttonSetImpersonation.Location = new System.Drawing.Point(481, 5);
            this.buttonSetImpersonation.Name = "buttonSetImpersonation";
            this.buttonSetImpersonation.Size = new System.Drawing.Size(41, 21);
            this.buttonSetImpersonation.TabIndex = 13;
            this.buttonSetImpersonation.Text = "Set...";
            this.buttonSetImpersonation.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Exchange version:";
            // 
            // comboBoxRequestServerVersion
            // 
            this.comboBoxRequestServerVersion.FormattingEnabled = true;
            this.comboBoxRequestServerVersion.Items.AddRange(new object[] {
            "Not set",
            "Exchange2007",
            "Exchange2007_SP1",
            "Exchange2010",
            "Exchange2010_SP1",
            "Exchange2010_SP2",
            "Exchange2013"});
            this.comboBoxRequestServerVersion.Location = new System.Drawing.Point(104, 6);
            this.comboBoxRequestServerVersion.Name = "comboBoxRequestServerVersion";
            this.comboBoxRequestServerVersion.Size = new System.Drawing.Size(165, 21);
            this.comboBoxRequestServerVersion.TabIndex = 11;
            this.comboBoxRequestServerVersion.Text = "Not set";
            // 
            // groupBoxImp
            // 
            this.groupBoxImp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBoxImp.Controls.Add(this.checkBox1);
            this.groupBoxImp.Controls.Add(this.label5);
            this.groupBoxImp.Controls.Add(this.label7);
            this.groupBoxImp.Controls.Add(this.textBox2);
            this.groupBoxImp.Controls.Add(this.comboBox1);
            this.groupBoxImp.Controls.Add(this.button1);
            this.groupBoxImp.Location = new System.Drawing.Point(302, 234);
            this.groupBoxImp.Name = "groupBoxImp";
            this.groupBoxImp.Size = new System.Drawing.Size(278, 101);
            this.groupBoxImp.TabIndex = 4;
            this.groupBoxImp.TabStop = false;
            this.groupBoxImp.Text = "EWS SOAP Header";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(102, 67);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(170, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Update EWS Header on Send";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Exchange version:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Impersonation:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(107, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(118, 20);
            this.textBox2.TabIndex = 8;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Not set",
            "Exchange2007",
            "Exchange2007_SP1",
            "Exchange2010",
            "Exchange2010_SP1",
            "Exchange2010_SP2",
            "Exchange2013"});
            this.comboBox1.Location = new System.Drawing.Point(107, 18);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(165, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Text = "Not set";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 21);
            this.button1.TabIndex = 5;
            this.button1.Text = "Set...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBoxAuth.Controls.Add(this.checkBox2);
            this.groupBoxAuth.Controls.Add(this.radioButton1);
            this.groupBoxAuth.Controls.Add(this.radioButton2);
            this.groupBoxAuth.Controls.Add(this.textBox3);
            this.groupBoxAuth.Controls.Add(this.label8);
            this.groupBoxAuth.Controls.Add(this.label9);
            this.groupBoxAuth.Controls.Add(this.textBox4);
            this.groupBoxAuth.Controls.Add(this.textBox5);
            this.groupBoxAuth.Controls.Add(this.label10);
            this.groupBoxAuth.Location = new System.Drawing.Point(80, 341);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Size = new System.Drawing.Size(478, 101);
            this.groupBoxAuth.TabIndex = 5;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Text = "Authentication";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(317, 70);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(151, 17);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.Text = "Force basic authentication";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(246, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(146, 17);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.Text = "Use specified credentials:";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(59, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(178, 17);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Use logged on user\'s credentials";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(298, 42);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(168, 20);
            this.textBox3.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(243, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Domain:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Password:";
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(71, 42);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(166, 20);
            this.textBox4.TabIndex = 6;
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(71, 68);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(166, 20);
            this.textBox5.TabIndex = 7;
            this.textBox5.UseSystemPasswordChar = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Username:";
            // 
            // textBox6
            // 
            this.textBox6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox6.Location = new System.Drawing.Point(172, 505);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(376, 20);
            this.textBox6.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(102, 508);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "SOAP URL:";
            // 
            // checkBox3
            // 
            this.checkBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(554, 507);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(102, 17);
            this.checkBox3.TabIndex = 7;
            this.checkBox3.Text = "Bypass IE Proxy";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // statusPercentBar1
            // 
            this.statusPercentBar1.BarColour = System.Drawing.Color.PaleGreen;
            this.statusPercentBar1.Location = new System.Drawing.Point(252, 85);
            this.statusPercentBar1.Name = "statusPercentBar1";
            this.statusPercentBar1.PercentComplete = 50D;
            this.statusPercentBar1.Size = new System.Drawing.Size(240, 20);
            this.statusPercentBar1.Status = "fdsfs";
            this.statusPercentBar1.TabIndex = 10;
            // 
            // dateTimeEdit1
            // 
            this.dateTimeEdit1.CalendarForeColor = System.Drawing.SystemColors.ControlText;
            this.dateTimeEdit1.CalendarMonthBackground = System.Drawing.SystemColors.Window;
            this.dateTimeEdit1.DateFormat = "dd/mm/yyyy hh:mm:ss";
            this.dateTimeEdit1.Location = new System.Drawing.Point(378, 153);
            this.dateTimeEdit1.MinimumSize = new System.Drawing.Size(150, 20);
            this.dateTimeEdit1.Name = "dateTimeEdit1";
            this.dateTimeEdit1.Size = new System.Drawing.Size(247, 20);
            this.dateTimeEdit1.TabIndex = 0;
            this.dateTimeEdit1.Value = new System.DateTime(2014, 3, 13, 10, 40, 23, 0);
            this.dateTimeEdit1.ValueChanged += new System.EventHandler(this.dateTimeEdit1_ValueChanged);
            // 
            // FormUserControlTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 569);
            this.Controls.Add(this.statusPercentBar1);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.groupBoxAuth);
            this.Controls.Add(this.groupBoxImp);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dateTimeEdit1);
            this.Name = "FormUserControlTest";
            this.Text = "FormUserControlTest";
            this.Load += new System.EventHandler(this.FormUserControlTest_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageUrl.ResumeLayout(false);
            this.tabPageUrl.PerformLayout();
            this.tabPageAuth.ResumeLayout(false);
            this.tabPageAuth.PerformLayout();
            this.tabPageEWSHeader.ResumeLayout(false);
            this.tabPageEWSHeader.PerformLayout();
            this.groupBoxImp.ResumeLayout(false);
            this.groupBoxImp.PerformLayout();
            this.groupBoxAuth.ResumeLayout(false);
            this.groupBoxAuth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DateTimeEdit dateTimeEdit1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageUrl;
        private System.Windows.Forms.TabPage tabPageAuth;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.CheckBox checkBoxBypassProxySettings;
        private System.Windows.Forms.RadioButton radioButtonDefaultCredentials;
        private System.Windows.Forms.RadioButton radioButtonSpecificCredentials;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDomain;
        private System.Windows.Forms.CheckBox checkBoxForceBasicAuth;
        private System.Windows.Forms.TabPage tabPageEWSHeader;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxRequestServerVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxImpersonationSID;
        private System.Windows.Forms.Button buttonSetImpersonation;
        private System.Windows.Forms.CheckBox checkBoxUpdateEWSHeader;
        private System.Windows.Forms.GroupBox groupBoxImp;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBox3;
        private StatusPercentBar statusPercentBar1;







    }
}