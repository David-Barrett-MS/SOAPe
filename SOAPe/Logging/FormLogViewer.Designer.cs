namespace SOAPe
{
    partial class FormLogViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogViewer));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewLogIndex = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xmlEditor1 = new SOAPe.XmlEditor();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.buttonLoadLogFolder = new System.Windows.Forms.Button();
            this.statusPercentBar1 = new SOAPe.StatusPercentBar();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.buttonReload = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewLogIndex);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.xmlEditor1);
            this.splitContainer1.Size = new System.Drawing.Size(1144, 428);
            this.splitContainer1.SplitterDistance = 529;
            this.splitContainer1.TabIndex = 0;
            // 
            // listViewLogIndex
            // 
            this.listViewLogIndex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listViewLogIndex.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewLogIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLogIndex.FullRowSelect = true;
            this.listViewLogIndex.GridLines = true;
            this.listViewLogIndex.HideSelection = false;
            this.listViewLogIndex.Location = new System.Drawing.Point(0, 0);
            this.listViewLogIndex.Name = "listViewLogIndex";
            this.listViewLogIndex.Size = new System.Drawing.Size(529, 428);
            this.listViewLogIndex.TabIndex = 0;
            this.listViewLogIndex.Tag = "Time ASC,Tid ASC";
            this.listViewLogIndex.UseCompatibleStateImageBehavior = false;
            this.listViewLogIndex.View = System.Windows.Forms.View.Details;
            this.listViewLogIndex.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewLogIndex_ColumnClick);
            this.listViewLogIndex.SelectedIndexChanged += new System.EventHandler(this.listViewLogIndex_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date/Time";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tid";
            this.columnHeader3.Width = 40;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "SOAP Method/Error";
            this.columnHeader4.Width = 115;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Size";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Mailbox";
            this.columnHeader6.Width = 150;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Impersonating";
            this.columnHeader7.Width = 150;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(138, 26);
            // 
            // deleteEntryToolStripMenuItem
            // 
            this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
            this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.deleteEntryToolStripMenuItem.Text = "Delete entry";
            this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.deleteEntryToolStripMenuItem_Click);
            // 
            // xmlEditor1
            // 
            this.xmlEditor1.BackColor = System.Drawing.SystemColors.Window;
            this.xmlEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmlEditor1.IndentXml = true;
            this.xmlEditor1.Location = new System.Drawing.Point(0, 0);
            this.xmlEditor1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xmlEditor1.Name = "xmlEditor1";
            this.xmlEditor1.ReadOnly = true;
            this.xmlEditor1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2057{\\fonttbl{\\f0\\fnil\\fcharset0 " +
    "Microsoft Sans Serif;}}\r\n{\\*\\generator Riched20 10.0.22000}\\viewkind4\\uc1 \r\n\\par" +
    "d\\f0\\fs17\\par\r\n}\r\n";
            this.xmlEditor1.SelectionLength = 0;
            this.xmlEditor1.SelectionStart = 0;
            this.xmlEditor1.SendItemIdToTemplateEnabled = false;
            this.xmlEditor1.Size = new System.Drawing.Size(611, 428);
            this.xmlEditor1.SyntaxHighlight = true;
            this.xmlEditor1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSaveAs);
            this.panel1.Controls.Add(this.buttonLoadLogFolder);
            this.panel1.Controls.Add(this.statusPercentBar1);
            this.panel1.Controls.Add(this.buttonFilter);
            this.panel1.Controls.Add(this.buttonReload);
            this.panel1.Controls.Add(this.buttonLoad);
            this.panel1.Controls.Add(this.buttonClearLog);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 430);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1144, 32);
            this.panel1.TabIndex = 1;
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.Location = new System.Drawing.Point(353, 3);
            this.buttonSaveAs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(64, 23);
            this.buttonSaveAs.TabIndex = 13;
            this.buttonSaveAs.Text = "Save As...";
            this.toolTip1.SetToolTip(this.buttonSaveAs, "Save displayed log (or optionally, only those trace elements currently selected)." +
        "");
            this.buttonSaveAs.UseVisualStyleBackColor = true;
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // buttonLoadLogFolder
            // 
            this.buttonLoadLogFolder.Location = new System.Drawing.Point(246, 3);
            this.buttonLoadLogFolder.Name = "buttonLoadLogFolder";
            this.buttonLoadLogFolder.Size = new System.Drawing.Size(102, 23);
            this.buttonLoadLogFolder.TabIndex = 11;
            this.buttonLoadLogFolder.Text = "Load log folder...";
            this.toolTip1.SetToolTip(this.buttonLoadLogFolder, "Attempt to load all files found in a specific folder as EWS traces.");
            this.buttonLoadLogFolder.UseVisualStyleBackColor = true;
            this.buttonLoadLogFolder.Click += new System.EventHandler(this.buttonLoadLogFolder_Click);
            // 
            // statusPercentBar1
            // 
            this.statusPercentBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusPercentBar1.BarColour = System.Drawing.Color.PaleGreen;
            this.statusPercentBar1.Location = new System.Drawing.Point(504, 4);
            this.statusPercentBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.statusPercentBar1.Name = "statusPercentBar1";
            this.statusPercentBar1.PercentComplete = 0D;
            this.statusPercentBar1.Size = new System.Drawing.Size(556, 21);
            this.statusPercentBar1.Status = "";
            this.statusPercentBar1.TabIndex = 10;
            this.statusPercentBar1.Visible = false;
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(422, 3);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(75, 23);
            this.buttonFilter.TabIndex = 9;
            this.buttonFilter.Text = "Filter...";
            this.toolTip1.SetToolTip(this.buttonFilter, "Apply a filter.");
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // buttonReload
            // 
            this.buttonReload.Location = new System.Drawing.Point(84, 3);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(75, 23);
            this.buttonReload.TabIndex = 5;
            this.buttonReload.Text = "Reload log";
            this.toolTip1.SetToolTip(this.buttonReload, "Reload the current log from disk.");
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(165, 3);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 4;
            this.buttonLoad.Text = "Load log...";
            this.toolTip1.SetToolTip(this.buttonLoad, "Load log file (replaces currently displayed log).");
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(3, 3);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(75, 23);
            this.buttonClearLog.TabIndex = 1;
            this.buttonClearLog.Text = "Clear log";
            this.toolTip1.SetToolTip(this.buttonClearLog, "Remove all logs from display.  Doesn\'t affect log files themselves.");
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(1066, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // FormLogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 462);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(814, 282);
            this.Name = "FormLogViewer";
            this.Text = "Log Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLogViewer_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ListView listViewLogIndex;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonClearLog;
        private XmlEditor xmlEditor1;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonFilter;
        private StatusPercentBar statusPercentBar1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button buttonLoadLogFolder;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button buttonSaveAs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}