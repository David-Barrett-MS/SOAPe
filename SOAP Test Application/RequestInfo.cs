using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPe
{
    public class RequestInfo
    {
        public Dictionary<DateTime, string> Traffic;
        public string Name { get; set; }
        public bool ErrorPresent
        {
            get
            {
                // Check if we have response headers...
                foreach (string sData in Traffic.Values)
                {
                    if (sData.StartsWith("EwsResponseHttpHeaders") || sData.StartsWith("Response Headers"))
                    {
                        // Check for server error
                        string sHTTPResponse = sData.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[1];
                        if (!(sHTTPResponse.Contains("200 OK")))
                            return true;
                    }
                    else if (sData.Contains("EwsResponse"))
                    {
                        if (sData.Contains("ResponseClass=\"Error\""))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public RequestInfo(string name)
        {
            Name = name;
            Traffic = new Dictionary<DateTime, string>();
        }
    }

    public class RequestEventArgs : EventArgs
    {
        private RequestInfo _info;

        public RequestEventArgs(RequestInfo Info)
        {
            _info = Info;
        }

        public RequestInfo Info
        {
            get { return _info; }
        }
    }
}
