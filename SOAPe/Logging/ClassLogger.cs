/*
 * By David Barrett, Microsoft Ltd. 2015. Use at your own risk.  No warranties are given.
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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace SOAPe
{
    public enum EWSTraceType
    {
        Unknown = -1,
        Autodiscover = 0,
        Request = 1,
        Response = 2
    }

    public class LogAddedEventArgs: EventArgs
    {
        private TraceElement _trace;

        public LogAddedEventArgs(TraceElement traceElement)
        {
            _trace = traceElement;
        }

        public int ThreadId
        {
            get { return _trace.TraceThreadId;  }
        }

        public TraceElement Trace
        {
            get { return _trace;  }
        }
    }

    public class ProgressEventArgs : EventArgs
    {
        private string _progress = "";
        float _percentComplete = -1;

        public ProgressEventArgs(string Progress)
        {
            _progress = Progress;
        }

        public ProgressEventArgs(string Progress, float PercentComplete):this(Progress)
        {
            _percentComplete = PercentComplete;
        }
        public string Progress
        {
            get { return _progress; }
        }

        public float PercentComplete
        {
            get { return _percentComplete; }
        }
    }

    public class ClassLogger: IDisposable
    {
        private FileStream _logFileStream = null;
        private string _logPath = "";
        private static StreamWriter _debugLogStream = null;
        private string _debugLogPath = "";
        private DataTable _logDataTable = null;
        private static Dictionary<string, int> _threadNameToNumber;
        private readonly object _lockObject = new object();
        private readonly object _dataTableLockObject = new object();
        private DateTime _lastKnownEventTime = DateTime.MinValue; // This is used to get the responses in the correct order for EWSEditor logs (to work around EWSEditor bug)
        private int _eventTimeOffset = 1;

        // Events
        public delegate void LogAddedEventHandler(object sender, LogAddedEventArgs e);
        public event LogAddedEventHandler LogAdded;
        public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);
        public event ProgressEventHandler ProgressChanged;

        public ClassLogger(string LogFile, bool WithDebug = true)
        {
            try
            {
                if (!File.Exists(LogFile))
                {
#pragma warning disable CS0642 // Possible mistaken empty statement
                    using (StreamWriter logStreamWriter = File.CreateText(LogFile)) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
                }
                if (File.Exists(LogFile))
                {
                    _logFileStream = new FileStream(LogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    _logPath = LogFile;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            _threadNameToNumber = new Dictionary<string, int>();

            if (_debugLogStream == null)
            {
                if (WithDebug)
                {
                    _debugLogPath = LogFile.Substring(0, LogFile.Length - System.IO.Path.GetExtension(LogFile).Length);
                    _debugLogPath += ".debug";
                    try
                    {
                        _debugLogStream = File.AppendText(_debugLogPath);
                    }
                    catch { }
                }
            }
            InitDataTable();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_logFileStream != null)
                {
                    _logFileStream.Close();
                    _logFileStream.Dispose();
                }
                _logFileStream = null;
                if (_debugLogStream != null)
                {
                    _debugLogStream.Close();
                    _debugLogStream.Dispose();
                }
                _debugLogStream = null;
                if (_logDataTable != null)
                    _logDataTable.Dispose();
                _logDataTable = null;
            }
        }  

        private void InitDataTable()
        {
            if (!(_logDataTable==null))
                _logDataTable.Dispose();

            _logDataTable = new DataTable("Logs");

            DataColumn column = new DataColumn("Id", System.Type.GetType("System.Int32"));
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            _logDataTable.Columns.Add(column);

            _logDataTable.Columns.Add(new DataColumn("Tag", System.Type.GetType("System.String")));
            _logDataTable.Columns.Add(new DataColumn("Tid", System.Type.GetType("System.Int32")));
            _logDataTable.Columns.Add(new DataColumn("Time", System.Type.GetType("System.DateTime")));
            _logDataTable.Columns.Add(new DataColumn("Version", System.Type.GetType("System.String")));
            _logDataTable.Columns.Add(new DataColumn("Application", System.Type.GetType("System.String")));
            _logDataTable.Columns.Add(new DataColumn("SOAPMethod", System.Type.GetType("System.String")));
            _logDataTable.Columns.Add(new DataColumn("Data", System.Type.GetType("System.String")));
            _logDataTable.Columns.Add(new DataColumn("Size", System.Type.GetType("System.Int64")));
            _logDataTable.Columns.Add(new DataColumn("ExchangeImpersonation", System.Type.GetType("System.String")));
            _logDataTable.Columns.Add(new DataColumn("Mailbox", System.Type.GetType("System.String")));
            _logDataTable.Columns.Add(new DataColumn("ClientRequestId", System.Type.GetType("System.String")));
        }

        public void ClearHistory()
        {

            InitDataTable();
        }

        public DataTable LogDataTable
        {
            get { return _logDataTable;  }
        }

        protected virtual void OnLogAdded(LogAddedEventArgs e)
        {
            LogAddedEventHandler handler = LogAdded;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnProgressChanged(ProgressEventArgs e)
        {
            Progress = e.Progress;
            ProgressEventHandler handler = ProgressChanged;
            if (handler != null)
                handler(this, e);
        }

        public string Progress { get; private set; } = "";

        private string TraceTag(string SOAPeTag)
        {
            // Returns the EWS Managed API equivalent tag given SOAPe's
            // Means that the traces files are compatible (and also with SOAPe's log parser)

            switch (SOAPeTag)
            {
                case "Request Credentials": return "EwsRequestHttpCredentials";
                case "Request Headers": return "EwsRequestHttpHeaders";
                case "Request": return "EwsRequest";
                case "Response Headers": return "EwsResponseHttpHeaders";
                case "Response": return "EwsResponse";
                case "Response Cookies": return "EwsResponseCookies";
                default: break;
            }

            if (SOAPeTag.ToLower().StartsWith("autodiscover response"))
                return "AutodiscoverResponse";

            return SOAPeTag;
        }

        private void LogToDatabase(string Data, string Tag, DateTime LogTime, int ThreadId, string LogApplication,
            string LogVersion, string Mailbox = "", string Impersonating = "", string ClientRequestId = "")
        {
            // Write the trace information to the local database (which is used for the log viewer)

            lock (_dataTableLockObject)
            {
                DataRow row = _logDataTable.NewRow();
                row["Tag"] = Tag;
                row["Tid"] = ThreadId;
                row["Time"] = LogTime;
                row["Version"] = LogVersion;
                row["Application"] = LogApplication;
                row["SOAPMethod"] = ""; // Populated during analysis to speed up log import
                row["Data"] = Data;
                row["Size"] = Data.Length;
                row["Mailbox"] = Mailbox;
                row["ExchangeImpersonation"] = Impersonating;
                row["ClientRequestId"] = ClientRequestId;
                _logDataTable.Rows.Add(row);
            }           
        }

        public void Log(string Details, string Description, string clientRequestId = "")
        {
            Log(Details, Description, false, DateTime.Now, -1, "SOAPe", "", false, clientRequestId);
        }

        public void Log(string Details, string Description, bool SuppressLogToFile, DateTime LogTime, int ThreadId, string LogApplication, string LogVersion, bool SuppressEvent,string clientRequestId="")
        {
            DateTime logTime = DateTime.Now;
            if (LogTime != null)
                logTime = (DateTime)LogTime;

            // Build the trace (we use the same format as EWSEditor)
            if (String.IsNullOrEmpty(LogVersion) && LogApplication.Equals("SOAPe"))
                LogVersion = Application.ProductVersion;
            StringBuilder sTrace = new StringBuilder();
            sTrace.Append($"<Trace Tag=\"{TraceTag(Description)}\" Tid=\"{ThreadId}\" Time=\"{logTime}\" Application=\"{LogApplication}\" Version=\"{LogVersion}\"");
            if (!String.IsNullOrEmpty(clientRequestId))
                sTrace.Append($" ClientRequestId=\"{clientRequestId}\"");
            sTrace.Append(" >");
            sTrace.Append(Environment.NewLine);
            sTrace.Append(Details);
            sTrace.Append(Environment.NewLine);
            sTrace.Append("</Trace>");

            lock (_lockObject)
            {
                if (!SuppressLogToFile)
                {
                    if (!(_logFileStream == null))
                    {
                        if (!_logFileStream.CanWrite)
                        {
                            try
                            {
                                _logFileStream.Close();
                            }
                            catch { }
                            _logFileStream = null;
                        }
                    }
                    if (_logFileStream == null)
                    {
                        try
                        {
                            _logFileStream = new FileStream(_logPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                        }
                        catch { }
                    }
                    using (StreamWriter logStreamWriter = new StreamWriter(_logFileStream))
                    {
                        logStreamWriter.WriteLine("");
                        logStreamWriter.WriteLine(sTrace.ToString());
                    }
                }
            }

            TraceElement traceElement = new TraceElement(sTrace.ToString());
            LogToDatabase(Details, TraceTag(Description), logTime, ThreadId, LogApplication, LogVersion,traceElement.Mailbox, traceElement.Impersonating, traceElement.ClientRequestId);
            if (!SuppressEvent)
                OnLogAdded(new LogAddedEventArgs(traceElement));
        }

        public void DebugLog(string Details)
        {
            if (_debugLogStream == null)
                return;

            lock (_lockObject)
            {
                try
                {
                    _debugLogStream.WriteLine(String.Format("{0:dd-MM-yy HH:mm:ss}  {1}", DateTime.Now, Details));
                    _debugLogStream.Flush();
                }
                catch { }
            }
        }

        public void DebugLogError(Exception exception)
        {
            DebugLog(new StringBuilder("ERROR: ").Append(exception.Message.TrimEnd(Environment.NewLine.ToCharArray())).ToString());
        }

        public void DebugLogError(Exception exception, string ErrorSource)
        {
            DebugLog(new StringBuilder(ErrorSource).Append(": ").Append(exception.Message.TrimEnd(Environment.NewLine.ToCharArray())).ToString());
        }

        public void LoadPreviousTrace()
        {
            LoadLogFile(_logPath);
        }

        public void LoadLogFolder(string LogFolder)
        {
            try
            {
                string[] files = Directory.GetFiles(LogFolder);
                int i = 0;
                foreach (string file in files)
                {
                    float percentComplete = ((float)(i * 100) / (float)files.Length);
                    string sProcessInfo = String.Format("Processing file {0} of {1}", i++, files.Length);
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Length > 1048576)
                        sProcessInfo = String.Format("Processing large file {0} of {1}", i++, files.Length);

                    OnProgressChanged(new ProgressEventArgs(sProcessInfo, percentComplete));
                    LoadLogFile(file, false, false);
                    OnProgressChanged(new ProgressEventArgs(String.Format("Processed file {0} of {1}", i, files.Length), percentComplete));
                }
            }
            catch { }
            OnProgressChanged(new ProgressEventArgs(""));
        }

        public void LoadLogFile(string LogFileName, bool ClearHistory = true, bool ShowProgress = true)
        {
            // Attempt to load the given log file (parsing it so that it can be displayed in the log viewers)
            FileStream readLogFileStream = null;
            DebugLog(String.Format("Opening log file ({0}) for reading", LogFileName));
            try
            {
                readLogFileStream = new FileStream(LogFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch (Exception ex)
            {
                DebugLogError(ex);
                System.Windows.Forms.MessageBox.Show(String.Format("Log file: {1}{0}Error: {2}", Environment.NewLine, LogFileName, ex.Message), "Unable to load log file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ClearHistory) this.ClearHistory();

            List<Task> parseTasks = new List<Task>();
            Action action;

            DateTime readStartTime = DateTime.Now;
            int i = 0;
            try
            {
                // Read and analyse log.  Regular expressions work, but are expensive (therefore slow) for large files
                // We read each character at a time until we have a full trace (based on <trace> tags), then parse it
                long streamLength = readLogFileStream.Length;
                long readBytes = 0;
                float percentComplete = 0;
                bool bFoundXmlTag = false;
                bool bFoundTraceTag = false;
                bool bInXml = false;

                using (StreamReader logFileReader = new StreamReader(readLogFileStream))
                {
                    StringBuilder sTrace = new StringBuilder();
                    StringBuilder sTag=null;
                    bool bCollectTag = false;
                    bool bInTraceTag = false;
                    byte lastShowProgressPercent = 0;

                    while (!logFileReader.EndOfStream)
                    {
                        char c=(char)logFileReader.Read();
                        sTrace.Append(c);
                        percentComplete = ((float)readBytes++ / (float)streamLength) * (float)100;

                        if (ShowProgress)
                        {
                            // Ensure we update progress at least once per percentage of the way through the file we are
                            if ((byte)percentComplete > lastShowProgressPercent)
                            {
                                lastShowProgressPercent = (byte)percentComplete;
                                if (i>0)
                                    OnProgressChanged(new ProgressEventArgs($"Processing {i}", percentComplete));
                                else
                                    OnProgressChanged(new ProgressEventArgs($"Processing...", percentComplete));
                            }
                        }

                        if (bCollectTag)
                        {
                            sTag.Append(c);
                            if (c.Equals('>'))
                            {
                                if (sTag.ToString().Equals("</trace>", StringComparison.OrdinalIgnoreCase))
                                {
                                    // We have a complete trace, so process it
                                    if (ShowProgress)
                                        OnProgressChanged(new ProgressEventArgs($"Processing {_logDataTable.Rows.Count}/{i++}", percentComplete));
                                    // When we try to offload this onto a new thread, we lose events.  No idea why currently, will
                                    // investigate when I have more time (there don't seem to be any errors)
                                    //action = new Action(() =>
                                    //{
                                        ParseTrace(sTrace.ToString());
                                    //});
                                    //parseTasks.Add(Task.Run(() => action()));
                                    sTrace = new StringBuilder();
                                    bInTraceTag = false;
                                }
                                else if (sTag.ToString().StartsWith("<trace", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (bInXml)
                                    {
                                        // We have been collecting Xml that has occurred outside trace tags
                                        // As we've now found another trace tag, we try to parse everything we have collected in between as Xml

                                        if (ShowProgress)
                                            OnProgressChanged(new ProgressEventArgs($"Processing {_logDataTable.Rows.Count}/{i++}", percentComplete));
                                        sTrace.Remove(sTrace.Length - sTag.Length, sTag.Length);

                                        // Now we remove any trailing characters until we reach a > (which should be the closing tag of the Xml)
                                        string xmlData = sTrace.ToString();
                                        int closingTag = xmlData.LastIndexOf(">");
                                        if (closingTag>0)
                                        {
                                            xmlData = xmlData.Substring(0, closingTag+1);
                                        }
                                        //action = new Action(() =>
                                        //{
                                            ParseGenericRequest(xmlData);
                                        //});
                                        //parseTasks.Add(Task.Run(() => action()));

                                        bInXml = false;
                                    }
                                    sTrace = new StringBuilder(sTag.ToString());
                                    bFoundTraceTag = true;
                                    bFoundXmlTag = false; // We need to ensure this is false if we find a trace tag so that trace tags take priority
                                    bInTraceTag = true;
                                }
                                else if (sTag.ToString().StartsWith("<?xml", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (!bInTraceTag)
                                    {
                                        // We've found the xml opening tag outside <trace> tag.  EWSEditor has a bug that causes some responses
                                        // to be presented this way, so we'll capture this Xml.  This also helps parse other logs.
                                        if (bFoundXmlTag)
                                        {
                                            // We've already found one Xml tag, so this file must contain more than one xml dump
                                            // We  process the collected trace as Xml
                                            if (ShowProgress)
                                                OnProgressChanged(new ProgressEventArgs($"Processing {_logDataTable.Rows.Count}/{i++}", percentComplete));
                                            sTrace.Remove(sTrace.Length - sTag.Length, sTag.Length);
                                            //action = new Action(() =>
                                            //{
                                                ParseGenericRequest(sTrace.ToString());
                                            //});
                                            //parseTasks.Add(Task.Run(() => action()));
                                        }
                                        else
                                        {
                                            if (!bFoundTraceTag)
                                            {
                                                bFoundXmlTag = true;
                                            }
                                            else
                                            {
                                                // We only get here when we have Xml mixed in with Trace (looking at you, EWSEditor...)
                                                // We should be able to collect everything up to the next Trace or Xml tag and then parse that

                                            }
                                        }
                                        sTrace = new StringBuilder(sTag.ToString());
                                        bInXml = true;
                                    }
                                }
                                bCollectTag = false;
                            }
                        }
                        else
                        {
                            if (c.Equals('<'))
                            {
                                bCollectTag = true;
                                sTag = new StringBuilder("<");
                            }
                        }
                    }
                    if (bFoundXmlTag)
                    {
                        if (ShowProgress)
                            OnProgressChanged(new ProgressEventArgs($"Processing {_logDataTable.Rows.Count}/{i++}", percentComplete));
                        //action = new Action(() =>
                        //{
                            ParseGenericRequest(sTrace.ToString());
                        //});
                        //parseTasks.Add(Task.Run(() => action()));
                    }
                }
            }
            catch (Exception ex)
            {
                this.DebugLog(String.Format("LoadLogFile: {0}", ex.Message));
            }
            finally
            {
                if (ShowProgress)
                    OnProgressChanged(new ProgressEventArgs($"Imported {_logDataTable.Rows.Count} of {i} potential events"));
                DebugLog(String.Format("Closing log file {0}", LogFileName));
                readLogFileStream.Close();
            }

            while (parseTasks.Count>0)
            {
                while (parseTasks[0].Status == TaskStatus.Running || parseTasks[0].Status == TaskStatus.WaitingToRun)
                    Thread.Yield();
                parseTasks.RemoveAt(0);
            }
            TimeSpan loadDuration = DateTime.Now.Subtract(readStartTime);
            DebugLog(String.Format("{0} loaded in {1}", LogFileName, loadDuration));
            DebugLog($"Imported {_logDataTable.Rows.Count} of {i} potential events");
            if (ShowProgress)
                OnProgressChanged(new ProgressEventArgs($"Imported {_logDataTable.Rows.Count} of {i} potential events"));
        }

        private string ReadTraceTag(string Trace)
        {
            Regex regex = new Regex("<trace.*?>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Match traceTagMatch = regex.Match(Trace);
            if (traceTagMatch.Success)
            {
                return traceTagMatch.Value;
            }
            return "";
        }

        private Dictionary<string,string> ParseTraceTag(string TraceTag)
        {
            // Parse the trace tag and return the attibutes as a dictionary object
            Regex regex = new Regex("(?<attribute>.*?)=\"(?<value>.*?)\"", RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(TraceTag);
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            foreach (Match match in matches)
            {
                attributes.Add(match.Groups[1].Value.ToLower(), match.Groups[2].Value);
            }
            return attributes;
        }

        private void ParseTrace(string Tags, string Data)
        {
            // Parse the trace
            Dictionary<string, string> attributes = ParseTraceTag(Tags);
            int threadId = -1;
            try
            {
                threadId = int.Parse(attributes["tid"]);
            }
            catch { }

            DateTime logTime = DateTime.MinValue;
            try
            {
                logTime = DateTime.Parse(attributes["time"]);
            }
            catch { }

            string application = "";
            try
            {
                application = attributes["application"];
            }
            catch { }

            try
            {
                Log(Data, attributes["tag"], true, logTime, threadId, application, "", false);
            }
            catch { }
        }

        private void ParseTrace(string Trace)
        {
            // Attempt to parse <trace> entry (as generated by EWS Managed API, and SOAPe), and add to the database

            XmlDocument xml = new XmlDocument();
            string sEWSData = "";

            // Extract the EWS payload (everything in the <Trace></Trace>)
            int iContentStart = Trace.IndexOf(">") + 1;
            int iContentEnd = Trace.LastIndexOf("</Trace>", StringComparison.OrdinalIgnoreCase);
            if (iContentStart < 0 || iContentEnd < 0 || (iContentEnd-iContentStart<1) )
                return;

            sEWSData = Trace.Substring(iContentStart, iContentEnd - iContentStart).TrimStart();
            StringBuilder sTraceInfo = new StringBuilder(Trace.Substring(0, iContentStart)).Append(Trace.Substring(iContentEnd));
            try
            {
                xml.LoadXml(sTraceInfo.ToString());
            }
            catch
            {
                // Failed to load Xml, test for double-quotes (occurs when data is wrapped in CSV)
                string sTraceFixed = sTraceInfo.ToString().Replace("\"\"", "\"");
                try
                {
                    xml.LoadXml(sTraceFixed);
                }
                catch
                {
                    return;
                }
                // If removing additional quotes worked for the <trace> tag, we need to do the same for the payload
                sEWSData = sEWSData.Replace("\"\"", "\"");
            }

            // Read the trace information
            string sTag = xml.FirstChild?.Attributes["Tag"]?.Value.ToString();

            if (sTag.EndsWith("Headers"))
            {
                // Some logs can mangle headers (remove the CR/LF).  We try to unmangle them here
                if (!sEWSData.Contains(Environment.NewLine))
                {
                    // No newline in the headers implies they have all been merged onto one line
                    StringBuilder sHeaderData = new StringBuilder();
                    int copyPos = 0;
                    int colonPos = sEWSData.IndexOf(':');
                    while (colonPos>0)
                    {
                        // Step backward to previous whitespace, and insert newline
                        int whiteSpacePos = colonPos - 1;
                        while (sEWSData[whiteSpacePos] != ' ' && whiteSpacePos > copyPos)
                            whiteSpacePos--;
                        if (whiteSpacePos==copyPos)
                        {
                            // No whitespace found before already copied data, this is part of same header
                            sHeaderData.Append(sEWSData.Substring(copyPos, colonPos-copyPos));
                            copyPos = colonPos;
                            colonPos = sEWSData.IndexOf(':', colonPos + 1);
                        }
                        else
                        {
                            sHeaderData.AppendLine(sEWSData.Substring(copyPos, whiteSpacePos - copyPos));
                            copyPos = whiteSpacePos + 1;
                            colonPos = sEWSData.IndexOf(':', colonPos + 1);
                        }
                    }
                    if (copyPos < sEWSData.Length)
                        sHeaderData.AppendLine(sEWSData.Substring(copyPos));
                    sEWSData = sHeaderData.ToString();
                }
            }

            DateTime logTime = _lastKnownEventTime.AddTicks(1);
            string sLogTime = xml.FirstChild.Attributes["Time"]?.Value;
            if (!String.IsNullOrEmpty(sLogTime))
                logTime = DateTime.Parse(sLogTime);

            if (logTime.Equals(_lastKnownEventTime))
            {
                // If the log time is the same as the previous imported event, we add an offset so time sorting works (and assume that the requests
                // were sequential - this might break multithreaded traces and needs more testing)
                logTime = logTime.AddMilliseconds(_eventTimeOffset++);
            }
            else
            {
                _lastKnownEventTime = logTime;
                _eventTimeOffset = 1;
            }

            int threadId = -1;
            string sThreadId = xml.FirstChild.Attributes["Tid"]?.Value;
            if (!String.IsNullOrEmpty(sThreadId) )
            {
                if (!int.TryParse(sThreadId, out threadId))
                {
                    // We have a thread Id, but it isn't a number...
                    // We use a dictionary to map names to numbers
                    if (_threadNameToNumber.ContainsKey(sThreadId))
                    {
                        threadId = _threadNameToNumber[sThreadId];
                    }
                    else
                    {
                        threadId = _threadNameToNumber.Count;
                        _threadNameToNumber.Add(sThreadId, threadId);
                    }
                }
            }

            string sApplication = "Unknown";
            try
            {
                sApplication = xml.FirstChild.Attributes["Application"]?.Value;
            }
            catch { }
            string sVersion = "";
            try
            {
                sVersion = xml.FirstChild.Attributes["Version"]?.Value;
            }
            catch { }
                
            string sClientRequestId = "";
            try
            {
                sClientRequestId = xml.FirstChild.Attributes["ClientRequestId"]?.Value;
            }
            catch { }

            LogToDatabase(sEWSData,sTag,logTime,threadId, sApplication, sVersion, "", "", sClientRequestId);
        }

        public void ParseGenericRequest(string Request)
        {
            // Attempt to read an EWS request/response that has no <trace> tags
            if (Request.Length < 50)
                return; // There is no valid EWS request this short, so we don't even check it.  Could probably make this quite a bit bigger.
            try
            {
                Request = Request.Trim();
                XmlDocument xml = new XmlDocument();
                bool xmlLoaded = false;
                try
                {
                    if (Request.StartsWith("<?xml"))
                        xml.LoadXml(Request);
                    else
                        xml.LoadXml($"<?xml version=\"1.0\" encoding=\"utf - 16\"?>{Request}");
                    xmlLoaded = true;
                }
                catch {}
                if (!xmlLoaded)
                {
                    // This isn't valid XML.  We'll do a final check to see if we can find the closing <Envelope> tag and parse again
                    int i = Request.IndexOf("envelope>", StringComparison.OrdinalIgnoreCase);
                    if (i < 0) return;
                    Request = Request.Substring(0, i + 9);
                    try
                    {
                        xml.LoadXml(Request);
                        xmlLoaded = true;
                    }
                    catch { }
                }

                if (!xmlLoaded)
                    return;

                string sEWSMethod = ReadMethodFromRequest(xml);
                string sTag = "Unknown";
                if (!String.IsNullOrEmpty(sEWSMethod))
                {
                    sTag = "EwsRequest";
                    if ( (sEWSMethod.IndexOf("response", StringComparison.OrdinalIgnoreCase) > 0) || sEWSMethod.StartsWith("fault", StringComparison.OrdinalIgnoreCase))
                        sTag = "EwsResponse";
                }

                // Read the trace information
                DateTime logTime = _lastKnownEventTime.AddTicks(1);
                try
                {
                    logTime = DateTime.Parse(xml.FirstChild.Attributes["Time"].Value);
                }
                catch { }
                _lastKnownEventTime = logTime;

                LogToDatabase(Request, sTag, logTime, 1, "Unknown", "Unknown");
            }
            catch { }
        }

        public static string ReadMethodFromRequest(XmlDocument Request)
        {
            // Read the method (e.g. GetItem) from the request
            // This is used for the Log tree viewer
            // The method should be the first element in the SOAP body
            // If we fail to parse the Xml, or it is unexpected, we return null

            try
            {
                XmlNode nodeEnvelope = Request.DocumentElement;
                if (!nodeEnvelope.LocalName.Equals("envelope", StringComparison.OrdinalIgnoreCase))
                    return null;

                XmlNode nodeBody = null;
                foreach (XmlNode childNode in nodeEnvelope.ChildNodes)
                {
                    if (childNode.LocalName.Equals("body", StringComparison.OrdinalIgnoreCase))
                    {
                        nodeBody = childNode;
                        break;
                    }
                }
                if (nodeBody == null)
                    return null;

                XmlNode nodeRequestType = nodeBody.FirstChild;
                return nodeRequestType.LocalName;
            }
            catch { }
            return null;
        }

        public static string ReadMethodFromRequest(string Request)
        {
            // Read the method (e.g. GetItem) from the request
            // This is used for the Log tree viewer
            // The method should be the first element in the SOAP body
            // If we fail to parse the Xml, or it is unexpected, we return null

            try
            {
                XmlDocument oRequestXml = new XmlDocument();
                oRequestXml.LoadXml(Request.Trim());
                return ReadMethodFromRequest(oRequestXml);
            }
            catch
            {
                return null;
            }
        }

        public void LogWebHeaders(WebHeaderCollection Headers, string Description, string Url = "", HttpWebResponse Response = null)
        {
            // Log request headers
            string sHeaders = "";
            if (Response != null)
            {
                sHeaders += String.Format("{0} {1}{2}", (int)Response.StatusCode, Response.StatusDescription, Environment.NewLine);
            }
            if (!String.IsNullOrEmpty(Url))
            {
                sHeaders += String.Format("POST URL: {0}{1}{1}", Url, Environment.NewLine);
            }
            string clientRequestId = "";
            try
            {
                foreach (string sHeader in Headers.AllKeys)
                {
                    if (sHeader.Equals("Client-Request-Id", StringComparison.OrdinalIgnoreCase))
                        clientRequestId = Headers[sHeader];
                    sHeaders += sHeader + ": " + Headers[sHeader] + Environment.NewLine;
                }
                Log(sHeaders, Description, clientRequestId);
            }
            catch { }
        }

        public void LogCookies(CookieCollection Cookies, string Description)
        {
            // Log cookies
            try
            {
                if (Cookies.Count == 0) return;
                string sCookies = "";
                foreach (Cookie cookie in Cookies)
                {
                    sCookies += cookie.ToString() + Environment.NewLine;
                }
                Log(sCookies, Description);
            }
            catch { }
        }
    }
}