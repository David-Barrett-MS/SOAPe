using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;

namespace SOAPe
{
    public class TraceElement
    {
        // This class contains information on a single <trace> element

        private string _rawTrace = ""; // The whole of the trace element (includes <trace/> tags)
        private string _traceData = ""; // The data of the trace (e.g. the Xml from a request or response)
        private Dictionary<string, string> _traceTagAttributes; // Trace tag attributes (e.g. Tag, Time, Tid), names all lower-cased
        private Dictionary<string, string> _xmlElements; // We extract certain useful elements from the Xml

        public TraceElement(string Trace)
        {
            _rawTrace = Trace;
            _traceTagAttributes = TraceTagAttributes(_rawTrace);
            _traceData = ExtractTraceContent(_rawTrace);
            _xmlElements = InterestingXMLElements(_traceData);
        }

        public TraceElement(string TraceData, string TraceTag)
        {
            _rawTrace = TraceData;
            _traceTagAttributes = new Dictionary<string, string>();
            _traceTagAttributes.Add("tag", TraceTag);
            _traceData = TraceData;
            _xmlElements = InterestingXMLElements(_traceData);
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
                    attributes.Add(match.Groups[1].Value.ToLower(), match.Groups[2].Value);
                }
            }
            catch { }
            return attributes;
        }

        public static Color ThrottledColour { get; set; } = Color.Orange;
        public static Color ThrottledRequestColour { get; set; } = Color.DarkOrange;
        public static Color ErrorColour { get; set; } = Color.Red;
        public static Color ErrorRequestColour { get; set; } = Color.DarkRed;

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
                foreach (XmlNode xmlNode in bodyNode.FirstChild.ChildNodes)
                {
                    if (xmlNode.LocalName.Equals("FolderIds"))
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
                xmlDoc.LoadXml(Xml);
                return InterestingXMLElements(xmlDoc);
            }
            catch
            {
                // We'll get an error if the payload is not Xml, in which case we've nothing to do
                return new Dictionary<string, string>();
            }

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

        public string Data
        {
            get
            {
                return _traceData;
            }
        }

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
                if (this.IsThrottledResponse)
                {
                    if (this.TraceType == EWSTraceType.Request)
                        return TraceElement.ThrottledRequestColour;
                    return TraceElement.ThrottledColour;
                }
                if (this.IsErrorResponse)
                {
                    if (this.TraceType == EWSTraceType.Request)
                        return TraceElement.ErrorRequestColour;
                    return TraceElement.ErrorColour;
                }
                return null;
            }
        }

        public bool IsThrottledResponse
        {
            get
            {
                if (this.TraceType != EWSTraceType.Response)
                    return false;

                // Now check the response
                if (this.TraceTag.Contains("EwsResponse"))
                {
                    if (_traceData.Contains("ResponseCode>ErrorServerBusy"))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsErrorResponse
        {
            get
            {
                if (this.TraceType != EWSTraceType.Response)
                    return false;

                // Check if we have response headers...
                if (this.TraceTag.StartsWith("EwsResponseHttpHeaders") || this.TraceTag.StartsWith("Response Headers"))
                {
                    // Check for server error
                    try
                    {
                        string sHTTPResponse = _traceData.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[0];
                        if (!(sHTTPResponse.StartsWith("200")))
                            return true;
                    }
                    catch { }
                    return false;
                }

                // Now check the response
                if (this.TraceTag.Contains("EwsResponse"))
                {
                    if (_traceData.Contains("ResponseClass=\"Error\""))
                    {
                        return true;
                    }
                    if (_traceData.Contains("Fault>"))
                    {
                        if (_traceData.Contains("<faultcode"))
                            return true;
                    }
                }
                return false;
            }
        }
    }
}