/*
 * By David Barrett, Microsoft Ltd. 2016. Use at your own risk.  No warranties are given.
 * 
 * DISCLAIMER:
 * THIS CODE IS SAMPLE CODE. THESE SAMPLES ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND.
 * MICROSOFT FURTHER DISCLAIMS ALL IMPLIED WARRANTIES INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OF MERCHANTABILITY OR OF FITNESS FOR
 * A PARTICULAR PURPOSE. THE ENTIRE RISK ARISING OUT OF THE USE OR PERFORMANCE OF THE SAMPLES REMAINS WITH YOU. IN NO EVENT SHALL
 * MICROSOFT OR ITS SUPPLIERS BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS,
 * BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR OTHER PECUNIARY LOSS) ARISING OUT OF THE USE OF OR INABILITY TO USE THE
 * SAMPLES, EVEN IF MICROSOFT HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. BECAUSE SOME STATES DO NOT ALLOW THE EXCLUSION OR LIMITATION
 * OF LIABILITY FOR CONSEQUENTIAL OR INCIDENTAL DAMAGES, THE ABOVE LIMITATION MAY NOT APPLY TO YOU.
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace SOAPe
{
    public partial class FormLogViewer : Form
    {
        private ClassLogger _logger = null;
        private ClassSyntaxHighlighter _syntaxHighlighter;
        private ListViewItem _lastSelectedItem = null;
        private bool _checkingForErrors = false;
        private string[] _filters = null;
        private bool _haveLoadedLog = false;
        private bool _suppressUIUpdates = false;
        private const string _fileFilter = "Log files (*.log)|*.log|XML files (*.xml)|*.xml|Text files (*.txt)|*.txt|Trace files (*.trace)|*.trace|All Files|*.*";


        public FormLogViewer()
        {
            // If we are opening without a Logger, then we prompt to open log file
            InitializeComponent();

            string logFile = OpenExistingLogFile();
            if (String.IsNullOrEmpty(logFile))
            {
                this.Close();
                return;
            }

            listViewLogIndex.Items.Clear();
            xmlEditor1.Text = "Please select a log to view.";
            this.Text = String.Format("Log Viewer - {0}", logFile);
            ShowStatus("Processing...");
            ThreadPool.QueueUserWorkItem(new WaitCallback(LoadLogFile), logFile);
            _syntaxHighlighter = new ClassSyntaxHighlighter(xmlEditor1);
            _filters = new string[0];
        }

        public FormLogViewer(ClassLogger Logger)
        {
            InitializeComponent();
            _logger = Logger;
            _logger.LogAdded += _logger_LogAdded;
            _logger.ProgressChanged += _logger_ProgressChanged;
            _syntaxHighlighter = new ClassSyntaxHighlighter(xmlEditor1);
            _filters = new string[0];
            UpdateView();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ShowLogList), null);
            statusPercentBar1.PercentComplete = 0;
            statusPercentBar1.Visible = false;
            listViewLogIndex.ContextMenuStrip = contextMenuStrip1;
            ToggleButtons(true);
        }

        void _logger_ProgressChanged(object sender, ProgressEventArgs e)
        {
            ShowStatus(e.Progress, (double)e.PercentComplete);
        }


        public bool HaveLoadedLog
        {
            get { return _haveLoadedLog; }
        }

        private void ShowLogList(object e)
        {
            ShowLogIndex();
        }

        private string GetLogFilter()
        {
            // Build the filter for the DataTable

            if (_filters == null)
                return String.Empty;
            if (_filters.Length < 1)
                return String.Empty;

            StringBuilder sFilter = new StringBuilder();
            for (int i = 1; i < _filters.Length; i++)
            {
                if (i > 1)
                    sFilter.Append(_filters[0]);
                sFilter.Append(_filters[i]);
            }
            return sFilter.ToString();
        }

        private void ShowLogIndex()
        {
            // Read the logs and update the UI

            if (_logger.LogDataTable == null)
                return;

            int iTraceCount = _logger.LogDataTable.Rows.Count;

            if (iTraceCount==0)
                return;

            string sOrderBy = (String)listViewLogIndex.Tag;
            if (String.IsNullOrEmpty(sOrderBy))
                sOrderBy = "Time ASC,Tid ASC";
            DataRow[] logRows = null;
            string sFilter = GetLogFilter();
            try
            {
                logRows = _logger.LogDataTable.Select(sFilter, sOrderBy);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error querying logs, please check filter{0}Error: {1}", Environment.NewLine, ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.DebugLog(String.Format("Error retrieving logs rows, filter: {0}", sFilter));
                _logger.DebugLogError(ex);
                return;
            }
            
            xmlEditor1.Text = "No data found for this session.";

            int i=-1;
            if (listViewLogIndex.InvokeRequired)
            {
                listViewLogIndex.Invoke(new MethodInvoker(delegate()
                {
                    listViewLogIndex.BeginUpdate();
                    listViewLogIndex.Items.Clear();
                }));
            }
            else
            {
                listViewLogIndex.BeginUpdate();
                listViewLogIndex.Items.Clear();
            }

            iTraceCount = logRows.Length;

            while (i < iTraceCount-1)
            {
                ShowStatus(String.Format("Processing {0} of {1}", i++, iTraceCount), ((double)i / (double)iTraceCount)*100);
                ListViewItem item = new ListViewItem(logRows[i]["Time"].ToString());
                item.Tag = logRows[i]["Data"].ToString();
                StringBuilder sDescription = new StringBuilder(logRows[i]["Tag"].ToString());
                item.SubItems.Add(sDescription.ToString());
                item.SubItems.Add(logRows[i]["Tid"].ToString());
                item.SubItems.Add(logRows[i]["SOAPMethod"].ToString());
                item.SubItems.Add(((Int64)logRows[i]["Size"]).ToString("0,0"));
                item.SubItems.Add(logRows[i]["Mailbox"].ToString());
                item.SubItems.Add(logRows[i]["ExchangeImpersonation"].ToString());
                if (listViewLogIndex.InvokeRequired)
                {
                    listViewLogIndex.Invoke(new MethodInvoker(delegate()
                    {
                        listViewLogIndex.Items.Add(item);
                    }));
                }
                else
                    listViewLogIndex.Items.Add(item);
            }
            if (listViewLogIndex.InvokeRequired)
            {
                listViewLogIndex.Invoke(new MethodInvoker(delegate()
                {
                    listViewLogIndex.EndUpdate();
                }));
            }
            else
                listViewLogIndex.EndUpdate();

            if (String.IsNullOrEmpty(sFilter))
            {
                ShowStatus(null);
            }
            else
                ShowStatus("Filter Active", 100, Color.MintCream);
            ThreadPool.QueueUserWorkItem(new WaitCallback(AnalyzeTrace), null);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void _logger_LogAdded(object sender, LogAddedEventArgs e)
        {
            // New log added

            ListViewItem oItem = new ListViewItem(e.Trace.TraceTime.ToString());
            string sTag = "";
            try
            {
                sTag = ClassLogger.ReadMethodFromRequest(e.Trace.Data);
            }
            catch { }
            oItem.Tag = e.Trace.Data;
            oItem.SubItems.Add(e.Trace.TraceTag);
            oItem.SubItems.Add(e.Trace.TraceThreadId.ToString());
            oItem.SubItems.Add(sTag);

            if (e.Trace.IsErrorResponse)
            {
                oItem.BackColor = Color.Red;
            }

            if (listViewLogIndex.InvokeRequired)
            {
                listViewLogIndex.Invoke(new MethodInvoker(delegate() { listViewLogIndex.Items.Add(oItem); }));
            }
            else
                listViewLogIndex.Items.Add(oItem);
            
        }

        private void listViewLogIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressUIUpdates)
                return;

            string sDetails = "Please select a log to view.";
            if (listViewLogIndex.FocusedItem != null)
            {
                if (listViewLogIndex.FocusedItem.Equals(_lastSelectedItem))
                    return;
                sDetails = "";
                try
                {
                    sDetails = (string)listViewLogIndex.FocusedItem.Tag;
                }
                catch { }
                _lastSelectedItem = listViewLogIndex.FocusedItem;
            }
            else
                _lastSelectedItem = null;
            xmlEditor1.Text = sDetails;
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            _logger.ClearHistory();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ShowLogList), null);
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            listViewLogIndex.Items.Clear();
            xmlEditor1.Text = "Please select a log to view.";
            ShowStatus("Processing...");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ReloadLogFile), null);
        }

        private string OpenExistingLogFile()
        {
            OpenFileDialog oDialog = new OpenFileDialog();
            oDialog.Filter = _fileFilter;
            oDialog.DefaultExt = "log";
            oDialog.Title = "Select log/trace file to open";
            oDialog.CheckFileExists = true;
            if (oDialog.ShowDialog() != DialogResult.OK)
                return String.Empty;

            return oDialog.FileName;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            string loadLogFilename = OpenExistingLogFile();
            if (String.IsNullOrEmpty(loadLogFilename))
                return;

            listViewLogIndex.Items.Clear();
            xmlEditor1.Text = "Please select a log to view.";
            this.Text = String.Format("Log Viewer - {0}", loadLogFilename);
            ShowStatus("Processing...");
            ThreadPool.QueueUserWorkItem(new WaitCallback(LoadLogFile), loadLogFilename);
        }

        private static void ToggleButton(bool Enabled, Control Button)
        {
            if (Button.InvokeRequired)
            {
                Button.Invoke(new MethodInvoker(delegate() { Button.Enabled = Enabled; }));
            }
            else
                Button.Enabled = Enabled;
        }

        private void ToggleButtons(bool Enabled)
        {
            ToggleButton(Enabled, buttonClearLog);
            ToggleButton(Enabled, buttonLoad);
            ToggleButton(Enabled, buttonLoadLogFolder);
            ToggleButton(Enabled, buttonReload);
            ToggleButton(Enabled, buttonFilter);
        }

        private void ShowStatus(string Status, double PercentComplete = 0, Color? Colour = null)
        {
            // Update the status information
            if (String.IsNullOrEmpty(Status))
            {
                // No status, so hide the control
                if (statusPercentBar1.InvokeRequired)
                {
                    statusPercentBar1.Invoke(new MethodInvoker(delegate()
                    {
                        statusPercentBar1.Status = "";
                        statusPercentBar1.PercentComplete = PercentComplete;
                        statusPercentBar1.Visible = false;
                    }));
                }
                else
                {
                    statusPercentBar1.Status = "";
                    statusPercentBar1.PercentComplete = PercentComplete;
                    statusPercentBar1.Visible = false;
                }
                return;
            }

            // Show the status
            if (Colour == null)
                Colour = Color.PaleGreen;
            if (statusPercentBar1.InvokeRequired)
            {
                statusPercentBar1.Invoke(new MethodInvoker(delegate()
                {
                    statusPercentBar1.Status = Status;
                    statusPercentBar1.PercentComplete = PercentComplete;
                    statusPercentBar1.BarColour = (Color)Colour; 
                    statusPercentBar1.Visible = true;
                }));
            }
            else
            {
                statusPercentBar1.Status = Status;
                statusPercentBar1.PercentComplete = PercentComplete;
                statusPercentBar1.BarColour = (Color)Colour;
                statusPercentBar1.Visible = true;
            }
        }

        private void LoadLogFile(object e)
        {
            ToggleButtons(false);
            // Unsubscribe from events of current logger
            if (_logger != null)
            {
                _logger.LogAdded -= _logger_LogAdded;
                _logger.ProgressChanged -= _logger_ProgressChanged;
            }

            // Create new logger so as not to interfere with SOAPe's log
            _logger = new ClassLogger((string)e, false);
            _logger.LogAdded += _logger_LogAdded;
            _logger.ProgressChanged += _logger_ProgressChanged;
            _haveLoadedLog = true;
            _logger.LoadLogFile((string)e);
            ShowLogIndex();
            ToggleButtons(true);
            ShowStatus(null);
        }

        private void ReloadLogFile(object e)
        {
            ToggleButtons(false);
            _logger.LoadPreviousTrace();
            ShowLogIndex();
            ToggleButtons(true);
            ShowStatus(null);
        }


        private void AnalyzeTrace(object o)
        {
            // Go through the listbox items and check content for errors
            // This method is intended to run on a background thread, as the checking can take a significant length of time

            if (this._checkingForErrors)
                return;

            this._checkingForErrors = true;

            int i = 0;
            if (listViewLogIndex.InvokeRequired)
            {
                listViewLogIndex.Invoke(new MethodInvoker(delegate() {
                    while (i < listViewLogIndex.Items.Count)
                    {
                        TraceElement trace = new TraceElement((string)listViewLogIndex.Items[i].Tag, listViewLogIndex.Items[i].SubItems[1].Text);
                        if (trace.IsErrorResponse)
                            listViewLogIndex.Items[i].BackColor = Color.Red;
                        listViewLogIndex.Items[i].SubItems[5].Text = trace.Mailbox;
                        listViewLogIndex.Items[i].SubItems[6].Text = trace.Impersonating;
                        i++;
                    }
                }));
            }
            else
            {
                while (i < listViewLogIndex.Items.Count)
                {
                    TraceElement trace = new TraceElement((string)listViewLogIndex.Items[i].Tag, listViewLogIndex.Items[i].SubItems[1].Text);
                    if (trace.IsErrorResponse)
                        listViewLogIndex.Items[i].BackColor = Color.Red;
                    listViewLogIndex.Items[i].SubItems[5].Text = trace.Mailbox;
                    listViewLogIndex.Items[i].SubItems[6].Text = trace.Impersonating;
                    i++;
                }
            }
            this._checkingForErrors = false;
        }

        private void UpdateView()
        {
            listViewLogIndex.Visible = true;
            listViewLogIndex_SelectedIndexChanged(null, null);
            listViewLogIndex.Focus();
        }

        private void radioButtonListView_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void radioButtonTreeView_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
        }


        private void listViewLogIndex_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch (e.Column)
            {
                case 0:
                    {
                        // Sort by time
                        if (listViewLogIndex.Tag.Equals("Time ASC,Tid ASC"))
                        {
                            listViewLogIndex.Tag = "Time DESC,Tid ASC";
                        }
                        else
                            listViewLogIndex.Tag = "Time ASC,Tid ASC";
                        break;
                    }

                case 1:
                    {
                        // Sort by time
                        if (listViewLogIndex.Tag.Equals("Tag ASC,Time ASC"))
                        {
                            listViewLogIndex.Tag = "Tag DESC,Time ASC";
                        }
                        else
                            listViewLogIndex.Tag = "Tag ASC,Time ASC";
                        break;
                    }

                case 2:
                    {
                        // Sort by thread id
                        if (listViewLogIndex.Tag.Equals("Tid ASC,Time ASC"))
                        {
                            listViewLogIndex.Tag = "Tid DESC,Time ASC";
                        }
                        else
                            listViewLogIndex.Tag = "Tid ASC,Time ASC";
                        break;
                    }

                case 3:
                    {
                        // Sort by SOAP Method
                        if (listViewLogIndex.Tag.Equals("SOAPMethod ASC,Time ASC"))
                        {
                            listViewLogIndex.Tag = "SOAPMethod DESC,Time ASC";
                        }
                        else
                            listViewLogIndex.Tag = "SOAPMethod ASC,Time ASC";
                        break;
                    }

                case 4:
                    {
                        // Sort by size
                        if (listViewLogIndex.Tag.Equals("Size ASC,Time ASC"))
                        {
                            listViewLogIndex.Tag = "Size DESC,Time ASC";
                        }
                        else
                            listViewLogIndex.Tag = "Size ASC,Time ASC";
                        break;
                    }

                default:
                    return;
            }
            ShowLogIndex();
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            FormLogFilterEdit formFilterEdit = new FormLogFilterEdit(_filters);
            _filters = formFilterEdit.GetFilters(_filters, this);
            ShowLogIndex();
        }

        private void buttonLoadLogFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog oDialog = new FolderBrowserDialog();
            oDialog.ShowNewFolderButton = false;
            oDialog.Description = "All files from the selected folder will be loaded (if valid)";
            if (oDialog.ShowDialog() != DialogResult.OK)
                return;

            listViewLogIndex.Items.Clear();
            xmlEditor1.Text = "Please select a log to view.";
            this.Text = String.Format("Log Viewer - {0}", oDialog.SelectedPath);
            ShowStatus("Processing...");
            ThreadPool.QueueUserWorkItem(new WaitCallback(LoadLogFolder), oDialog.SelectedPath);
        }

        private void LoadLogFolder(object e)
        {
            ToggleButtons(false);
            // Unsubscribe from events of current logger
            _logger.LogAdded -= _logger_LogAdded;
            _logger.ProgressChanged -= _logger_ProgressChanged;

            // Create new logger so as not to interfere with SOAPe's log
            _logger = new ClassLogger((string)e, false);
            _logger.LogAdded += _logger_LogAdded;
            _logger.ProgressChanged += _logger_ProgressChanged;
            _haveLoadedLog = true;
            _logger.LoadLogFolder((string)e);
            ShowLogIndex();
            ToggleButtons(true);
            ShowStatus(null);
        }

        private void buttonAdvancedImport_Click(object sender, EventArgs e)
        {

        }

        private void deleteEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Delete the selected entry/ies

            if (listViewLogIndex.SelectedItems.Count == 0) return;

            _suppressUIUpdates = true;
            listViewLogIndex.BeginUpdate();
            foreach (ListViewItem item in listViewLogIndex.SelectedItems)
            {
                try
                {
                    item.Remove();
                }
                catch { }
            }
            listViewLogIndex.EndUpdate();
            _suppressUIUpdates = false;
            listViewLogIndex_SelectedIndexChanged(null, null);
        }

        private string GetTraceFromListItem(int Index)
        {
            // Return full <trace> xml for given ListViewItem

            if (Index >= listViewLogIndex.Items.Count)
                return null;
            ListViewItem listViewItem = listViewLogIndex.Items[Index];

            StringBuilder trace = new StringBuilder("<Trace ");

            try
            {
                trace.Append("Tag=\"");
                trace.Append(listViewItem.SubItems[1].Text);
                trace.Append("\" ");
            }
            catch { }

            try { 
                trace.Append("Tid=\"");
                trace.Append(listViewItem.SubItems[2].Text);
                trace.Append("\" ");
            }
            catch { }

            try
            { 
                trace.Append("Time=\"");
                trace.Append(listViewItem.SubItems[0].Text);
                trace.Append("\" ");
            }
            catch { }

            trace.Append("Application=\"SOAPe[Export]\" ");
            trace.Append("Version=\"");
            trace.Append(Application.ProductVersion);
            trace.Append("\" ");

            trace.AppendLine(">");

            trace.Append(listViewItem.Tag.ToString());
            trace.AppendLine("</Trace>");

            return trace.ToString();
        }

        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            // Save the log to file

            SaveFileDialog oDialog = new SaveFileDialog();
            oDialog.Filter = _fileFilter;
            oDialog.DefaultExt = "log";
            oDialog.Title = "Save log as";
            oDialog.CheckFileExists = false;
            if (oDialog.ShowDialog() != DialogResult.OK)
                return;

            if (File.Exists(oDialog.FileName))
            {
                try
                {
                    File.Delete(oDialog.FileName);
                }
                catch { }
            }

            ShowStatus("Saving to " + oDialog.FileName,0);
            using (StreamWriter logStreamWriter = File.CreateText(oDialog.FileName))
            {
                for (int i=0; i<listViewLogIndex.Items.Count; i++)
                {
                    ShowStatus("Saving to " + oDialog.FileName, i / listViewLogIndex.Items.Count);
                    logStreamWriter.WriteLine(GetTraceFromListItem(i));
                    logStreamWriter.WriteLine();
                }
            }
            ShowStatus(null);
        }

        private void FormLogViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_haveLoadedLog)
            {
                _logger.Dispose();
            }
        }
    }
}
