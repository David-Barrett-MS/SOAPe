/*
 * By David Barrett, Dark Bytes Ltd. 2016-2021. Use at your own risk.  No warranties are given.
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
