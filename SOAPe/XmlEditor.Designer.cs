namespace SOAPe
{
    partial class XmlEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.richTextBoxXml = new System.Windows.Forms.RichTextBox();
            this.contextMenuLogXml = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectXMLElementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectXmlValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectXmlAttributeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyItemIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyItemIdwithoutChangeKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyItemIdIdOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SendItemIdToTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addExtendedPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.syntaxHighlightingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xmlFormattingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateXmlincludingCharacterTestingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuLogXml.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxXml
            // 
            this.richTextBoxXml.ContextMenuStrip = this.contextMenuLogXml;
            this.richTextBoxXml.DetectUrls = false;
            this.richTextBoxXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxXml.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxXml.Name = "richTextBoxXml";
            this.richTextBoxXml.Size = new System.Drawing.Size(150, 150);
            this.richTextBoxXml.TabIndex = 0;
            this.richTextBoxXml.Text = "";
            this.richTextBoxXml.TextChanged += new System.EventHandler(this.richTextBoxXml_TextChanged);
            this.richTextBoxXml.MouseUp += new System.Windows.Forms.MouseEventHandler(this.richTextBoxXml_MouseUp);
            // 
            // contextMenuLogXml
            // 
            this.contextMenuLogXml.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuLogXml.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectXMLElementToolStripMenuItem,
            this.selectXmlValueToolStripMenuItem,
            this.selectXmlAttributeToolStripMenuItem,
            this.toolStripSeparator1,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.PasteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyItemIdToolStripMenuItem,
            this.copyItemIdwithoutChangeKeyToolStripMenuItem,
            this.copyItemIdIdOnlyToolStripMenuItem,
            this.SendItemIdToTemplateToolStripMenuItem,
            this.addExtendedPropertyToolStripMenuItem,
            this.toolStripSeparator2,
            this.syntaxHighlightingToolStripMenuItem,
            this.xmlFormattingToolStripMenuItem,
            this.validateXmlincludingCharacterTestingToolStripMenuItem});
            this.contextMenuLogXml.Name = "contextMenuLogXml";
            this.contextMenuLogXml.Size = new System.Drawing.Size(286, 352);
            this.contextMenuLogXml.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuLogXml_Opening);
            // 
            // selectXMLElementToolStripMenuItem
            // 
            this.selectXMLElementToolStripMenuItem.Name = "selectXMLElementToolStripMenuItem";
            this.selectXMLElementToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.selectXMLElementToolStripMenuItem.Text = "Select XML Element";
            this.selectXMLElementToolStripMenuItem.Click += new System.EventHandler(this.selectXMLElementToolStripMenuItem_Click);
            // 
            // selectXmlValueToolStripMenuItem
            // 
            this.selectXmlValueToolStripMenuItem.Name = "selectXmlValueToolStripMenuItem";
            this.selectXmlValueToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.selectXmlValueToolStripMenuItem.Text = "Select Xml Value";
            this.selectXmlValueToolStripMenuItem.Click += new System.EventHandler(this.selectXmlValueToolStripMenuItem_Click);
            // 
            // selectXmlAttributeToolStripMenuItem
            // 
            this.selectXmlAttributeToolStripMenuItem.Name = "selectXmlAttributeToolStripMenuItem";
            this.selectXmlAttributeToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.selectXmlAttributeToolStripMenuItem.Text = "Select Xml Attribute";
            this.selectXmlAttributeToolStripMenuItem.Click += new System.EventHandler(this.selectXmlAttributeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(282, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // PasteToolStripMenuItem
            // 
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.PasteToolStripMenuItem.Text = "Paste";
            this.PasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(282, 6);
            // 
            // copyItemIdToolStripMenuItem
            // 
            this.copyItemIdToolStripMenuItem.Name = "copyItemIdToolStripMenuItem";
            this.copyItemIdToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.copyItemIdToolStripMenuItem.Text = "Copy ItemId";
            this.copyItemIdToolStripMenuItem.Click += new System.EventHandler(this.copyItemIdToolStripMenuItem_Click);
            // 
            // copyItemIdwithoutChangeKeyToolStripMenuItem
            // 
            this.copyItemIdwithoutChangeKeyToolStripMenuItem.Name = "copyItemIdwithoutChangeKeyToolStripMenuItem";
            this.copyItemIdwithoutChangeKeyToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.copyItemIdwithoutChangeKeyToolStripMenuItem.Text = "Copy ItemId (without ChangeKey)";
            this.copyItemIdwithoutChangeKeyToolStripMenuItem.Click += new System.EventHandler(this.copyItemIdwithoutChangeKeyToolStripMenuItem_Click);
            // 
            // copyItemIdIdOnlyToolStripMenuItem
            // 
            this.copyItemIdIdOnlyToolStripMenuItem.Name = "copyItemIdIdOnlyToolStripMenuItem";
            this.copyItemIdIdOnlyToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.copyItemIdIdOnlyToolStripMenuItem.Text = "Copy ItemId (Id only)";
            this.copyItemIdIdOnlyToolStripMenuItem.Click += new System.EventHandler(this.copyItemIdIdOnlyToolStripMenuItem_Click);
            // 
            // SendItemIdToTemplateToolStripMenuItem
            // 
            this.SendItemIdToTemplateToolStripMenuItem.Name = "SendItemIdToTemplateToolStripMenuItem";
            this.SendItemIdToTemplateToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.SendItemIdToTemplateToolStripMenuItem.Text = "Send ItemId to Template...";
            this.SendItemIdToTemplateToolStripMenuItem.Visible = false;
            this.SendItemIdToTemplateToolStripMenuItem.Click += new System.EventHandler(this.SendItemIdToTemplateToolStripMenuItem_Click);
            // 
            // addExtendedPropertyToolStripMenuItem
            // 
            this.addExtendedPropertyToolStripMenuItem.Name = "addExtendedPropertyToolStripMenuItem";
            this.addExtendedPropertyToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.addExtendedPropertyToolStripMenuItem.Text = "Add Extended Property...";
            this.addExtendedPropertyToolStripMenuItem.Click += new System.EventHandler(this.addExtendedPropertyToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(282, 6);
            // 
            // syntaxHighlightingToolStripMenuItem
            // 
            this.syntaxHighlightingToolStripMenuItem.CheckOnClick = true;
            this.syntaxHighlightingToolStripMenuItem.Name = "syntaxHighlightingToolStripMenuItem";
            this.syntaxHighlightingToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.syntaxHighlightingToolStripMenuItem.Text = "Syntax Highlighting";
            this.syntaxHighlightingToolStripMenuItem.Click += new System.EventHandler(this.syntaxHighlightingToolStripMenuItem_Click);
            // 
            // xmlFormattingToolStripMenuItem
            // 
            this.xmlFormattingToolStripMenuItem.CheckOnClick = true;
            this.xmlFormattingToolStripMenuItem.Name = "xmlFormattingToolStripMenuItem";
            this.xmlFormattingToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.xmlFormattingToolStripMenuItem.Text = "Xml Formatting (Indent)";
            this.xmlFormattingToolStripMenuItem.Click += new System.EventHandler(this.xmlFormattingToolStripMenuItem_Click);
            // 
            // validateXmlincludingCharacterTestingToolStripMenuItem
            // 
            this.validateXmlincludingCharacterTestingToolStripMenuItem.Name = "validateXmlincludingCharacterTestingToolStripMenuItem";
            this.validateXmlincludingCharacterTestingToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.validateXmlincludingCharacterTestingToolStripMenuItem.Text = "Validate Xml (includes character testing)";
            this.validateXmlincludingCharacterTestingToolStripMenuItem.Click += new System.EventHandler(this.validateXmlincludingCharacterTestingToolStripMenuItem_Click);
            // 
            // XmlEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.richTextBoxXml);
            this.Name = "XmlEditor";
            this.BackColorChanged += new System.EventHandler(this.XmlEditor_BackColorChanged);
            this.ForeColorChanged += new System.EventHandler(this.XmlEditor_ForeColorChanged);
            this.contextMenuLogXml.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxXml;
        private System.Windows.Forms.ContextMenuStrip contextMenuLogXml;
        private System.Windows.Forms.ToolStripMenuItem selectXMLElementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyItemIdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyItemIdwithoutChangeKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyItemIdIdOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem syntaxHighlightingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xmlFormattingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectXmlValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectXmlAttributeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SendItemIdToTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateXmlincludingCharacterTestingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addExtendedPropertyToolStripMenuItem;
    }
}
