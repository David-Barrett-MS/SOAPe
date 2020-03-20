/*
 * By David Barrett, Microsoft Ltd. 2015. Use at your own risk.  No warranties are given.
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
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using System.Data.Common;
using System.Net;

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
        private string _currentProgress = "";
        private DataTable _logDataTable = null;
        private static Dictionary<string, int> _threadNameToNumber;
        private readonly object _lockObject = new object();
        private DateTime _lastKnownEventTime = DateTime.MinValue; // This is used to get the responses in the correct order for EWSEditor logs (to work around EWSEditor bug)

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
            _currentProgress = e.Progress;
            ProgressEventHandler handler = ProgressChanged;
            if (handler != null)
                handler(this, e);
        }

        public string Progress
        {
            get { return _currentProgress; }
        }

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
            string LogVersion, string Mailbox = "", string Impersonating = "")
        {
            // Write the trace information to the local database (which is used for the log viewer)

            DataRow row = _logDataTable.NewRow();
            row["Tag"] = Tag;
            row["Tid"] = ThreadId;
            row["Time"] = LogTime;
            row["Version"] = LogVersion;
            row["Application"] = LogApplication;
            row["SOAPMethod"] = ReadMethodFromRequest(Data);
            row["Data"] = Data;
            row["Size"] = Data.Length;
            row["Mailbox"] = Mailbox;
            row["ExchangeImpersonation"] = Impersonating;
            _logDataTable.Rows.Add(row);
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

            LogToDatabase(Details, TraceTag(Description), logTime, ThreadId, LogApplication, LogVersion);
            if (!SuppressEvent)
                OnLogAdded(new LogAddedEventArgs(new TraceElement(sTrace.ToString())));
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

            DateTime readStartTime = DateTime.Now;
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
                    int i = 0;
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
                                        OnProgressChanged(new ProgressEventArgs(String.Format("Processing {0}", i++), percentComplete));
                                    ParseTrace(sTrace.ToString());
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
                                            OnProgressChanged(new ProgressEventArgs(String.Format("Processing {0}", i++), percentComplete));
                                        sTrace.Remove(sTrace.Length - sTag.Length, sTag.Length);

                                        // Now we remove any trailing characters until we reach a > (which should be the closing tag of the Xml)
                                        string xmlData = sTrace.ToString();
                                        int closingTag = xmlData.LastIndexOf(">");
                                        if (closingTag>0)
                                        {
                                            xmlData = xmlData.Substring(0, closingTag+1);
                                        }
                                        ParseGenericRequest(xmlData);
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
                                        // to be presented this way, so we'll capture this Xml
                                        if (bFoundXmlTag)
                                        {
                                            // We've already found one Xml tag, so this file must contain more than one xml dump
                                            // We  process the collected trace as Xml
                                            if (ShowProgress)
                                                OnProgressChanged(new ProgressEventArgs(String.Format("Processing {0}", i++), percentComplete));
                                            sTrace.Remove(sTrace.Length - 4, 4);
                                            ParseGenericRequest(sTrace.ToString());
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
                            OnProgressChanged(new ProgressEventArgs(String.Format("Processing {0}", i++), percentComplete));
                        ParseGenericRequest(sTrace.ToString());
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
                    OnProgressChanged(new ProgressEventArgs(""));
                DebugLog(String.Format("Closing log file {0}", LogFileName));
                readLogFileStream.Close();
            }
            TimeSpan loadDuration = DateTime.Now.Subtract(readStartTime);
            DebugLog(String.Format("{0} loaded in {1}", LogFileName, loadDuration));
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
            try
            {
                XmlDocument xml = new XmlDocument();
                string sEWSData = "";
                try
                {
                    // We have invalid Xml (this happens when we have the actual requests/responses, as the tracer doesn't make nice Xml! :) )
                    // We strip out the contents (everything in the <Trace></Trace>), and try again
                    int iContentStart = Trace.IndexOf(">") + 1;
                    int iContentEnd = Trace.LastIndexOf("</Trace>", StringComparison.OrdinalIgnoreCase);
                    sEWSData = Trace.Substring(iContentStart, iContentEnd - iContentStart).TrimStart();
                    StringBuilder sTraceInfo = new StringBuilder(Trace.Substring(0, iContentStart)).Append(Trace.Substring(iContentEnd));
                    xml.LoadXml(sTraceInfo.ToString());
                }
                catch
                {
                    return;
                }

                // Read the trace information
                string sTag = xml.FirstChild.Attributes["Tag"].Value.ToString();
                DateTime logTime = _lastKnownEventTime.AddTicks(1);
                try
                {
                    logTime = DateTime.Parse(xml.FirstChild.Attributes["Time"].Value);
                }
                catch { }
                _lastKnownEventTime = logTime;
                int threadId = -1;
                try
                {
                    string sThreadId = xml.FirstChild.Attributes["Tid"].Value;
                    if (!int.TryParse(sThreadId, out threadId))
                    {
                        if (!String.IsNullOrEmpty(sThreadId))
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
                }
                catch { }
                string sApplication = "Unknown";
                try
                {
                    sApplication = xml.FirstChild.Attributes["Application"].Value;
                }
                catch { }
                string sVersion = "";
                try
                {
                    sVersion = xml.FirstChild.Attributes["Version"].Value;
                }
                catch { }

                LogToDatabase(sEWSData,sTag,logTime,threadId, sApplication, sVersion, "", "");
            }
            catch { }
        }

        public void ParseGenericRequest(string Request)
        {
            // Attempt to read an EWS request/response that has no <trace> tags
            try
            {
                Request = Request.Trim();
                XmlDocument xml = new XmlDocument();
                bool xmlLoaded = false;
                try
                {
                    xml.LoadXml(Request);
                    xmlLoaded = true;
                }
                catch {}
                if (!xmlLoaded)
                {
                    // This isn't valid XML.  We'll do a final check to see if we can find the closing <Envelope> tag and parse again
                    int i = Request.LastIndexOf("envelope>", StringComparison.OrdinalIgnoreCase);
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