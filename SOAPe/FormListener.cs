/*
 * By David Barrett, Microsoft Ltd. 2012. Use at your own risk.  No warranties are given.
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Reflection;

namespace SOAPe
{
    public partial class FormListener : Form
    {
        private HttpListener _Listener = null;
        private string _ListenURi = "";
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
            string sMachineName = Environment.MachineName;
            try
            {
                System.DirectoryServices.ActiveDirectory.Domain oDomain = System.DirectoryServices.ActiveDirectory.Domain.GetComputerDomain();
                if (!String.IsNullOrEmpty(oDomain.Name))
                    sMachineName = sMachineName + "." + oDomain.Name;
            }
            catch { }
            _ListenURi = "http://" + sMachineName + ":" + _ListenPort + "/" + Application.ProductName.ToString() + "/";
            _Listener.Prefixes.Clear();
            _Listener.Prefixes.Add(_ListenURi);
            textBoxListenerURi.Text = _ListenURi;
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
                _ListenURi = "";
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
                return _ListenURi;
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

    }
}
