using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SOAPe
{
    public class TraceElement
    {
        // This class contains information on a single <trace> element

        private string _rawTrace = ""; // The whole of the trace element (includes <trace/> tags)
        private string _traceData = ""; // The data of the trace (e.g. the Xml from a request or response)
        private Dictionary<string, string> _traceTagAttributes; // Trace tag attributes (e.g. Tag, Time, Tid), names all lower-cased

        public TraceElement(string Trace)
        {
            _rawTrace = Trace;
            _traceTagAttributes = TraceTagAttributes(_rawTrace);
            _traceData = ExtractTraceContent(_rawTrace);
        }

        public TraceElement(string TraceData, string TraceTag)
        {
            _rawTrace = TraceData;
            _traceTagAttributes = new Dictionary<string, string>();
            _traceTagAttributes.Add("tag", TraceTag);
            _traceData = TraceData;
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
                        if (!(sHTTPResponse.Contains("200 OK")))
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