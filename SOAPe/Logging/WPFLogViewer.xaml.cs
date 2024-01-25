using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Data;

namespace SOAPe.Logging
{
    /// <summary>
    /// Interaction logic for WPFLogViewer.xaml
    /// </summary>
    public partial class WPFLogViewer : UserControl
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

        public WPFLogViewer()
        {
            InitializeComponent();

            //string logFile = OpenExistingLogFile();

            listViewIndex.Items.Clear();
            //SortByColumn(0);
            xmlEditor1.Document.Blocks.Clear();
            xmlEditor1.Document.Blocks.Add(new Paragraph(new Run("Please select a log to view.")));
            //ShowStatus("Processing...");
            //ThreadPool.QueueUserWorkItem(new WaitCallback(LoadLogFile), logFile);
            //_syntaxHighlighter = new ClassSyntaxHighlighter(xmlEditor1);
            _filters = new string[0];
        }

        private string OpenExistingLogFile()
        {
            OpenFileDialog oDialog = new OpenFileDialog();
            oDialog.Filter = _fileFilter;
            oDialog.DefaultExt = "";
            oDialog.Title = "Select log/trace file to open";
            oDialog.CheckFileExists = true;
            if (oDialog.ShowDialog() != true)
                return String.Empty;

            return oDialog.FileName;
        }

        //private void ShowLogIndex()
        //{
        //    // Read the logs and update the UI

        //    if (_logger.LogDataTable == null)
        //        return;

        //    int iTraceCount = _logger.LogDataTable.Rows.Count;

        //    if (iTraceCount == 0)
        //        return;

        //    DataRow[] logRows = null;
        //    string sFilter = GetLogFilter();
        //    try
        //    {
        //        logRows = _logger.LogDataTable.Select(sFilter);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error querying logs, please check filter{Environment.NewLine}Error: {ex.Message}",
        //            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        _logger.DebugLog(String.Format("Error retrieving logs rows, filter: {0}", sFilter));
        //        _logger.DebugLogError(ex);
        //        return;
        //    }

        //    Paragraph defaultText = new Paragraph();
        //    defaultText.Inlines.Add("Select trace to view.");
        //    xmlEditor1.Document = new FlowDocument(defaultText);
        //    // We disable sorting while we load the listview
        //    //IComparer sorter = listViewLogIndex.ListViewItemSorter;

        //    int i = -1;

        //    listViewIndex.Items.Clear();

        //    iTraceCount = logRows.Length;

        //    while (i < iTraceCount - 1)
        //    {
        //        ShowStatus($"Analysing {i++} of {iTraceCount}", ((double)i / (double)iTraceCount) * 100);
        //        ListViewItem item = new ListViewItem(); //(logRows[i]["Time"].ToString());
        //        item.Tag = TraceElement.CreateFromDataTableRow(logRows[i]);
        //        StringBuilder sDescription = new StringBuilder(logRows[i]["Tag"].ToString());
        //        item.SubItems.Add(sDescription.ToString());
        //        item.SubItems.Add(logRows[i]["Tid"].ToString());
        //        item.SubItems.Add(logRows[i]["SOAPMethod"].ToString());
        //        item.SubItems.Add(((Int64)logRows[i]["Size"]).ToString("0,0"));
        //        item.SubItems.Add(logRows[i]["Mailbox"].ToString());
        //        item.SubItems.Add(logRows[i]["ExchangeImpersonation"].ToString());
        //        if (listViewLogIndex.InvokeRequired)
        //        {
        //            listViewLogIndex.Invoke(new MethodInvoker(delegate ()
        //            {
        //                listViewLogIndex.Items.Add(item);
        //            }));
        //        }
        //        else
        //            listViewLogIndex.Items.Add(item);
        //    }

        //    action = new Action(() => {
        //        listViewLogIndex.EndUpdate();
        //        listViewLogIndex.ListViewItemSorter = sorter;
        //    });

        //    if (listViewLogIndex.InvokeRequired)
        //        listViewLogIndex.Invoke(action);
        //    else
        //        action();

        //    if (String.IsNullOrEmpty(sFilter))
        //    {
        //        ShowStatus(null);
        //    }
        //    else
        //        ShowStatus("Filter Active", 100, Color.MintCream);
        //    Task.Run(new Action(() => { AnalyzeTrace(); }));
        //}

        //private void ShowLogList(object e)
        //{
        //    ShowLogIndex();
        //}

        private void LoadLogButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearLogButton_Click(object sender, RoutedEventArgs e)
        {
            _logger.ClearHistory();
            //ThreadPool.QueueUserWorkItem(new WaitCallback(ShowLogList), null);
        }

        private void LoadLogFolderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
