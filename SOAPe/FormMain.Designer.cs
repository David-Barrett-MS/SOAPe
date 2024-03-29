﻿namespace SOAPe
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxPersistCookies = new System.Windows.Forms.CheckBox();
            this.buttonLoadTemplate = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.xmlEditorRequest = new SOAPe.XmlEditor();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxResponse = new SOAPe.GroupBoxHighlight();
            this.xmlEditorResponse = new SOAPe.XmlEditor();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAutoDiscover = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemConvertId = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.getFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GetFolderInboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GetFolderCalendarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GetFolderContactsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GetFolderByIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GetItemByIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FindItemInboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FindItemContactsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTTPListenerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.logViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ConfigurationManagerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.releaseNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageUrl = new System.Windows.Forms.TabPage();
            this.checkBoxFollowRedirect = new System.Windows.Forms.CheckBox();
            this.radioButtonUrlCustom = new System.Windows.Forms.RadioButton();
            this.radioButtonUrlOffice365 = new System.Windows.Forms.RadioButton();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.checkBoxBypassProxySettings = new System.Windows.Forms.CheckBox();
            this.tabPageAuth = new System.Windows.Forms.TabPage();
            this.buttonAppRegistration = new System.Windows.Forms.Button();
            this.buttonAcquireOAuthToken = new System.Windows.Forms.Button();
            this.buttonChooseCertificate = new System.Windows.Forms.Button();
            this.radioButtonCertificateAuthentication = new System.Windows.Forms.RadioButton();
            this.radioButtonNoAuth = new System.Windows.Forms.RadioButton();
            this.labelOAuthToken = new System.Windows.Forms.Label();
            this.radioButtonOAuth = new System.Windows.Forms.RadioButton();
            this.textBoxDomain = new System.Windows.Forms.TextBox();
            this.labelDomain = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.radioButtonSpecificCredentials = new System.Windows.Forms.RadioButton();
            this.radioButtonDefaultCredentials = new System.Windows.Forms.RadioButton();
            this.textBoxAuthCertificate = new System.Windows.Forms.TextBox();
            this.textBoxOAuthToken = new System.Windows.Forms.TextBox();
            this.tabPageEWSHeader = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonUpdateEWSHeader = new System.Windows.Forms.Button();
            this.comboBoxImpersonationMethod = new System.Windows.Forms.ComboBox();
            this.checkBoxUpdateEWSHeader = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxImpersonationSID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxRequestServerVersion = new System.Windows.Forms.ComboBox();
            this.tabPageHTTPHeaders = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonHTTPHeadersClear = new System.Windows.Forms.Button();
            this.buttonHTTPHeaderRemove = new System.Windows.Forms.Button();
            this.buttonHTTPHeaderAdd = new System.Windows.Forms.Button();
            this.textBoxHTTPHeaderValue = new System.Windows.Forms.TextBox();
            this.textBoxHTTPHeaderName = new System.Windows.Forms.TextBox();
            this.listViewHTTPHeaders = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageCookies = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonHTTPCookiesClear = new System.Windows.Forms.Button();
            this.buttonHTTPCookieRemove = new System.Windows.Forms.Button();
            this.buttonHTTPCookieAdd = new System.Windows.Forms.Button();
            this.textBoxHTTPCookieValue = new System.Windows.Forms.TextBox();
            this.textBoxHTTPCookieName = new System.Windows.Forms.TextBox();
            this.listViewHTTPCookies = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageTLS = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxIgnoreCertErrors = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxTLS1_2 = new System.Windows.Forms.CheckBox();
            this.checkBoxTLS1_1 = new System.Windows.Forms.CheckBox();
            this.checkBoxTLS1_0 = new System.Windows.Forms.CheckBox();
            this.tabPageLogging = new System.Windows.Forms.TabPage();
            this.buttonViewOtherLog = new System.Windows.Forms.Button();
            this.buttonViewLogFile = new System.Windows.Forms.Button();
            this.buttonCreateNewLogFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLogFileName = new System.Windows.Forms.TextBox();
            this.buttonBrowseLogFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLogFolder = new System.Windows.Forms.TextBox();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxResponse.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageUrl.SuspendLayout();
            this.tabPageAuth.SuspendLayout();
            this.tabPageEWSHeader.SuspendLayout();
            this.tabPageHTTPHeaders.SuspendLayout();
            this.tabPageCookies.SuspendLayout();
            this.tabPageTLS.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageLogging.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.checkBoxPersistCookies);
            this.groupBox2.Controls.Add(this.buttonLoadTemplate);
            this.groupBox2.Controls.Add(this.buttonSend);
            this.groupBox2.Controls.Add(this.xmlEditorRequest);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(786, 237);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Request";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(281, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxPersistCookies
            // 
            this.checkBoxPersistCookies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPersistCookies.AutoSize = true;
            this.checkBoxPersistCookies.Checked = true;
            this.checkBoxPersistCookies.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPersistCookies.Location = new System.Drawing.Point(604, 22);
            this.checkBoxPersistCookies.Name = "checkBoxPersistCookies";
            this.checkBoxPersistCookies.Size = new System.Drawing.Size(98, 17);
            this.checkBoxPersistCookies.TabIndex = 5;
            this.checkBoxPersistCookies.Text = "Persist Cookies";
            this.toolTips.SetToolTip(this.checkBoxPersistCookies, "If enabled, any cookies received in the response will be put in the cookies for t" +
        "he next request");
            this.checkBoxPersistCookies.UseVisualStyleBackColor = true;
            // 
            // buttonLoadTemplate
            // 
            this.buttonLoadTemplate.Location = new System.Drawing.Point(6, 19);
            this.buttonLoadTemplate.Name = "buttonLoadTemplate";
            this.buttonLoadTemplate.Size = new System.Drawing.Size(118, 21);
            this.buttonLoadTemplate.TabIndex = 3;
            this.buttonLoadTemplate.Text = "Load from template";
            this.toolTips.SetToolTip(this.buttonLoadTemplate, "Open the Xml template editor to create an Xml request");
            this.buttonLoadTemplate.UseVisualStyleBackColor = true;
            this.buttonLoadTemplate.Click += new System.EventHandler(this.buttonLoadTemplate_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(708, 18);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Send";
            this.toolTips.SetToolTip(this.buttonSend, "Send the request");
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // xmlEditorRequest
            // 
            this.xmlEditorRequest.AddExtendedPropertyEnabled = true;
            this.xmlEditorRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xmlEditorRequest.BackColor = System.Drawing.SystemColors.Window;
            this.xmlEditorRequest.IndentXml = true;
            this.xmlEditorRequest.Location = new System.Drawing.Point(3, 48);
            this.xmlEditorRequest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xmlEditorRequest.Name = "xmlEditorRequest";
            this.xmlEditorRequest.ReadOnly = false;
            this.xmlEditorRequest.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2057{\\fonttbl{\\f0\\fnil\\fcharset0 " +
    "Microsoft Sans Serif;}}\r\n{\\*\\generator Riched20 10.0.22000}\\viewkind4\\uc1 \r\n\\par" +
    "d\\f0\\fs17\\par\r\n}\r\n";
            this.xmlEditorRequest.SelectionLength = 0;
            this.xmlEditorRequest.SelectionStart = 0;
            this.xmlEditorRequest.SendItemIdToTemplateEnabled = false;
            this.xmlEditorRequest.Size = new System.Drawing.Size(780, 183);
            this.xmlEditorRequest.SyntaxHighlight = true;
            this.xmlEditorRequest.TabIndex = 7;
            this.xmlEditorRequest.Tag = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 93);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxResponse);
            this.splitContainer1.Size = new System.Drawing.Size(786, 502);
            this.splitContainer1.SplitterDistance = 237;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBoxResponse
            // 
            this.groupBoxResponse.Controls.Add(this.xmlEditorResponse);
            this.groupBoxResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxResponse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxResponse.HighlightColour = System.Drawing.Color.Red;
            this.groupBoxResponse.Highlighted = false;
            this.groupBoxResponse.Location = new System.Drawing.Point(0, 0);
            this.groupBoxResponse.Name = "groupBoxResponse";
            this.groupBoxResponse.Size = new System.Drawing.Size(786, 261);
            this.groupBoxResponse.TabIndex = 0;
            this.groupBoxResponse.TabStop = false;
            this.groupBoxResponse.Text = "Response";
            // 
            // xmlEditorResponse
            // 
            this.xmlEditorResponse.AddExtendedPropertyEnabled = false;
            this.xmlEditorResponse.BackColor = System.Drawing.SystemColors.Window;
            this.xmlEditorResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmlEditorResponse.IndentXml = true;
            this.xmlEditorResponse.Location = new System.Drawing.Point(3, 16);
            this.xmlEditorResponse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xmlEditorResponse.Name = "xmlEditorResponse";
            this.xmlEditorResponse.ReadOnly = true;
            this.xmlEditorResponse.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2057{\\fonttbl{\\f0\\fnil\\fcharset0 " +
    "Microsoft Sans Serif;}}\r\n{\\*\\generator Riched20 10.0.22000}\\viewkind4\\uc1 \r\n\\par" +
    "d\\f0\\fs17\\par\r\n}\r\n";
            this.xmlEditorResponse.SelectionLength = 0;
            this.xmlEditorResponse.SelectionStart = 0;
            this.xmlEditorResponse.SendItemIdToTemplateEnabled = true;
            this.xmlEditorResponse.Size = new System.Drawing.Size(780, 242);
            this.xmlEditorResponse.SyntaxHighlight = true;
            this.xmlEditorResponse.TabIndex = 0;
            this.xmlEditorResponse.Tag = "NoConfigSave";
            this.xmlEditorResponse.SendItemIdToTemplate += new SOAPe.XmlEditor.SendItemIdEventHandler(this.xmlEditorResponse_SendItemIdToTemplate);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(702, -1);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(95, 30);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStripTools";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.AutoSize = false;
            this.toolsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAutoDiscover,
            this.toolStripMenuItemConvertId,
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.hTTPListenerToolStripMenuItem,
            this.toolStripSeparator2,
            this.logViewerToolStripMenuItem,
            this.toolStripSeparator1,
            this.ConfigurationManagerToolStripMenuItem1,
            this.toolStripMenuItem3,
            this.releaseNotesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.toolsToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(91, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // toolStripMenuItemAutoDiscover
            // 
            this.toolStripMenuItemAutoDiscover.Name = "toolStripMenuItemAutoDiscover";
            this.toolStripMenuItemAutoDiscover.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItemAutoDiscover.Text = "Autodiscover...";
            this.toolStripMenuItemAutoDiscover.Click += new System.EventHandler(this.toolStripMenuItemAutoDiscover_Click);
            // 
            // toolStripMenuItemConvertId
            // 
            this.toolStripMenuItemConvertId.Name = "toolStripMenuItemConvertId";
            this.toolStripMenuItemConvertId.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItemConvertId.Text = "ConvertId...";
            this.toolStripMenuItemConvertId.Click += new System.EventHandler(this.toolStripMenuItemConvertId_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItem2.Text = "Base64 Encoder...";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getFolderToolStripMenuItem,
            this.getItemToolStripMenuItem,
            this.findItemToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItem1.Text = "EWS Tests";
            // 
            // getFolderToolStripMenuItem
            // 
            this.getFolderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GetFolderInboxToolStripMenuItem,
            this.GetFolderCalendarToolStripMenuItem,
            this.GetFolderContactsToolStripMenuItem,
            this.GetFolderByIdToolStripMenuItem});
            this.getFolderToolStripMenuItem.Name = "getFolderToolStripMenuItem";
            this.getFolderToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.getFolderToolStripMenuItem.Text = "GetFolder";
            // 
            // GetFolderInboxToolStripMenuItem
            // 
            this.GetFolderInboxToolStripMenuItem.Name = "GetFolderInboxToolStripMenuItem";
            this.GetFolderInboxToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.GetFolderInboxToolStripMenuItem.Text = "Inbox";
            this.GetFolderInboxToolStripMenuItem.Click += new System.EventHandler(this.GetFolderInboxToolStripMenuItem_Click);
            // 
            // GetFolderCalendarToolStripMenuItem
            // 
            this.GetFolderCalendarToolStripMenuItem.Name = "GetFolderCalendarToolStripMenuItem";
            this.GetFolderCalendarToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.GetFolderCalendarToolStripMenuItem.Text = "Calendar";
            this.GetFolderCalendarToolStripMenuItem.Click += new System.EventHandler(this.GetFolderCalendarToolStripMenuItem_Click);
            // 
            // GetFolderContactsToolStripMenuItem
            // 
            this.GetFolderContactsToolStripMenuItem.Name = "GetFolderContactsToolStripMenuItem";
            this.GetFolderContactsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.GetFolderContactsToolStripMenuItem.Text = "Contacts";
            this.GetFolderContactsToolStripMenuItem.Click += new System.EventHandler(this.GetFolderContactsToolStripMenuItem_Click);
            // 
            // GetFolderByIdToolStripMenuItem
            // 
            this.GetFolderByIdToolStripMenuItem.Name = "GetFolderByIdToolStripMenuItem";
            this.GetFolderByIdToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.GetFolderByIdToolStripMenuItem.Text = "By Id...";
            this.GetFolderByIdToolStripMenuItem.Click += new System.EventHandler(this.GetFolderByIdToolStripMenuItem_Click);
            // 
            // getItemToolStripMenuItem
            // 
            this.getItemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GetItemByIdToolStripMenuItem});
            this.getItemToolStripMenuItem.Name = "getItemToolStripMenuItem";
            this.getItemToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.getItemToolStripMenuItem.Text = "GetItem";
            // 
            // GetItemByIdToolStripMenuItem
            // 
            this.GetItemByIdToolStripMenuItem.Name = "GetItemByIdToolStripMenuItem";
            this.GetItemByIdToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.GetItemByIdToolStripMenuItem.Text = "By Id...";
            this.GetItemByIdToolStripMenuItem.Click += new System.EventHandler(this.GetItemByIdToolStripMenuItem_Click);
            // 
            // findItemToolStripMenuItem
            // 
            this.findItemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FindItemInboxToolStripMenuItem,
            this.FindItemContactsToolStripMenuItem});
            this.findItemToolStripMenuItem.Name = "findItemToolStripMenuItem";
            this.findItemToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.findItemToolStripMenuItem.Text = "FindItem";
            // 
            // FindItemInboxToolStripMenuItem
            // 
            this.FindItemInboxToolStripMenuItem.Name = "FindItemInboxToolStripMenuItem";
            this.FindItemInboxToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.FindItemInboxToolStripMenuItem.Text = "Inbox";
            this.FindItemInboxToolStripMenuItem.Click += new System.EventHandler(this.FindItemInboxToolStripMenuItem_Click);
            // 
            // FindItemContactsToolStripMenuItem
            // 
            this.FindItemContactsToolStripMenuItem.Name = "FindItemContactsToolStripMenuItem";
            this.FindItemContactsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.FindItemContactsToolStripMenuItem.Text = "Contacts";
            this.FindItemContactsToolStripMenuItem.Click += new System.EventHandler(this.FindItemContactsToolStripMenuItem_Click);
            // 
            // hTTPListenerToolStripMenuItem
            // 
            this.hTTPListenerToolStripMenuItem.Name = "hTTPListenerToolStripMenuItem";
            this.hTTPListenerToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.hTTPListenerToolStripMenuItem.Text = "HTTP Listener...";
            this.hTTPListenerToolStripMenuItem.Click += new System.EventHandler(this.hTTPListenerToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(204, 6);
            // 
            // logViewerToolStripMenuItem
            // 
            this.logViewerToolStripMenuItem.Name = "logViewerToolStripMenuItem";
            this.logViewerToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.logViewerToolStripMenuItem.Text = "Log Viewer...";
            this.logViewerToolStripMenuItem.Click += new System.EventHandler(this.logViewerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
            // 
            // ConfigurationManagerToolStripMenuItem1
            // 
            this.ConfigurationManagerToolStripMenuItem1.Name = "ConfigurationManagerToolStripMenuItem1";
            this.ConfigurationManagerToolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
            this.ConfigurationManagerToolStripMenuItem1.Text = "Configuration Manager...";
            this.ConfigurationManagerToolStripMenuItem1.Click += new System.EventHandler(this.ConfigurationManagerToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(204, 6);
            // 
            // releaseNotesToolStripMenuItem
            // 
            this.releaseNotesToolStripMenuItem.Name = "releaseNotesToolStripMenuItem";
            this.releaseNotesToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.releaseNotesToolStripMenuItem.Text = "Release Notes...";
            this.releaseNotesToolStripMenuItem.Click += new System.EventHandler(this.releaseNotesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageUrl);
            this.tabControl1.Controls.Add(this.tabPageAuth);
            this.tabControl1.Controls.Add(this.tabPageEWSHeader);
            this.tabControl1.Controls.Add(this.tabPageHTTPHeaders);
            this.tabControl1.Controls.Add(this.tabPageCookies);
            this.tabControl1.Controls.Add(this.tabPageTLS);
            this.tabControl1.Controls.Add(this.tabPageLogging);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(789, 75);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPageUrl
            // 
            this.tabPageUrl.Controls.Add(this.checkBoxFollowRedirect);
            this.tabPageUrl.Controls.Add(this.radioButtonUrlCustom);
            this.tabPageUrl.Controls.Add(this.radioButtonUrlOffice365);
            this.tabPageUrl.Controls.Add(this.textBoxURL);
            this.tabPageUrl.Controls.Add(this.checkBoxBypassProxySettings);
            this.tabPageUrl.Location = new System.Drawing.Point(4, 22);
            this.tabPageUrl.Name = "tabPageUrl";
            this.tabPageUrl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUrl.Size = new System.Drawing.Size(781, 49);
            this.tabPageUrl.TabIndex = 0;
            this.tabPageUrl.Text = "SOAP URL";
            this.tabPageUrl.UseVisualStyleBackColor = true;
            // 
            // checkBoxFollowRedirect
            // 
            this.checkBoxFollowRedirect.AutoSize = true;
            this.checkBoxFollowRedirect.Location = new System.Drawing.Point(468, 30);
            this.checkBoxFollowRedirect.Name = "checkBoxFollowRedirect";
            this.checkBoxFollowRedirect.Size = new System.Drawing.Size(181, 17);
            this.checkBoxFollowRedirect.TabIndex = 12;
            this.checkBoxFollowRedirect.Text = "Follow HTTP redirects (301/302)";
            this.toolTips.SetToolTip(this.checkBoxFollowRedirect, "Shouldn\'t be required for EWS, but is useful when manually testing AutoDiscover");
            this.checkBoxFollowRedirect.UseVisualStyleBackColor = true;
            // 
            // radioButtonUrlCustom
            // 
            this.radioButtonUrlCustom.AutoSize = true;
            this.radioButtonUrlCustom.Location = new System.Drawing.Point(150, 29);
            this.radioButtonUrlCustom.Name = "radioButtonUrlCustom";
            this.radioButtonUrlCustom.Size = new System.Drawing.Size(60, 17);
            this.radioButtonUrlCustom.TabIndex = 11;
            this.radioButtonUrlCustom.TabStop = true;
            this.radioButtonUrlCustom.Tag = "";
            this.radioButtonUrlCustom.Text = "Custom";
            this.toolTips.SetToolTip(this.radioButtonUrlCustom, "Use a custom Url");
            this.radioButtonUrlCustom.UseVisualStyleBackColor = true;
            this.radioButtonUrlCustom.CheckedChanged += new System.EventHandler(this.radioButtonUrlCustom_CheckedChanged);
            // 
            // radioButtonUrlOffice365
            // 
            this.radioButtonUrlOffice365.AutoSize = true;
            this.radioButtonUrlOffice365.Location = new System.Drawing.Point(6, 29);
            this.radioButtonUrlOffice365.Name = "radioButtonUrlOffice365";
            this.radioButtonUrlOffice365.Size = new System.Drawing.Size(74, 17);
            this.radioButtonUrlOffice365.TabIndex = 10;
            this.radioButtonUrlOffice365.TabStop = true;
            this.radioButtonUrlOffice365.Tag = "https://outlook.office365.com/EWS/Exchange.asmx";
            this.radioButtonUrlOffice365.Text = "Office 365";
            this.toolTips.SetToolTip(this.radioButtonUrlOffice365, "Use standard Office 365 EWS Url");
            this.radioButtonUrlOffice365.UseVisualStyleBackColor = true;
            this.radioButtonUrlOffice365.CheckedChanged += new System.EventHandler(this.radioButtonUrlOffice365_CheckedChanged);
            // 
            // textBoxURL
            // 
            this.textBoxURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURL.Location = new System.Drawing.Point(6, 6);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(769, 20);
            this.textBoxURL.TabIndex = 8;
            this.textBoxURL.TextChanged += new System.EventHandler(this.textBoxURL_TextChanged);
            // 
            // checkBoxBypassProxySettings
            // 
            this.checkBoxBypassProxySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxBypassProxySettings.AutoSize = true;
            this.checkBoxBypassProxySettings.Location = new System.Drawing.Point(652, 30);
            this.checkBoxBypassProxySettings.Name = "checkBoxBypassProxySettings";
            this.checkBoxBypassProxySettings.Size = new System.Drawing.Size(123, 17);
            this.checkBoxBypassProxySettings.TabIndex = 7;
            this.checkBoxBypassProxySettings.Text = "Bypass system proxy";
            this.toolTips.SetToolTip(this.checkBoxBypassProxySettings, "Connect directly to the specified URL (bypassing any proxy settings)");
            this.checkBoxBypassProxySettings.UseVisualStyleBackColor = true;
            // 
            // tabPageAuth
            // 
            this.tabPageAuth.Controls.Add(this.buttonAppRegistration);
            this.tabPageAuth.Controls.Add(this.buttonAcquireOAuthToken);
            this.tabPageAuth.Controls.Add(this.buttonChooseCertificate);
            this.tabPageAuth.Controls.Add(this.radioButtonCertificateAuthentication);
            this.tabPageAuth.Controls.Add(this.radioButtonNoAuth);
            this.tabPageAuth.Controls.Add(this.labelOAuthToken);
            this.tabPageAuth.Controls.Add(this.radioButtonOAuth);
            this.tabPageAuth.Controls.Add(this.textBoxDomain);
            this.tabPageAuth.Controls.Add(this.labelDomain);
            this.tabPageAuth.Controls.Add(this.textBoxPassword);
            this.tabPageAuth.Controls.Add(this.labelPassword);
            this.tabPageAuth.Controls.Add(this.labelUsername);
            this.tabPageAuth.Controls.Add(this.textBoxUsername);
            this.tabPageAuth.Controls.Add(this.radioButtonSpecificCredentials);
            this.tabPageAuth.Controls.Add(this.radioButtonDefaultCredentials);
            this.tabPageAuth.Controls.Add(this.textBoxAuthCertificate);
            this.tabPageAuth.Controls.Add(this.textBoxOAuthToken);
            this.tabPageAuth.Location = new System.Drawing.Point(4, 22);
            this.tabPageAuth.Name = "tabPageAuth";
            this.tabPageAuth.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAuth.Size = new System.Drawing.Size(781, 49);
            this.tabPageAuth.TabIndex = 1;
            this.tabPageAuth.Text = "Authentication";
            this.tabPageAuth.UseVisualStyleBackColor = true;
            // 
            // buttonAppRegistration
            // 
            this.buttonAppRegistration.Location = new System.Drawing.Point(680, 22);
            this.buttonAppRegistration.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAppRegistration.Name = "buttonAppRegistration";
            this.buttonAppRegistration.Size = new System.Drawing.Size(97, 22);
            this.buttonAppRegistration.TabIndex = 28;
            this.buttonAppRegistration.Text = "OAuth Settings...";
            this.buttonAppRegistration.UseVisualStyleBackColor = true;
            this.buttonAppRegistration.Click += new System.EventHandler(this.buttonAppRegistration_Click);
            // 
            // buttonAcquireOAuthToken
            // 
            this.buttonAcquireOAuthToken.Location = new System.Drawing.Point(613, 22);
            this.buttonAcquireOAuthToken.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAcquireOAuthToken.Name = "buttonAcquireOAuthToken";
            this.buttonAcquireOAuthToken.Size = new System.Drawing.Size(62, 22);
            this.buttonAcquireOAuthToken.TabIndex = 27;
            this.buttonAcquireOAuthToken.Text = "Acquire";
            this.buttonAcquireOAuthToken.UseVisualStyleBackColor = true;
            this.buttonAcquireOAuthToken.Click += new System.EventHandler(this.buttonAcquireOAuthToken_Click);
            // 
            // buttonChooseCertificate
            // 
            this.buttonChooseCertificate.Location = new System.Drawing.Point(705, 22);
            this.buttonChooseCertificate.Name = "buttonChooseCertificate";
            this.buttonChooseCertificate.Size = new System.Drawing.Size(75, 22);
            this.buttonChooseCertificate.TabIndex = 26;
            this.buttonChooseCertificate.Text = "Choose...";
            this.buttonChooseCertificate.UseVisualStyleBackColor = true;
            this.buttonChooseCertificate.Click += new System.EventHandler(this.buttonChooseCertificate_Click);
            // 
            // radioButtonCertificateAuthentication
            // 
            this.radioButtonCertificateAuthentication.AutoSize = true;
            this.radioButtonCertificateAuthentication.Location = new System.Drawing.Point(177, 6);
            this.radioButtonCertificateAuthentication.Name = "radioButtonCertificateAuthentication";
            this.radioButtonCertificateAuthentication.Size = new System.Drawing.Size(72, 17);
            this.radioButtonCertificateAuthentication.TabIndex = 24;
            this.radioButtonCertificateAuthentication.TabStop = true;
            this.radioButtonCertificateAuthentication.Text = "Certificate";
            this.radioButtonCertificateAuthentication.UseVisualStyleBackColor = true;
            this.radioButtonCertificateAuthentication.CheckedChanged += new System.EventHandler(this.radioButtonCertificateAuthentication_CheckedChanged);
            // 
            // radioButtonNoAuth
            // 
            this.radioButtonNoAuth.AutoSize = true;
            this.radioButtonNoAuth.Location = new System.Drawing.Point(6, 24);
            this.radioButtonNoAuth.Name = "radioButtonNoAuth";
            this.radioButtonNoAuth.Size = new System.Drawing.Size(51, 17);
            this.radioButtonNoAuth.TabIndex = 23;
            this.radioButtonNoAuth.TabStop = true;
            this.radioButtonNoAuth.Text = "None";
            this.radioButtonNoAuth.UseVisualStyleBackColor = true;
            // 
            // labelOAuthToken
            // 
            this.labelOAuthToken.AutoSize = true;
            this.labelOAuthToken.Location = new System.Drawing.Point(155, 26);
            this.labelOAuthToken.Name = "labelOAuthToken";
            this.labelOAuthToken.Size = new System.Drawing.Size(41, 13);
            this.labelOAuthToken.TabIndex = 22;
            this.labelOAuthToken.Text = "Token:";
            // 
            // radioButtonOAuth
            // 
            this.radioButtonOAuth.AutoSize = true;
            this.radioButtonOAuth.Location = new System.Drawing.Point(94, 24);
            this.radioButtonOAuth.Name = "radioButtonOAuth";
            this.radioButtonOAuth.Size = new System.Drawing.Size(55, 17);
            this.radioButtonOAuth.TabIndex = 20;
            this.radioButtonOAuth.TabStop = true;
            this.radioButtonOAuth.Text = "OAuth";
            this.radioButtonOAuth.UseVisualStyleBackColor = true;
            this.radioButtonOAuth.CheckedChanged += new System.EventHandler(this.radioButtonOAuth_CheckedChanged);
            // 
            // textBoxDomain
            // 
            this.textBoxDomain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDomain.Location = new System.Drawing.Point(569, 24);
            this.textBoxDomain.Name = "textBoxDomain";
            this.textBoxDomain.Size = new System.Drawing.Size(110, 20);
            this.textBoxDomain.TabIndex = 17;
            // 
            // labelDomain
            // 
            this.labelDomain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDomain.AutoSize = true;
            this.labelDomain.Location = new System.Drawing.Point(566, 7);
            this.labelDomain.Name = "labelDomain";
            this.labelDomain.Size = new System.Drawing.Size(43, 13);
            this.labelDomain.TabIndex = 16;
            this.labelDomain.Text = "Domain";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPassword.Location = new System.Drawing.Point(453, 24);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(110, 20);
            this.textBoxPassword.TabIndex = 15;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // labelPassword
            // 
            this.labelPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(450, 7);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 14;
            this.labelPassword.Text = "Password";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(274, 8);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(55, 13);
            this.labelUsername.TabIndex = 11;
            this.labelUsername.Text = "Username";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUsername.Location = new System.Drawing.Point(277, 24);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(170, 20);
            this.textBoxUsername.TabIndex = 7;
            // 
            // radioButtonSpecificCredentials
            // 
            this.radioButtonSpecificCredentials.AutoSize = true;
            this.radioButtonSpecificCredentials.Location = new System.Drawing.Point(94, 6);
            this.radioButtonSpecificCredentials.Name = "radioButtonSpecificCredentials";
            this.radioButtonSpecificCredentials.Size = new System.Drawing.Size(77, 17);
            this.radioButtonSpecificCredentials.TabIndex = 6;
            this.radioButtonSpecificCredentials.Text = "Credentials";
            this.radioButtonSpecificCredentials.UseVisualStyleBackColor = true;
            this.radioButtonSpecificCredentials.CheckedChanged += new System.EventHandler(this.radioButtonSpecificCredentials_CheckedChanged);
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
            this.radioButtonDefaultCredentials.CheckedChanged += new System.EventHandler(this.radioButtonDefaultCredentials_CheckedChanged);
            // 
            // textBoxAuthCertificate
            // 
            this.textBoxAuthCertificate.Location = new System.Drawing.Point(177, 23);
            this.textBoxAuthCertificate.Name = "textBoxAuthCertificate";
            this.textBoxAuthCertificate.ReadOnly = true;
            this.textBoxAuthCertificate.Size = new System.Drawing.Size(524, 20);
            this.textBoxAuthCertificate.TabIndex = 25;
            // 
            // textBoxOAuthToken
            // 
            this.textBoxOAuthToken.Location = new System.Drawing.Point(202, 23);
            this.textBoxOAuthToken.Name = "textBoxOAuthToken";
            this.textBoxOAuthToken.Size = new System.Drawing.Size(408, 20);
            this.textBoxOAuthToken.TabIndex = 21;
            // 
            // tabPageEWSHeader
            // 
            this.tabPageEWSHeader.Controls.Add(this.label10);
            this.tabPageEWSHeader.Controls.Add(this.buttonUpdateEWSHeader);
            this.tabPageEWSHeader.Controls.Add(this.comboBoxImpersonationMethod);
            this.tabPageEWSHeader.Controls.Add(this.checkBoxUpdateEWSHeader);
            this.tabPageEWSHeader.Controls.Add(this.label4);
            this.tabPageEWSHeader.Controls.Add(this.textBoxImpersonationSID);
            this.tabPageEWSHeader.Controls.Add(this.label6);
            this.tabPageEWSHeader.Controls.Add(this.comboBoxRequestServerVersion);
            this.tabPageEWSHeader.Location = new System.Drawing.Point(4, 22);
            this.tabPageEWSHeader.Name = "tabPageEWSHeader";
            this.tabPageEWSHeader.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEWSHeader.Size = new System.Drawing.Size(781, 49);
            this.tabPageEWSHeader.TabIndex = 2;
            this.tabPageEWSHeader.Text = "EWS Header";
            this.tabPageEWSHeader.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(248, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(341, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Note: X-AnchorMailbox is automatically set to the impersonated mailbox";
            // 
            // buttonUpdateEWSHeader
            // 
            this.buttonUpdateEWSHeader.Location = new System.Drawing.Point(700, 5);
            this.buttonUpdateEWSHeader.Name = "buttonUpdateEWSHeader";
            this.buttonUpdateEWSHeader.Size = new System.Drawing.Size(75, 22);
            this.buttonUpdateEWSHeader.TabIndex = 18;
            this.buttonUpdateEWSHeader.Text = "Update Now";
            this.buttonUpdateEWSHeader.UseVisualStyleBackColor = true;
            this.buttonUpdateEWSHeader.Click += new System.EventHandler(this.buttonUpdateEWSHeader_Click);
            // 
            // comboBoxImpersonationMethod
            // 
            this.comboBoxImpersonationMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxImpersonationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImpersonationMethod.FormattingEnabled = true;
            this.comboBoxImpersonationMethod.Items.AddRange(new object[] {
            "Primary SMTP Address",
            "UPN (User Principal Name)",
            "SID"});
            this.comboBoxImpersonationMethod.Location = new System.Drawing.Point(532, 6);
            this.comboBoxImpersonationMethod.Name = "comboBoxImpersonationMethod";
            this.comboBoxImpersonationMethod.Size = new System.Drawing.Size(151, 21);
            this.comboBoxImpersonationMethod.TabIndex = 17;
            // 
            // checkBoxUpdateEWSHeader
            // 
            this.checkBoxUpdateEWSHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUpdateEWSHeader.AutoSize = true;
            this.checkBoxUpdateEWSHeader.Checked = true;
            this.checkBoxUpdateEWSHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUpdateEWSHeader.Location = new System.Drawing.Point(608, 30);
            this.checkBoxUpdateEWSHeader.Name = "checkBoxUpdateEWSHeader";
            this.checkBoxUpdateEWSHeader.Size = new System.Drawing.Size(170, 17);
            this.checkBoxUpdateEWSHeader.TabIndex = 16;
            this.checkBoxUpdateEWSHeader.Text = "Update EWS Header on Send";
            this.checkBoxUpdateEWSHeader.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Impersonate:";
            // 
            // textBoxImpersonationSID
            // 
            this.textBoxImpersonationSID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxImpersonationSID.Location = new System.Drawing.Point(311, 6);
            this.textBoxImpersonationSID.Name = "textBoxImpersonationSID";
            this.textBoxImpersonationSID.Size = new System.Drawing.Size(215, 20);
            this.textBoxImpersonationSID.TabIndex = 14;
            this.textBoxImpersonationSID.TextChanged += new System.EventHandler(this.textBoxImpersonationSID_TextChanged);
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
            "Exchange2013",
            "Exchange2013_SP1",
            "Exchange2016"});
            this.comboBoxRequestServerVersion.Location = new System.Drawing.Point(104, 6);
            this.comboBoxRequestServerVersion.Name = "comboBoxRequestServerVersion";
            this.comboBoxRequestServerVersion.Size = new System.Drawing.Size(128, 21);
            this.comboBoxRequestServerVersion.TabIndex = 11;
            this.comboBoxRequestServerVersion.Text = "Not set";
            this.comboBoxRequestServerVersion.SelectedIndexChanged += new System.EventHandler(this.comboBoxRequestServerVersion_SelectedIndexChanged);
            // 
            // tabPageHTTPHeaders
            // 
            this.tabPageHTTPHeaders.Controls.Add(this.label7);
            this.tabPageHTTPHeaders.Controls.Add(this.label5);
            this.tabPageHTTPHeaders.Controls.Add(this.buttonHTTPHeadersClear);
            this.tabPageHTTPHeaders.Controls.Add(this.buttonHTTPHeaderRemove);
            this.tabPageHTTPHeaders.Controls.Add(this.buttonHTTPHeaderAdd);
            this.tabPageHTTPHeaders.Controls.Add(this.textBoxHTTPHeaderValue);
            this.tabPageHTTPHeaders.Controls.Add(this.textBoxHTTPHeaderName);
            this.tabPageHTTPHeaders.Controls.Add(this.listViewHTTPHeaders);
            this.tabPageHTTPHeaders.Location = new System.Drawing.Point(4, 22);
            this.tabPageHTTPHeaders.Name = "tabPageHTTPHeaders";
            this.tabPageHTTPHeaders.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHTTPHeaders.Size = new System.Drawing.Size(781, 49);
            this.tabPageHTTPHeaders.TabIndex = 3;
            this.tabPageHTTPHeaders.Text = "HTTP Headers";
            this.tabPageHTTPHeaders.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(585, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Value";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(479, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Header Name";
            // 
            // buttonHTTPHeadersClear
            // 
            this.buttonHTTPHeadersClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHTTPHeadersClear.Location = new System.Drawing.Point(402, 1);
            this.buttonHTTPHeadersClear.Name = "buttonHTTPHeadersClear";
            this.buttonHTTPHeadersClear.Size = new System.Drawing.Size(59, 21);
            this.buttonHTTPHeadersClear.TabIndex = 5;
            this.buttonHTTPHeadersClear.Text = "Clear";
            this.buttonHTTPHeadersClear.UseVisualStyleBackColor = true;
            this.buttonHTTPHeadersClear.Click += new System.EventHandler(this.buttonHTTPHeadersClear_Click);
            // 
            // buttonHTTPHeaderRemove
            // 
            this.buttonHTTPHeaderRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHTTPHeaderRemove.Location = new System.Drawing.Point(402, 28);
            this.buttonHTTPHeaderRemove.Name = "buttonHTTPHeaderRemove";
            this.buttonHTTPHeaderRemove.Size = new System.Drawing.Size(59, 21);
            this.buttonHTTPHeaderRemove.TabIndex = 4;
            this.buttonHTTPHeaderRemove.Text = "Remove";
            this.buttonHTTPHeaderRemove.UseVisualStyleBackColor = true;
            this.buttonHTTPHeaderRemove.Click += new System.EventHandler(this.buttonHTTPHeaderRemove_Click);
            // 
            // buttonHTTPHeaderAdd
            // 
            this.buttonHTTPHeaderAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHTTPHeaderAdd.Location = new System.Drawing.Point(736, 28);
            this.buttonHTTPHeaderAdd.Name = "buttonHTTPHeaderAdd";
            this.buttonHTTPHeaderAdd.Size = new System.Drawing.Size(44, 21);
            this.buttonHTTPHeaderAdd.TabIndex = 3;
            this.buttonHTTPHeaderAdd.Text = "Add";
            this.buttonHTTPHeaderAdd.UseVisualStyleBackColor = true;
            this.buttonHTTPHeaderAdd.Click += new System.EventHandler(this.buttonHTTPHeaderAdd_Click);
            // 
            // textBoxHTTPHeaderValue
            // 
            this.textBoxHTTPHeaderValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHTTPHeaderValue.Location = new System.Drawing.Point(588, 29);
            this.textBoxHTTPHeaderValue.Name = "textBoxHTTPHeaderValue";
            this.textBoxHTTPHeaderValue.Size = new System.Drawing.Size(143, 20);
            this.textBoxHTTPHeaderValue.TabIndex = 2;
            // 
            // textBoxHTTPHeaderName
            // 
            this.textBoxHTTPHeaderName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHTTPHeaderName.Location = new System.Drawing.Point(482, 29);
            this.textBoxHTTPHeaderName.Name = "textBoxHTTPHeaderName";
            this.textBoxHTTPHeaderName.Size = new System.Drawing.Size(100, 20);
            this.textBoxHTTPHeaderName.TabIndex = 1;
            this.textBoxHTTPHeaderName.TextChanged += new System.EventHandler(this.textBoxHTTPHeaderName_TextChanged);
            // 
            // listViewHTTPHeaders
            // 
            this.listViewHTTPHeaders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewHTTPHeaders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderValue});
            this.listViewHTTPHeaders.FullRowSelect = true;
            this.listViewHTTPHeaders.GridLines = true;
            this.listViewHTTPHeaders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewHTTPHeaders.HideSelection = false;
            this.listViewHTTPHeaders.Location = new System.Drawing.Point(0, 1);
            this.listViewHTTPHeaders.MultiSelect = false;
            this.listViewHTTPHeaders.Name = "listViewHTTPHeaders";
            this.listViewHTTPHeaders.Size = new System.Drawing.Size(400, 48);
            this.listViewHTTPHeaders.TabIndex = 0;
            this.listViewHTTPHeaders.UseCompatibleStateImageBehavior = false;
            this.listViewHTTPHeaders.View = System.Windows.Forms.View.Details;
            this.listViewHTTPHeaders.SelectedIndexChanged += new System.EventHandler(this.listViewHTTPHeaders_SelectedIndexChanged);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 140;
            // 
            // columnHeaderValue
            // 
            this.columnHeaderValue.Text = "Value";
            this.columnHeaderValue.Width = 235;
            // 
            // tabPageCookies
            // 
            this.tabPageCookies.Controls.Add(this.label8);
            this.tabPageCookies.Controls.Add(this.label9);
            this.tabPageCookies.Controls.Add(this.buttonHTTPCookiesClear);
            this.tabPageCookies.Controls.Add(this.buttonHTTPCookieRemove);
            this.tabPageCookies.Controls.Add(this.buttonHTTPCookieAdd);
            this.tabPageCookies.Controls.Add(this.textBoxHTTPCookieValue);
            this.tabPageCookies.Controls.Add(this.textBoxHTTPCookieName);
            this.tabPageCookies.Controls.Add(this.listViewHTTPCookies);
            this.tabPageCookies.Location = new System.Drawing.Point(4, 22);
            this.tabPageCookies.Name = "tabPageCookies";
            this.tabPageCookies.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCookies.Size = new System.Drawing.Size(781, 49);
            this.tabPageCookies.TabIndex = 4;
            this.tabPageCookies.Text = "HTTP Cookies";
            this.tabPageCookies.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(585, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Value";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(479, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Cookie Name";
            // 
            // buttonHTTPCookiesClear
            // 
            this.buttonHTTPCookiesClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHTTPCookiesClear.Location = new System.Drawing.Point(402, 1);
            this.buttonHTTPCookiesClear.Name = "buttonHTTPCookiesClear";
            this.buttonHTTPCookiesClear.Size = new System.Drawing.Size(59, 21);
            this.buttonHTTPCookiesClear.TabIndex = 13;
            this.buttonHTTPCookiesClear.Text = "Clear";
            this.buttonHTTPCookiesClear.UseVisualStyleBackColor = true;
            this.buttonHTTPCookiesClear.Click += new System.EventHandler(this.buttonHTTPCookiesClear_Click);
            // 
            // buttonHTTPCookieRemove
            // 
            this.buttonHTTPCookieRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHTTPCookieRemove.Location = new System.Drawing.Point(402, 28);
            this.buttonHTTPCookieRemove.Name = "buttonHTTPCookieRemove";
            this.buttonHTTPCookieRemove.Size = new System.Drawing.Size(59, 21);
            this.buttonHTTPCookieRemove.TabIndex = 12;
            this.buttonHTTPCookieRemove.Text = "Remove";
            this.buttonHTTPCookieRemove.UseVisualStyleBackColor = true;
            this.buttonHTTPCookieRemove.Click += new System.EventHandler(this.buttonHTTPCookieRemove_Click);
            // 
            // buttonHTTPCookieAdd
            // 
            this.buttonHTTPCookieAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHTTPCookieAdd.Location = new System.Drawing.Point(736, 28);
            this.buttonHTTPCookieAdd.Name = "buttonHTTPCookieAdd";
            this.buttonHTTPCookieAdd.Size = new System.Drawing.Size(44, 21);
            this.buttonHTTPCookieAdd.TabIndex = 11;
            this.buttonHTTPCookieAdd.Text = "Add";
            this.buttonHTTPCookieAdd.UseVisualStyleBackColor = true;
            this.buttonHTTPCookieAdd.Click += new System.EventHandler(this.buttonHTTPCookiesAdd_Click);
            // 
            // textBoxHTTPCookieValue
            // 
            this.textBoxHTTPCookieValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHTTPCookieValue.Location = new System.Drawing.Point(588, 29);
            this.textBoxHTTPCookieValue.Name = "textBoxHTTPCookieValue";
            this.textBoxHTTPCookieValue.Size = new System.Drawing.Size(143, 20);
            this.textBoxHTTPCookieValue.TabIndex = 10;
            // 
            // textBoxHTTPCookieName
            // 
            this.textBoxHTTPCookieName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHTTPCookieName.Location = new System.Drawing.Point(482, 29);
            this.textBoxHTTPCookieName.Name = "textBoxHTTPCookieName";
            this.textBoxHTTPCookieName.Size = new System.Drawing.Size(100, 20);
            this.textBoxHTTPCookieName.TabIndex = 9;
            this.textBoxHTTPCookieName.TextChanged += new System.EventHandler(this.textBoxHTTPCookieName_TextChanged);
            // 
            // listViewHTTPCookies
            // 
            this.listViewHTTPCookies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewHTTPCookies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewHTTPCookies.FullRowSelect = true;
            this.listViewHTTPCookies.GridLines = true;
            this.listViewHTTPCookies.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewHTTPCookies.HideSelection = false;
            this.listViewHTTPCookies.Location = new System.Drawing.Point(0, 1);
            this.listViewHTTPCookies.MultiSelect = false;
            this.listViewHTTPCookies.Name = "listViewHTTPCookies";
            this.listViewHTTPCookies.Size = new System.Drawing.Size(400, 48);
            this.listViewHTTPCookies.TabIndex = 8;
            this.listViewHTTPCookies.UseCompatibleStateImageBehavior = false;
            this.listViewHTTPCookies.View = System.Windows.Forms.View.Details;
            this.listViewHTTPCookies.SelectedIndexChanged += new System.EventHandler(this.listViewHTTPCookies_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 235;
            // 
            // tabPageTLS
            // 
            this.tabPageTLS.Controls.Add(this.groupBox3);
            this.tabPageTLS.Controls.Add(this.label1);
            this.tabPageTLS.Controls.Add(this.groupBox1);
            this.tabPageTLS.Location = new System.Drawing.Point(4, 22);
            this.tabPageTLS.Name = "tabPageTLS";
            this.tabPageTLS.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTLS.Size = new System.Drawing.Size(781, 49);
            this.tabPageTLS.TabIndex = 5;
            this.tabPageTLS.Text = "TLS/SSL";
            this.tabPageTLS.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxIgnoreCertErrors);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(150, 37);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Server Certificate";
            // 
            // checkBoxIgnoreCertErrors
            // 
            this.checkBoxIgnoreCertErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIgnoreCertErrors.AutoSize = true;
            this.checkBoxIgnoreCertErrors.Location = new System.Drawing.Point(6, 15);
            this.checkBoxIgnoreCertErrors.Name = "checkBoxIgnoreCertErrors";
            this.checkBoxIgnoreCertErrors.Size = new System.Drawing.Size(135, 17);
            this.checkBoxIgnoreCertErrors.TabIndex = 11;
            this.checkBoxIgnoreCertErrors.Text = "Ignore Validation Errors";
            this.toolTips.SetToolTip(this.checkBoxIgnoreCertErrors, "SSL certificates that fail validation will still be accepted (USE WITH CARE)");
            this.checkBoxIgnoreCertErrors.UseVisualStyleBackColor = true;
            this.checkBoxIgnoreCertErrors.CheckedChanged += new System.EventHandler(this.checkBoxIgnoreCertErrors_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(398, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SSL 3.0 will be enabled if no TLS version is selected, otherwise SSL 3.0 is disab" +
    "led";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxTLS1_2);
            this.groupBox1.Controls.Add(this.checkBoxTLS1_1);
            this.groupBox1.Controls.Add(this.checkBoxTLS1_0);
            this.groupBox1.Location = new System.Drawing.Point(162, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 37);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TLS Versions Accepted";
            // 
            // checkBoxTLS1_2
            // 
            this.checkBoxTLS1_2.AutoSize = true;
            this.checkBoxTLS1_2.Checked = true;
            this.checkBoxTLS1_2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTLS1_2.Location = new System.Drawing.Point(99, 15);
            this.checkBoxTLS1_2.Name = "checkBoxTLS1_2";
            this.checkBoxTLS1_2.Size = new System.Drawing.Size(41, 17);
            this.checkBoxTLS1_2.TabIndex = 2;
            this.checkBoxTLS1_2.Text = "1.2";
            this.checkBoxTLS1_2.UseVisualStyleBackColor = true;
            // 
            // checkBoxTLS1_1
            // 
            this.checkBoxTLS1_1.AutoSize = true;
            this.checkBoxTLS1_1.Checked = true;
            this.checkBoxTLS1_1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTLS1_1.Location = new System.Drawing.Point(53, 15);
            this.checkBoxTLS1_1.Name = "checkBoxTLS1_1";
            this.checkBoxTLS1_1.Size = new System.Drawing.Size(41, 17);
            this.checkBoxTLS1_1.TabIndex = 1;
            this.checkBoxTLS1_1.Text = "1.1";
            this.checkBoxTLS1_1.UseVisualStyleBackColor = true;
            // 
            // checkBoxTLS1_0
            // 
            this.checkBoxTLS1_0.AutoSize = true;
            this.checkBoxTLS1_0.Checked = true;
            this.checkBoxTLS1_0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTLS1_0.Location = new System.Drawing.Point(6, 15);
            this.checkBoxTLS1_0.Name = "checkBoxTLS1_0";
            this.checkBoxTLS1_0.Size = new System.Drawing.Size(41, 17);
            this.checkBoxTLS1_0.TabIndex = 0;
            this.checkBoxTLS1_0.Text = "1.0";
            this.checkBoxTLS1_0.UseVisualStyleBackColor = true;
            // 
            // tabPageLogging
            // 
            this.tabPageLogging.Controls.Add(this.buttonViewOtherLog);
            this.tabPageLogging.Controls.Add(this.buttonViewLogFile);
            this.tabPageLogging.Controls.Add(this.buttonCreateNewLogFile);
            this.tabPageLogging.Controls.Add(this.label3);
            this.tabPageLogging.Controls.Add(this.textBoxLogFileName);
            this.tabPageLogging.Controls.Add(this.buttonBrowseLogFolder);
            this.tabPageLogging.Controls.Add(this.label2);
            this.tabPageLogging.Controls.Add(this.textBoxLogFolder);
            this.tabPageLogging.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogging.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageLogging.Name = "tabPageLogging";
            this.tabPageLogging.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageLogging.Size = new System.Drawing.Size(781, 49);
            this.tabPageLogging.TabIndex = 6;
            this.tabPageLogging.Text = "Logging";
            this.tabPageLogging.UseVisualStyleBackColor = true;
            // 
            // buttonViewOtherLog
            // 
            this.buttonViewOtherLog.Location = new System.Drawing.Point(594, 26);
            this.buttonViewOtherLog.Margin = new System.Windows.Forms.Padding(2);
            this.buttonViewOtherLog.Name = "buttonViewOtherLog";
            this.buttonViewOtherLog.Size = new System.Drawing.Size(183, 21);
            this.buttonViewOtherLog.TabIndex = 7;
            this.buttonViewOtherLog.Text = "Open other log in Log Viewer...";
            this.buttonViewOtherLog.UseVisualStyleBackColor = true;
            this.buttonViewOtherLog.Click += new System.EventHandler(this.buttonViewOtherLog_Click);
            // 
            // buttonViewLogFile
            // 
            this.buttonViewLogFile.Location = new System.Drawing.Point(324, 26);
            this.buttonViewLogFile.Margin = new System.Windows.Forms.Padding(2);
            this.buttonViewLogFile.Name = "buttonViewLogFile";
            this.buttonViewLogFile.Size = new System.Drawing.Size(121, 21);
            this.buttonViewLogFile.TabIndex = 6;
            this.buttonViewLogFile.Text = "Open in Log Viewer...";
            this.buttonViewLogFile.UseVisualStyleBackColor = true;
            this.buttonViewLogFile.Click += new System.EventHandler(this.buttonViewLogFile_Click);
            // 
            // buttonCreateNewLogFile
            // 
            this.buttonCreateNewLogFile.Location = new System.Drawing.Point(242, 26);
            this.buttonCreateNewLogFile.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCreateNewLogFile.Name = "buttonCreateNewLogFile";
            this.buttonCreateNewLogFile.Size = new System.Drawing.Size(78, 21);
            this.buttonCreateNewLogFile.TabIndex = 5;
            this.buttonCreateNewLogFile.Text = "Create new...";
            this.buttonCreateNewLogFile.UseVisualStyleBackColor = true;
            this.buttonCreateNewLogFile.Click += new System.EventHandler(this.buttonCreateNewLogFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Log file:";
            // 
            // textBoxLogFileName
            // 
            this.textBoxLogFileName.Location = new System.Drawing.Point(64, 27);
            this.textBoxLogFileName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLogFileName.Name = "textBoxLogFileName";
            this.textBoxLogFileName.ReadOnly = true;
            this.textBoxLogFileName.Size = new System.Drawing.Size(175, 20);
            this.textBoxLogFileName.TabIndex = 3;
            // 
            // buttonBrowseLogFolder
            // 
            this.buttonBrowseLogFolder.Location = new System.Drawing.Point(654, 4);
            this.buttonBrowseLogFolder.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBrowseLogFolder.Name = "buttonBrowseLogFolder";
            this.buttonBrowseLogFolder.Size = new System.Drawing.Size(123, 21);
            this.buttonBrowseLogFolder.TabIndex = 2;
            this.buttonBrowseLogFolder.Text = "Open in File Explorer...";
            this.buttonBrowseLogFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseLogFolder.Click += new System.EventHandler(this.buttonBrowseLogFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Log folder:";
            // 
            // textBoxLogFolder
            // 
            this.textBoxLogFolder.Location = new System.Drawing.Point(64, 5);
            this.textBoxLogFolder.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLogFolder.Name = "textBoxLogFolder";
            this.textBoxLogFolder.ReadOnly = true;
            this.textBoxLogFolder.Size = new System.Drawing.Size(586, 20);
            this.textBoxLogFolder.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 607);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(810, 376);
            this.Name = "FormMain";
            this.Text = "SOAPe";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxResponse.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageUrl.ResumeLayout(false);
            this.tabPageUrl.PerformLayout();
            this.tabPageAuth.ResumeLayout(false);
            this.tabPageAuth.PerformLayout();
            this.tabPageEWSHeader.ResumeLayout(false);
            this.tabPageEWSHeader.PerformLayout();
            this.tabPageHTTPHeaders.ResumeLayout(false);
            this.tabPageHTTPHeaders.PerformLayout();
            this.tabPageCookies.ResumeLayout(false);
            this.tabPageCookies.PerformLayout();
            this.tabPageTLS.ResumeLayout(false);
            this.tabPageTLS.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageLogging.ResumeLayout(false);
            this.tabPageLogging.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonLoadTemplate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTTPListenerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logViewerToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxPersistCookies;
        private XmlEditor xmlEditorResponse;
        private XmlEditor xmlEditorRequest;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageUrl;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.CheckBox checkBoxBypassProxySettings;
        private System.Windows.Forms.TabPage tabPageAuth;
        private System.Windows.Forms.TextBox textBoxDomain;
        private System.Windows.Forms.Label labelDomain;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.RadioButton radioButtonSpecificCredentials;
        private System.Windows.Forms.RadioButton radioButtonDefaultCredentials;
        private System.Windows.Forms.TabPage tabPageEWSHeader;
        private System.Windows.Forms.CheckBox checkBoxUpdateEWSHeader;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxImpersonationSID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxRequestServerVersion;
        private System.Windows.Forms.TabPage tabPageHTTPHeaders;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonHTTPHeadersClear;
        private System.Windows.Forms.Button buttonHTTPHeaderRemove;
        private System.Windows.Forms.Button buttonHTTPHeaderAdd;
        private System.Windows.Forms.TextBox textBoxHTTPHeaderValue;
        private System.Windows.Forms.TextBox textBoxHTTPHeaderName;
        private System.Windows.Forms.ListView listViewHTTPHeaders;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderValue;
        private System.Windows.Forms.ComboBox comboBoxImpersonationMethod;
        private System.Windows.Forms.TabPage tabPageCookies;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonHTTPCookiesClear;
        private System.Windows.Forms.Button buttonHTTPCookieRemove;
        private System.Windows.Forms.Button buttonHTTPCookieAdd;
        private System.Windows.Forms.TextBox textBoxHTTPCookieValue;
        private System.Windows.Forms.TextBox textBoxHTTPCookieName;
        private System.Windows.Forms.ListView listViewHTTPCookies;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButtonOAuth;
        private System.Windows.Forms.Label labelOAuthToken;
        private System.Windows.Forms.TextBox textBoxOAuthToken;
        private System.Windows.Forms.RadioButton radioButtonUrlCustom;
        private System.Windows.Forms.RadioButton radioButtonUrlOffice365;
        private GroupBoxHighlight groupBoxResponse;
        private System.Windows.Forms.RadioButton radioButtonNoAuth;
        private System.Windows.Forms.TabPage tabPageTLS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxTLS1_2;
        private System.Windows.Forms.CheckBox checkBoxTLS1_1;
        private System.Windows.Forms.CheckBox checkBoxTLS1_0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonCertificateAuthentication;
        private System.Windows.Forms.Button buttonChooseCertificate;
        private System.Windows.Forms.TextBox textBoxAuthCertificate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxIgnoreCertErrors;
        private System.Windows.Forms.TabPage tabPageLogging;
        private System.Windows.Forms.Button buttonViewLogFile;
        private System.Windows.Forms.Button buttonCreateNewLogFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLogFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLogFolder;
        private System.Windows.Forms.Button buttonBrowseLogFolder;
        private System.Windows.Forms.Button buttonAcquireOAuthToken;
        private System.Windows.Forms.Button buttonViewOtherLog;
        private System.Windows.Forms.Button buttonAppRegistration;
        private System.Windows.Forms.Button buttonUpdateEWSHeader;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem getFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAutoDiscover;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConvertId;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem GetFolderInboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GetFolderCalendarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GetFolderContactsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FindItemInboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FindItemContactsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GetFolderByIdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GetItemByIdToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ConfigurationManagerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem releaseNotesToolStripMenuItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxFollowRedirect;
    }
}

