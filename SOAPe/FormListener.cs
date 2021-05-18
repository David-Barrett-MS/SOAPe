/*
 * By David Barrett, Microsoft Ltd. 2012. Use at your own risk.  No warranties are given.
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
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Reflection;

namespace SOAPe
{
    public partial class FormListener : Form
    {
        private HttpListener _Listener = null;
        //private string _ListenURi = "";
        private int _ListenPort = 36728;
        private List<string> _Requests;
        private string _NotificationResponse = "";
        private ClassLogger _Logger = null;

        public FormListener(ClassLogger Logger)
        {
            InitializeComponent();

            StreamReader oReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("SOAPe.EWSTools.EWSSendNotificationResult.xml"));
            _NotificationResponse = oReader.ReadToEnd();
            oReader.Close();

            _Requests = new List<string>();
            _Listener = new HttpListener();

            hScrollBarReceived.Value = 0;
            hScrollBarReceived.Maximum = 0;
            hScrollBarReceived.Minimum = 0;

            _Logger = Logger;

            StartListening();
        }

        private void StartListening()
        {
            textBoxListenerURi.Text = "http://*:" + _ListenPort + "/" + Application.ProductName.ToString() + "/";
            _Listener.Prefixes.Clear();
            _Listener.Prefixes.Add(textBoxListenerURi.Text);
            try
            {
                _Listener.Start();
                _Listener.BeginGetContext(new AsyncCallback(ListenerCallback), _Listener);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Unable to start HTTP listener" + Environment.NewLine + "(are you running as administrator?)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxListenerURi.Text = "FAILED TO START LISTENER: " + ex.Message;
                textBoxListenerURi.Text = "";
                textBoxListenerURi.BackColor = Color.Red;
                textBoxListenerURi.ForeColor = Color.Black;
            }
        }

        public int Port
        {
            get
            {
                return _ListenPort;
            }
            set
            {
                if (value == _ListenPort) return;
                _Listener.Stop();
                _ListenPort = value;
                StartListening();
            }
        }

        public string URi
        {
            get
            {
                return textBoxListenerURi.Text;
            }
        }

        public void ListenerCallback(IAsyncResult result)
        {
            try
            {
                HttpListener listener = (HttpListener)result.AsyncState;
                
                HttpListenerContext context = listener.EndGetContext(result);
                HttpListenerRequest request = context.Request;
                string sRequest = "";

                using (StreamReader reader = new StreamReader(request.InputStream))
                {
                    sRequest = reader.ReadToEnd();
                    AddRequest(sRequest);
                }

                DetermineResponse(sRequest, context.Response);
                _Listener.BeginGetContext(new AsyncCallback(ListenerCallback), _Listener);
            }
            catch { }
        }

        private void DetermineResponse(string Request, HttpListenerResponse Response)
        {
            if (Request.Contains("exchange") && Request.Contains("SendNotificationResponseMessage"))
            {
                // This is an EWS notification message
                WriteEWSSubscriptionResponse(Response);
            }
            Response.OutputStream.Flush();
            Response.Close();
        }

        private void AddRequest(string Request)
        {
            _Requests.Add(Request);
            int iRequestCount = _Requests.Count;

            if (hScrollBarReceived.InvokeRequired)
            {
                hScrollBarReceived.Invoke((MethodInvoker)delegate
                {
                    hScrollBarReceived.Maximum = iRequestCount;
                    if (hScrollBarReceived.Minimum == 0)
                        hScrollBarReceived.Minimum = 1;
                    if (hScrollBarReceived.Value == (iRequestCount - 1))
                        hScrollBarReceived.Value = iRequestCount;
                    hScrollBarReceived.Refresh();
                });
            }
            else
            {
                hScrollBarReceived.Maximum = iRequestCount;
                if (hScrollBarReceived.Minimum == 0)
                    hScrollBarReceived.Minimum = 1;
                if (hScrollBarReceived.Value == (iRequestCount - 1))
                    hScrollBarReceived.Value = iRequestCount;
                hScrollBarReceived.Refresh();
            }
            Log(Request, "Notification");
            ShowCurrentRequest();
        }

        private void ShowCurrentRequest()
        {
            // Update info textbox (nav)
            string sShowing = hScrollBarReceived.Value + " of " + hScrollBarReceived.Maximum;

            if (textBoxReceivedShowing.InvokeRequired)
            {
                textBoxReceivedShowing.Invoke((MethodInvoker)delegate
                {
                    textBoxReceivedShowing.Text = sShowing;
                });
            }
            else
                textBoxReceivedShowing.Text = sShowing;

            // Update request textbox
            string sRequest = "";
            try
            {
                sRequest = _Requests[hScrollBarReceived.Value - 1];
            }
            catch { }
            if (xmlEditor1.InvokeRequired)
            {
                xmlEditor1.Invoke((MethodInvoker)delegate
                {
                    xmlEditor1.Text = sRequest;
                });
            }
            else
            {
                xmlEditor1.Text = sRequest;
            }
        }

        private void WriteEWSSubscriptionResponse(HttpListenerResponse Response)
        {
            // Send EWS OK response
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(_NotificationResponse);
            Response.ContentLength64 = buffer.Length;
            Response.ContentType = "text/xml";
            using (Stream output = Response.OutputStream)
            {
                output.Write(buffer, 0, buffer.Length);
            }
            Log(_NotificationResponse, "Notification Response");
        }

        private void hScrollBarReceived_ValueChanged(object sender, EventArgs e)
        {
            ShowCurrentRequest();
        }

        private void Log(string Details, string Description = "")
        {
            try
            {
                if (_Logger == null) return;
                _Logger.Log(Details, Description);
            }
            catch { }
        }

        private void buttonClearEvents_Click(object sender, EventArgs e)
        {
            _Requests = new List<string>();
            hScrollBarReceived.Maximum = 0;
            hScrollBarReceived.Minimum = 0;
            hScrollBarReceived.Value = 0;
            ShowCurrentRequest();
        }

        private void FormListener_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Release the listener
            try
            {
                _Listener.Stop();
                _Listener.Close();
            }
            catch { }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonEditListenUrl_Click(object sender, EventArgs e)
        {
            if (buttonEditListenUrl.Text=="Edit...")
            {
                textBoxListenerURi.Enabled = true;
                textBoxListenerURi.ReadOnly = false;
                buttonEditListenUrl.Text = "Apply";
                return;
            }

            textBoxListenerURi.ReadOnly = true;
            buttonEditListenUrl.Text = "Edit...";

            _Listener.Stop();
            _Listener.Prefixes.Clear();
            _Listener.Prefixes.Add(textBoxListenerURi.Text);
            try
            {
                _Listener.Start();
                _Listener.BeginGetContext(new AsyncCallback(ListenerCallback), _Listener);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Unable to start HTTP listener" + Environment.NewLine + "(are you running as administrator?)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxListenerURi.Text = "FAILED TO START LISTENER: " + ex.Message;
                textBoxListenerURi.Text = "";
                textBoxListenerURi.BackColor = Color.Red;
                textBoxListenerURi.ForeColor = Color.Black;
            }
        }
    }
}
