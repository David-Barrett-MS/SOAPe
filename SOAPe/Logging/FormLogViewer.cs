/*
 * By David Barrett, Microsoft Ltd. 2016. Use at your own risk.  No warranties are given.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * */

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;

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
        private const string _fileFilter = "All Files|*.*|Log files (*.log)|*.log|XML files (*.xml)|*.xml|Text files (*.txt)|*.txt|Trace files (*.trace)|*.trace";
        private static string _lastLogFolderOpenPath = "";
        private List<ListViewItem> _clientRequestIdTracking = new List<ListViewItem>();
        private string _trackedClientRequestId = "";

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
            SortByColumn(0);
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

            DataRow[] logRows = null;
            string sFilter = GetLogFilter();
            try
            {
                logRows = _logger.LogDataTable.Select(sFilter);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error querying logs, please check filter{0}Error: {1}", Environment.NewLine, ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.DebugLog(String.Format("Error retrieving logs rows, filter: {0}", sFilter));
                _logger.DebugLogError(ex);
                return;
            }

            xmlEditor1.Text = "Select trace to view.";
            // We disable sorting while we load the listview
            IComparer sorter = listViewLogIndex.ListViewItemSorter;

            int i=-1;
            Action action = new Action(() => {
                listViewLogIndex.ListViewItemSorter = null;
                listViewLogIndex.BeginUpdate();
                listViewLogIndex.Items.Clear();
            });
            if (listViewLogIndex.InvokeRequired)
                listViewLogIndex.Invoke(action);
            else
                action();

            iTraceCount = logRows.Length;

            while (i < iTraceCount-1)
            {
                ShowStatus($"Analysing {i++} of {iTraceCount}", ((double)i / (double)iTraceCount)*100);
                ListViewItem item = new ListViewItem(logRows[i]["Time"].ToString());
                item.Tag = TraceElement.CreateFromDataTableRow(logRows[i]);
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

            action = new Action(() => {
                listViewLogIndex.EndUpdate();
                listViewLogIndex.ListViewItemSorter = sorter;
            });

            if (listViewLogIndex.InvokeRequired)
                listViewLogIndex.Invoke(action);
            else
                action();

            if (String.IsNullOrEmpty(sFilter))
            {
                ShowStatus(null);
            }
            else
                ShowStatus("Filter Active", 100, Color.MintCream);
            Task.Run(new Action(() => { AnalyzeTrace(); }));
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void _logger_LogAdded(object sender, LogAddedEventArgs e)
        {
            // New log added

            Action action = new Action(() =>
            {
                ListViewItem oItem = new ListViewItem(e.Trace.TraceTime.ToString());
                string sTag = "";
                try
                {
                    sTag = ClassLogger.ReadMethodFromRequest(e.Trace.Data);
                }
                catch { }
                oItem.Tag = e.Trace;
                oItem.SubItems.Add(e.Trace.TraceTag);
                oItem.SubItems.Add(e.Trace.TraceThreadId.ToString());
                oItem.SubItems.Add(sTag);
                oItem.SubItems.Add(String.Empty);
                oItem.SubItems.Add(String.Empty);
                oItem.SubItems.Add(String.Empty);

                UpdateListViewItem(oItem, e.Trace);

                if (listViewLogIndex.InvokeRequired)
                {
                    listViewLogIndex.Invoke(new MethodInvoker(delegate () { listViewLogIndex.Items.Add(oItem); }));
                }
                else
                    listViewLogIndex.Items.Add(oItem);

            });
            Task.Run(action);
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
                    sDetails = ((TraceElement)listViewLogIndex.FocusedItem.Tag).Data;
                }
                catch { }
                _lastSelectedItem = listViewLogIndex.FocusedItem;
            }
            else
                _lastSelectedItem = null;
            xmlEditor1.Text = sDetails;


            // client-request-id tracking
            string clientRequestId = "";
            if (listViewLogIndex.FocusedItem != null)
                clientRequestId = ((TraceElement)listViewLogIndex.FocusedItem.Tag).ClientRequestId;

            if (clientRequestId != _trackedClientRequestId)
            {
                if (_clientRequestIdTracking.Count > 0)
                {
                    // Remove highlighting from previous id tracking
                    foreach (ListViewItem item in _clientRequestIdTracking)
                    {
                        Color restoreColour = Color.White;
                        if (item.SubItems[0].Tag != null)
                            restoreColour = (Color)item.SubItems[0].Tag;
                        item.BackColor = restoreColour;
                        item.SubItems[0].Tag = null;
                    }
                    _clientRequestIdTracking.Clear();
                    _trackedClientRequestId = "";
                }

                if (listViewLogIndex.FocusedItem != null)
                {
                    if (!String.IsNullOrEmpty(clientRequestId))
                    {
                        // We have a client request Id, so highlight any matching items
                        foreach (ListViewItem item in listViewLogIndex.Items)
                            if (((TraceElement)item.Tag).ClientRequestId == clientRequestId)
                            {
                                _clientRequestIdTracking.Add(item);
                                item.SubItems[0].Tag = item.BackColor;
                                item.BackColor = TraceElement.ClientRequestIdMatchesColour;
                            }
                        _trackedClientRequestId = clientRequestId;
                    }
                }
            }
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
            oDialog.DefaultExt = "";
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
            ShowStatus($"Opening {loadLogFilename}");
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
            ToggleButton(Enabled, buttonSaveAs);
        }

        private void ShowStatus(string Status, double PercentComplete = 0, Color? Colour = null)
        {
            // Update the status information

            if (String.IsNullOrEmpty(Status))
            {
                // No status, so hide the control
                Action hideStatusAction = new Action(() =>
                {
                    statusPercentBar1.Status = "";
                    statusPercentBar1.PercentComplete = PercentComplete;
                    statusPercentBar1.Visible = false;
                });

                if (statusPercentBar1.InvokeRequired)
                    statusPercentBar1.Invoke(hideStatusAction);
                else
                    hideStatusAction();

                return;
            }

            // Show the status
            if (Colour == null)
                Colour = Color.PaleGreen;

            Action showStatusAction = new Action(() =>
            {
                statusPercentBar1.Status = Status;
                statusPercentBar1.PercentComplete = PercentComplete;
                statusPercentBar1.BarColour = (Color)Colour;
                statusPercentBar1.Visible = true;
                statusPercentBar1.Refresh();
            });

            if (statusPercentBar1.InvokeRequired)
                statusPercentBar1.Invoke(showStatusAction);
            else
                showStatusAction();
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
            _logger.ProgressChanged += _logger_ProgressChanged;
            _logger.LoadLogFile((string)e);
            _haveLoadedLog = true;
            ShowLogIndex();
            SortByColumn(0);
            ToggleButtons(true);
            ShowStatus(null);
            _logger.LogAdded += _logger_LogAdded;
        }

        private void ReloadLogFile(object e)
        {
            ToggleButtons(false);
            _logger.LoadPreviousTrace();
            ShowLogIndex();
            ToggleButtons(true);
            ShowStatus(null);
        }


        private void AnalyzeTrace()
        {
            // Go through the listbox items and check content for errors
            // This method is intended to run on a background thread, as the checking can take a significant length of time

            if (this._checkingForErrors)
                return;

            this._checkingForErrors = true;

            try
            {
                Action action;
                Dictionary<ListViewItem, TraceElement> updatedElements = new Dictionary<ListViewItem, TraceElement>();
                string status = "Cross-referencing and error checking...";

                // Analyse all the elements (two passes), and store any that have been updated
                double percentComplete = 0;
                ShowStatus(status, percentComplete);
                int updateCount = 0;
                for (int j = 1; j < 3; j++)
                    for (int i = 0; i < listViewLogIndex.Items.Count; i++)
                    {
                        action = new Action(() =>
                        {
                            TraceElement traceElement = (TraceElement)listViewLogIndex.Items[i].Tag;
                            if (traceElement.Analyze())
                                if (!updatedElements.ContainsKey(listViewLogIndex.Items[i]))
                                    updatedElements.Add(listViewLogIndex.Items[i], traceElement);
                        });

                        if (listViewLogIndex.InvokeRequired)
                            listViewLogIndex.Invoke(action);
                        else
                            action();

                        if (updateCount++ > 100)
                        {
                            percentComplete = ((((double)i / (double)listViewLogIndex.Items.Count) / 2) + (((double)j-1) / 2)) * 100;
                            ShowStatus(status, percentComplete);
                            updateCount = 0;
                        }
                    }

                ShowStatus($"Updating UI ({updatedElements.Count} items)", 100);
                action = new Action(() =>
                {
                    // Update UI
                    listViewLogIndex.BeginUpdate();
                    foreach (ListViewItem listViewItem in updatedElements.Keys)
                        UpdateListViewItem(listViewItem, updatedElements[listViewItem]);

                    listViewLogIndex.EndUpdate();
                    ShowStatus(null);
                });
                if (listViewLogIndex.InvokeRequired)
                    listViewLogIndex.Invoke(action);
                else
                    action();
            }
            catch { }
            this._checkingForErrors = false;
        }

        public static void UpdateListViewItem(ListViewItem listViewItem, TraceElement traceElement)
        {
            if (traceElement.HighlightColour != null && listViewItem.BackColor != (Color)traceElement.HighlightColour)
                listViewItem.BackColor = (Color)traceElement.HighlightColour;

            if (listViewItem.SubItems[1].Text != traceElement.TraceTag)
                listViewItem.SubItems[1].Text = traceElement.TraceTag;

            if (listViewItem.SubItems[3].Text != traceElement.SOAPMethod)
                listViewItem.SubItems[3].Text = traceElement.SOAPMethod;

            if (listViewItem.SubItems[4].Text != traceElement.Data.Length.ToString())
                listViewItem.SubItems[4].Text = traceElement.Data.Length.ToString();

            if (listViewItem.SubItems[5].Text != traceElement.Mailbox)
                listViewItem.SubItems[5].Text = traceElement.Mailbox;

            if (listViewItem.SubItems[6].Text != traceElement.Impersonating)
                listViewItem.SubItems[6].Text = traceElement.Impersonating;
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

        /// <summary>
        /// Creates the appropriate IComparer to sort the list view based on the given column
        /// </summary>
        /// <param name="col">The column that will be sorted</param>
        private void SortByColumn(int col)
        {
            bool sortAscending = !((String)listViewLogIndex.Tag).EndsWith("ASC");
            bool sortAsDateTime = col == 0;
            bool sortAsNumber = col == 2 || col == 4;

            if (sortAscending)
                listViewLogIndex.Tag = "ASC";
            else
                listViewLogIndex.Tag = "DESC";

            
            if (listViewLogIndex.InvokeRequired)
            {
                listViewLogIndex.Invoke(new MethodInvoker(delegate ()
                {
                    listViewLogIndex.ListViewItemSorter = new ListViewItemComparer(col, sortAscending, sortAsDateTime, sortAsNumber);
                }));
            }
            else
                listViewLogIndex.ListViewItemSorter = new ListViewItemComparer(col, sortAscending, sortAsDateTime, sortAsNumber);
        }

        private void listViewLogIndex_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortByColumn(e.Column);
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

            if (!String.IsNullOrEmpty(_lastLogFolderOpenPath))
                oDialog.SelectedPath = _lastLogFolderOpenPath;

            if (oDialog.ShowDialog() != DialogResult.OK)
                return;

            _lastLogFolderOpenPath = oDialog.SelectedPath;

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

            if (!String.IsNullOrEmpty(((TraceElement)listViewItem.Tag).ClientRequestId))
            {
                trace.Append("ClientRequestId=\"");
                trace.Append(((TraceElement)listViewItem.Tag).ClientRequestId);
                trace.Append("\" ");
            }

            trace.AppendLine(">");

            trace.Append(((TraceElement)listViewItem.Tag).Data);
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

            bool exportSelectedOnly = false;
            if (listViewLogIndex.SelectedItems.Count>0)
            {
                if (MessageBox.Show("Save only selected items?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    exportSelectedOnly = true;
            }

            ShowStatus("Saving to " + oDialog.FileName,0);
            using (StreamWriter logStreamWriter = File.CreateText(oDialog.FileName))
            {
                if (exportSelectedOnly)
                {
                    // If there are items selected, we just export those
                    for (int i=0; i<listViewLogIndex.SelectedIndices.Count; i++)
                    {
                        ShowStatus("Saving to " + oDialog.FileName, i / listViewLogIndex.SelectedIndices.Count);
                        logStreamWriter.WriteLine(GetTraceFromListItem(listViewLogIndex.SelectedIndices[i]));
                        logStreamWriter.WriteLine();
                    }
                }
                else
                {
                    for (int i = 0; i < listViewLogIndex.Items.Count; i++)
                    {
                        ShowStatus("Saving to " + oDialog.FileName, i / listViewLogIndex.Items.Count);
                        logStreamWriter.WriteLine(GetTraceFromListItem(i));
                        logStreamWriter.WriteLine();
                    }
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

    /// <summary>
    /// Implements the sort by column comparer
    /// </summary>
    class ListViewItemComparer : IComparer
    {
        private int col;
        private bool asc = true;
        private bool isDateTime = false;
        private bool isNumber = false;

        public ListViewItemComparer()
        {
            col = 0;
        }
        public ListViewItemComparer(int column, bool Ascending = true, bool typeIsDateTime = false, bool typeIsNumber = false)
        {
            col = column;
            asc = Ascending;
            isDateTime = typeIsDateTime;
            isNumber = typeIsNumber;
        }
        public int Compare(object x, object y)
        {
            if (isDateTime)
            {
                try
                {
                    DateTime dx = DateTime.Parse(((ListViewItem)x).SubItems[col].Text);
                    DateTime dy = DateTime.Parse(((ListViewItem)y).SubItems[col].Text);
                    if (asc)
                        return DateTime.Compare(dx, dy);

                    return DateTime.Compare(dy, dx);
                }
                catch { }
                return 0;
            }
            else if (isNumber)
            {
                try
                {
                    long lx = long.Parse(((ListViewItem)x).SubItems[col].Text);
                    long ly = long.Parse(((ListViewItem)y).SubItems[col].Text);
                    long diff = lx - ly;

                    if (asc)
                        diff = ly - lx;

                    if (diff < 0)
                        return -1;
                    else if (diff > 0)
                        return 1;
                }
                catch { }
                return 0;
            }

            // Default is to use string comparison
            if (asc)
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);

            return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
        }
    }
}
