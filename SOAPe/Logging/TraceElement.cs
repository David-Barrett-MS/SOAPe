using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace SOAPe
{
    public class TraceElement
    {
        // This class contains information on a single <trace> element

        private string _rawTrace = ""; // The whole of the trace element (includes <trace/> tags)
        private Dictionary<string, string> _traceTagAttributes; // Trace tag attributes (e.g. Tag, Time, Tid), names all lower-cased
        private Dictionary<string, string> _xmlElements; // We extract certain useful elements from the Xml
        private static List<string> _clientRequestIdsWithError = new List<string>();
        private static List<string> _clientRequestIdsThrottled = new List<string>();
        private bool _traceAnalysed = false;
        private bool _isThrottled = false;
        private bool _isError = false;

        private TraceElement()
        {
        }

        public TraceElement(string Trace)
        {
            // Create and analyse a trace
            _rawTrace = Trace;
            _traceTagAttributes = TraceTagAttributes(_rawTrace);
            Data = ExtractTraceContent(_rawTrace);
            _xmlElements = InterestingXMLElements(Data);
            AnalyseErrors(this);
            _traceAnalysed = true;
        }

        public TraceElement(string TraceData, string TraceTag)
        {
            // Create and analyse a trace
            _rawTrace = TraceData;
            _traceTagAttributes = TraceTagAttributes(TraceTag);
            _traceTagAttributes.Add("tag", TraceTag);
            Data = TraceData;
            _xmlElements = InterestingXMLElements(Data);
            AnalyseErrors(this);
            _traceAnalysed = true;
        }

        public static TraceElement CreateFromDataTableRow(DataRow dataRow)
        {
            // We create a TraceElement from the passed DataRow.  We do not perform any analysis at this stage

            TraceElement traceElement = new TraceElement();
            traceElement._traceTagAttributes = new Dictionary<string, string>();
            traceElement._xmlElements = new Dictionary<string, string>();

            foreach (DataColumn dataColumn in dataRow.Table.Columns)
            {
                switch (dataColumn.ColumnName)
                {
                    case "Data":
                        traceElement.Data = (string)dataRow["Data"];
                        break;

                    case "SOAPMethod":
                    case "Mailbox":
                    case "ExchangeImpersonation":
                        traceElement._xmlElements.Add(dataColumn.ColumnName.ToLower(), dataRow[dataColumn.ColumnName].ToString());
                        break;

                    default:
                        traceElement._traceTagAttributes.Add(dataColumn.ColumnName.ToLower(), dataRow[dataColumn.ColumnName].ToString());
                        break;
                }
            }

            /*
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
            row["ClientRequestId"] = ClientRequestId;
            */

            return traceElement;
        }

        public void Analyze()
        {
            if (_traceAnalysed)
            {
                if (TraceType == EWSTraceType.Request)
                    AnalyseErrors(this);  // We always recheck for errors as requests can only be identified retrospectively
                return;
            }
            AnalyseErrors(this);
            _xmlElements = InterestingXMLElements(Data);
            _traceAnalysed = true;
        }

        public static Dictionary<string, string> TraceTagAttributes(string TraceTag)
        {
            // Parse the trace tag and return the attibutes as a dictionary object
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            try
            {
                Regex regex = new Regex(" (?<attribute>.*?)=\"(?<value>.*?)\"", RegexOptions.Singleline);
                MatchCollection matches = regex.Matches(TraceTag);
                foreach (Match match in matches)
                {
                    try
                    {
                        attributes.Add(match.Groups[1].Value.ToLower(), match.Groups[2].Value);
                    }
                    catch { }
                }
            }
            catch { }
            return attributes;
        }

        public static Color ThrottledColour { get; set; } = Color.Orange;
        public static Color ThrottledRequestColour { get; set; } = Color.DarkOrange;
        public static Color ErrorColour { get; set; } = Color.Red;
        public static Color ErrorRequestColour { get; set; } = Color.Pink;

        private static string[] _mailboxContainingElements = { "FolderIds", "ParentFolderIds", "SavedItemFolderId" };
        public static Dictionary<string, string> InterestingXMLElements(XmlDocument xmlDoc)
        {
            // We have an Xml document, so we look for interesting information to extract

            Dictionary<string, string> xmlElements = new Dictionary<string, string>();

            // Analyze SOAP header
            XmlNodeList headerNodeList = xmlDoc.GetElementsByTagName("Header", "http://schemas.xmlsoap.org/soap/envelope/");
            if (headerNodeList.Count == 0)
                headerNodeList = xmlDoc.GetElementsByTagName("soap:Header");

            XmlNode headerNode = null;
            if (headerNodeList.Count == 1)
            {
                headerNode = headerNodeList[0];
            }

            // Look for Impersonation Header
            if (headerNode != null)
            {
                foreach (XmlNode xmlNode in headerNode.ChildNodes)
                {
                    if (xmlNode.LocalName.Equals("ExchangeImpersonation"))
                    {
                        foreach (XmlNode xmlImpersonationNode in xmlNode)
                        {
                            if (xmlImpersonationNode.LocalName == "ConnectingSID")
                            {
                                // We should have just one child node here, which will be the impersonated user
                                if (xmlImpersonationNode.ChildNodes.Count == 1)
                                {
                                    try
                                    {
                                        xmlElements.Add("ExchangeImpersonationType", xmlImpersonationNode.FirstChild.Name);
                                        xmlElements.Add("ExchangeImpersonation", xmlImpersonationNode.FirstChild.FirstChild.Value);
                                    }
                                    catch { }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            
            // Analyze SOAP Body
            XmlNodeList bodyNodeList = xmlDoc.GetElementsByTagName("Body", "http://schemas.xmlsoap.org/soap/envelope/");
            if (bodyNodeList.Count == 0)
                bodyNodeList = xmlDoc.GetElementsByTagName("soap:Body");
            XmlNode bodyNode = null;
            if (bodyNodeList.Count == 1)
            {
                bodyNode = bodyNodeList[0];
            }

            // Look for Mailbox (implies delegate access)
            if (bodyNode != null)
            {
                xmlElements.Add("SOAPMethod", bodyNode.FirstChild.LocalName);
                foreach (XmlNode xmlNode in bodyNode.FirstChild.ChildNodes)
                {
                    if (_mailboxContainingElements.Contains(xmlNode.LocalName))
                    {
                        foreach (XmlNode xmlFolderIdNode in xmlNode)
                        {
                            if (xmlFolderIdNode.LocalName == "DistinguishedFolderId")
                            {
                                // DistinguishedFolderId could have a mailbox element, so we check for that
                                // FolderIds contain mailbox info in the Id, so normal Ids can be ignored (we can't read the mailbox info)
                                foreach (XmlNode xmlDistinguishedFolderIdNode in xmlFolderIdNode)
                                {
                                    if (xmlDistinguishedFolderIdNode.LocalName.Equals("Mailbox"))
                                    {
                                        try
                                        {
                                            xmlElements.Add("Mailbox", xmlDistinguishedFolderIdNode.FirstChild.FirstChild.Value);
                                        }
                                        catch { }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return xmlElements;
        }

        public static Dictionary<string,string> InterestingXMLElements(string Xml)
        {

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                if (Xml.Substring(0, 6).Equals("<?xml ", StringComparison.OrdinalIgnoreCase))
                {
                    xmlDoc.LoadXml(Xml);
                    return InterestingXMLElements(xmlDoc);
                }
            }
            catch
            {
                // We'll get an error if the payload is not Xml, in which case we've nothing to do
            }
            return new Dictionary<string, string>();
        }

        public static string ExtractTraceContent(string Trace)
        {
            // Return the data from the trace (e.g. the Xml payload)
            try
            {
                Regex regex = new Regex("<trace.*?>(.*?)</trace>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                Match match = regex.Match(Trace);
                return match.Groups[1].Value.TrimStart();
            }
            catch { }
            return "";
        }

        public string Data { get; private set; } = "";

        public int TraceThreadId
        {
            get
            {
                try
                {
                    return int.Parse(_traceTagAttributes["tid"]);
                }
                catch { }
                return -1;
            }
        }

        public DateTime TraceTime
        {
            get
            {
                try
                {
                    return DateTime.Parse(_traceTagAttributes["time"]);
                }
                catch { }
                return DateTime.MinValue;
            }
        }

        public string TraceTag
        {
            get
            {
                if (_traceTagAttributes.ContainsKey("tag"))
                    return _traceTagAttributes["tag"];
                return "";
            }
        }

        public string SOAPMethod
        {
            get
            {
                return InterestingXmlElement("SOAPMethod");
            }
        }


        public string ClientRequestId
        {
            get
            {
                if (_traceTagAttributes.ContainsKey("clientrequestid"))
                    return _traceTagAttributes["clientrequestid"];
                return "";
            }
        }

        public EWSTraceType TraceType
        {
            get
            {
                string sTag = this.TraceTag.ToLower();
                if (sTag.Contains("request")) { return EWSTraceType.Request; }
                if (sTag.Contains("response")) { return EWSTraceType.Response; }
                if (sTag.Contains("autodiscover")) { return EWSTraceType.Autodiscover; }
                return EWSTraceType.Unknown;
            }
        }

        public string Mailbox
        {
            get
            {
                return InterestingXmlElement("Mailbox");
            }
        }

        public string Impersonating
        {
            get
            {
                return InterestingXmlElement("ExchangeImpersonation");
            }
        }

        public string InterestingXmlElement(string ElementTag)
        {
            if (_xmlElements.ContainsKey(ElementTag))
                return _xmlElements[ElementTag];
            return null;
        }

        public Color? HighlightColour
        {
            get
            {
                // We check for throttling first, as a throttled response can also be an error (in which case, we highlight the throttling, not the error)
                if (_isThrottled)
                {
                    if (this.TraceType == EWSTraceType.Request)
                        return TraceElement.ThrottledRequestColour;
                    return TraceElement.ThrottledColour;
                }
                if (_isError)
                {
                    if (this.TraceType == EWSTraceType.Request)
                        return TraceElement.ErrorRequestColour;
                    return TraceElement.ErrorColour;
                }
                return null;
            }
        }

        public static void AnalyseErrors(TraceElement traceElement)
        {
            if (traceElement.TraceType == EWSTraceType.Response)
            {
                // Check for errors

                // Check if we have response headers...
                if (traceElement.TraceTag.StartsWith("EwsResponseHttpHeaders") || traceElement.TraceTag.StartsWith("Response Headers"))
                {
                    // Check for server error
                    try
                    {
                        string sHTTPResponse = traceElement.Data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[0];
                        if (!(sHTTPResponse.StartsWith("200")))
                        {
                            if (!(sHTTPResponse.Contains(" 200 ")))
                                traceElement._isError = true;
                        }
                    }
                    catch { }
                }
                else if (traceElement.TraceTag.Contains("EwsResponse"))
                {
                    // Check EWS response for errors
                    if (traceElement.Data.Contains("ResponseClass=\"Error\""))
                        traceElement._isError = true;
                    else if (traceElement.Data.Contains("Fault>"))
                        if (traceElement.Data.Contains("<faultcode"))
                            traceElement._isError = true;
                }
                else if (traceElement.TraceTag.Contains("AutodiscoverResponse"))
                {
                    // Check autodiscover response for errors
                    if (traceElement.Data.Contains("<ErrorCode>"))
                        traceElement._isError = true;
                }

                if (traceElement._isError)
                    if (!String.IsNullOrEmpty(traceElement.ClientRequestId))
                        _clientRequestIdsWithError.Add(traceElement.ClientRequestId);

                // Check for throttling
                // Check for server busy response
                if (traceElement.TraceTag.Contains("EwsResponse"))
                {
                    if (traceElement.Data.Contains("ResponseCode>ErrorServerBusy"))
                        traceElement._isThrottled = true;
                    if (traceElement.Data.Contains("Too many concurrent connections opened."))
                        traceElement._isThrottled = true;
                }
                if (traceElement._isThrottled)
                    if (!String.IsNullOrEmpty(traceElement.ClientRequestId))
                        _clientRequestIdsThrottled.Add(traceElement.ClientRequestId);
            }
            else if (traceElement.TraceType == EWSTraceType.Autodiscover)
            {
                if (traceElement.Data.Contains("<ErrorCode>"))
                    traceElement._isError = true;

                if (traceElement._isError)
                    if (!String.IsNullOrEmpty(traceElement.ClientRequestId))
                        _clientRequestIdsWithError.Add(traceElement.ClientRequestId);
            }
            else
            {
                if (!String.IsNullOrEmpty(traceElement.ClientRequestId))
                {
                    traceElement._isThrottled = _clientRequestIdsThrottled.Contains(traceElement.ClientRequestId);
                    traceElement._isError = _clientRequestIdsWithError.Contains(traceElement.ClientRequestId);
                }
            }
        }

        public bool IsThrottled
        {
            get
            {
                return _isThrottled;
            }
        }

        public bool IsError
        {
            get
            {
                return _isError;
            }
        }
    }
}