/*
 * By David Barrett, Microsoft Ltd. 2012 - 2014. Use at your own risk.  No warranties are given.
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
using System.Xml;
using System.IO;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Reflection;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using SOAPe.EWSTools;
using System.Threading;
using SOAPe.Auth;
using SOAPe.ConfigurationManager;

namespace SOAPe
{
    public partial class FormMain : Form
    {
        private FormListener _httpListener = null;
        private FormLogViewer _logViewer = null;
        private FormEWSAutodiscover _autodiscoverForm = null;
        private ClassLogger _logger = null;
        private string _userSMTPAddress = "";
        private static bool _ignoreSSLErrors = false;
        private X509Certificate2 _authCertificate = null;
        private ConfigurationManager.ClassFormConfig _formConfig = null;
        private Auth.FormAzureApplicationRegistration _oAuthAppRegForm=null;

        public FormMain(bool DebugLogging)
        {
            InitializeComponent();

            // Add our form configuration helper
            _formConfig = new ConfigurationManager.ClassFormConfig(this, true);
            _formConfig.AddControlTypeRecurseExclusion("SOAPe.XmlEditor");
            _formConfig.ExcludedControls.Add(groupBoxResponse);
            _formConfig.ExcludedControls.Add(xmlEditorResponse);
            ClassFormConfig.ApplyConfiguration();

            // Configure log file
            if (String.IsNullOrEmpty(textBoxLogFolder.Text)) textBoxLogFolder.Text = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            if (String.IsNullOrEmpty(textBoxLogFileName.Text)) textBoxLogFileName.Text = "SOAPe.log";
            _logger = new ClassLogger(LogFileName(), DebugLogging);

            LoadCertificate(textBoxAuthCertificate.Text);

            this.Text = Application.ProductName + " v" + Application.ProductVersion;
            _logger.DebugLog(this.Text);

            // Hook up the cert callback.  
            ServicePointManager.ServerCertificateValidationCallback = ValidateCertificate;

            GetUserSMTPAddress();
            UpdateAuthUI();
            UpdateHTTPHeaderControls();
            UpdateHTTPCookieControls();
            xmlEditorResponse.XmlValidationComplete += xmlEditorResponse_XmlValidationComplete;

            try
            {
                if (!NativeMethods.IsProcessElevated())
                {
                    // The HTTP listener requires elevation, check if we have this  hTTPListenerToolStripMenuItem
                    hTTPListenerToolStripMenuItem.Image = NativeMethods.GetStockIcon(NativeMethods.SHSTOCKICONID.SIID_SHIELD, NativeMethods.SHGSI.SHGSI_ICON).ToBitmap();
                }
            }
            catch { }
            /*
            if (System.Diagnostics.Debugger.IsAttached)
            {
                FormUserControlTest oForm = new FormUserControlTest();
                oForm.Show(this);
            }
             */
            _logger.DebugLog("Initialisation complete");
        }

        private string LogFileName()
        {
            // Return the full log filename

            return String.Format("{0}\\{1}", textBoxLogFolder.Text, textBoxLogFileName.Text);
        }

        private static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (_ignoreSSLErrors || (errors == SslPolicyErrors.None))
                return true;

            return false;
        }

        public FormListener HTTPListener
        {
            get
            {
                if (_httpListener == null)
                {
                    _httpListener = new FormListener(new ClassLogger(Path.GetDirectoryName(Application.ExecutablePath) + "\\Listener.log"));
                    _httpListener.Show();
                }
                if (_httpListener.IsDisposed)
                {
                    _httpListener = new FormListener(new ClassLogger(Path.GetDirectoryName(Application.ExecutablePath) + "\\Listener.log"));
                    _httpListener.Show();
                }
                return _httpListener;
            }
        }

        private ICredentials CurrentCredentials
        {
            get
            {
                ICredentials oCredential;
                if (radioButtonDefaultCredentials.Checked)
                {
                    oCredential = CredentialCache.DefaultCredentials;
                }
                else
                {
                    if (radioButtonOAuth.Checked)
                        return null;
                    oCredential = new NetworkCredential(textBoxUsername.Text, textBoxPassword.Text);
                    if (!String.IsNullOrEmpty(textBoxDomain.Text))
                       (oCredential as NetworkCredential).Domain = textBoxDomain.Text;
                    
                }
                return oCredential;
            }
        }

        private void GetUserSMTPAddress()
        {
            // We dump this onto a background thread as otherwise we get an unacceptable delay
            // when starting SOAPe (at least, we do when not in a domain)
            ThreadPool.QueueUserWorkItem(new WaitCallback(GetUserSMTPAddressWorker), null);
        }

        void GetUserSMTPAddressWorker(object a)
        {
            // We will try to retrieve SMTP address of current user.

            _logger.DebugLog("Attempting to retrieve SMTP address of current user");
            try
            {
                // Populate domain first of all
                string sDomain;
                using (DirectoryEntry rootDSE = new DirectoryEntry("LDAP://RootDSE"))
                {
                    sDomain = rootDSE.Properties["defaultNamingContext"].Value.ToString();
                }

                if (sDomain == "") return;

                sDomain = sDomain.Replace("DC=", "").Replace(",", ".");
                if (String.IsNullOrEmpty(textBoxDomain.Text) && String.IsNullOrEmpty(textBoxUsername.Text))
                {
                    if (textBoxDomain.InvokeRequired)
                    {
                        textBoxDomain.Invoke(new MethodInvoker(delegate()
                        {
                            textBoxDomain.Text = sDomain;
                        }));
                    }
                    else
                        textBoxDomain.Text = sDomain;
                }
                _logger.DebugLog("Current domain: " + sDomain);

                using (DirectoryEntry userDE = new DirectoryEntry("LDAP://" + UserPrincipal.Current.DistinguishedName.ToString()))
                {
                    _userSMTPAddress = userDE.Properties["mail"].Value.ToString();
                }
                _logger.DebugLog("Current user's SMTP address: " + _userSMTPAddress);
            }
            catch (Exception ex)
            {
                // Failed to resolve name, so don't update
                _logger.DebugLogError(ex, "GetUserSMTPAddress");
                return;
            }
        }

        private void SetSecurityProtocol(ClassSOAP oSOAP)
        {
            // Set security protocol as per form settings
            if (!(checkBoxTLS1_0.Checked || checkBoxTLS1_1.Checked || checkBoxTLS1_2.Checked))
            {
                oSOAP.SecurityProtocol = SecurityProtocolType.Ssl3;
                return;
            }
            SecurityProtocolType securityProtocolType = 0;
            if (checkBoxTLS1_0.Checked)
                securityProtocolType = securityProtocolType | SecurityProtocolType.Tls;
            if (checkBoxTLS1_1.Checked)
                securityProtocolType = securityProtocolType | SecurityProtocolType.Tls11;
            if (checkBoxTLS1_2.Checked)
                securityProtocolType = securityProtocolType | SecurityProtocolType.Tls12;
            oSOAP.SecurityProtocol = securityProtocolType;
        }

        public CredentialHandler CredentialHandler()
        {
            // Create a CredentialHandler object with the selected credentials
            CredentialHandler credentialHandler;
            if (radioButtonNoAuth.Checked)
            {
                credentialHandler = new CredentialHandler(AuthType.None);
            }
            else if (radioButtonCertificateAuthentication.Checked)
            {
                credentialHandler = new CredentialHandler(AuthType.Certificate);
                credentialHandler.Certificate = _authCertificate;
            }
            else if (radioButtonOAuth.Checked)
            {
                credentialHandler = new CredentialHandler(AuthType.OAuth);
                credentialHandler.OAuthToken = textBoxOAuthToken.Text;
            }
            else
            {
                credentialHandler = new CredentialHandler(AuthType.Basic);
                credentialHandler.Username = textBoxUsername.Text;
                credentialHandler.Password = textBoxPassword.Text;
                if (!String.IsNullOrEmpty(textBoxDomain.Text))
                    credentialHandler.Domain = textBoxDomain.Text;
            }
            return credentialHandler;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (checkBoxUpdateEWSHeader.Checked)
                UpdateSOAPHeader();
            string sSOAPRequest = xmlEditorRequest.Text;
            string sSOAPResponse = "";
            string sErrorResponse = "";

            xmlEditorResponse.Text = "";
            HighlightResponseGroupbox(false);

            buttonSend.Enabled = false;
            xmlEditorRequest.Enabled = false;
            this.Update();

            CredentialHandler credentialHandler = CredentialHandler();
            ClassSOAP oSOAP = new ClassSOAP(textBoxURL.Text, _logger, credentialHandler);

            SetSecurityProtocol(oSOAP);
            oSOAP.BypassWebProxy = checkBoxBypassProxySettings.Checked;
            if ( (listViewHTTPHeaders.Items.Count > 0) || radioButtonOAuth.Checked)
            {
                // Add the HTTP headers
                List<string[]> headers = new List<string[]>();
                foreach (ListViewItem item in listViewHTTPHeaders.Items)
                {
                    string[] sHeader = new string[2];
                    sHeader[0] = item.Text;
                    sHeader[1] = item.SubItems[1].Text;
                    headers.Add(sHeader);
                }
                if (radioButtonOAuth.Checked)
                {
                    oSOAP.AuthorizationHeader = String.Format("Bearer {0}", textBoxOAuthToken.Text); // Add OAuth token
                }
                oSOAP.HTTPHeaders = headers;
            }

            sSOAPResponse = oSOAP.SendRequest(sSOAPRequest, out sErrorResponse, RequestCookies());

            xmlEditorResponse.Text = sSOAPResponse;
            if (!String.IsNullOrEmpty(sErrorResponse))
            {
                HighlightResponseGroupbox(true, sErrorResponse);
            }

            // Store any cookies we have had returned
            PersistCookies(oSOAP.ResponseCookies);
            UpdateHTTPCookieControls();
            UpdateHTTPHeaderControls();
            buttonSend.Enabled = true;
            xmlEditorRequest.Enabled = true;
        }

        private void PersistCookies(CookieCollection Cookies)
        {
            if (!checkBoxPersistCookies.Checked)
                return;

            for (int i = listViewHTTPCookies.Items.Count - 1; i > -1; i-- )
            {
                if ((string)listViewHTTPCookies.Items[i].Tag != "User cookie")
                    listViewHTTPCookies.Items.RemoveAt(i);
            }
            if (Cookies == null)
                return;

            foreach (Cookie cookie in Cookies)
            {
                try
                {
                    ListViewItem item = new ListViewItem(cookie.Name);
                    item.Name = cookie.Name;
                    item.SubItems.Add(cookie.Value);
                    listViewHTTPCookies.Items.Add(item);
                }
                catch { }
            }
        }

        private CookieCollection RequestCookies()
        {
            CookieCollection requestCookies = new CookieCollection();

            foreach (ListViewItem item in listViewHTTPCookies.Items)
            {
                try
                {
                    requestCookies.Add(new Cookie(item.SubItems[0].Text, item.SubItems[1].Text));
                }
                catch { }
            }
            return requestCookies;
        }

        private void UpdateAuthUI()
        {
            bool bOAuthVisible = radioButtonOAuth.Checked;
            bool bUserCredsVisible = radioButtonSpecificCredentials.Checked;
            bool bCertAuthVisible = radioButtonCertificateAuthentication.Checked;

            // Hide/show credentials boxes
            textBoxUsername.Visible = bUserCredsVisible;
            textBoxPassword.Visible = bUserCredsVisible;
            textBoxDomain.Visible = bUserCredsVisible;
            textBoxDomain.Enabled = bUserCredsVisible;
            labelUsername.Visible = bUserCredsVisible;
            labelPassword.Visible = bUserCredsVisible;
            labelDomain.Visible = bUserCredsVisible;

            // Hide/show OAuth
            textBoxOAuthToken.Visible = bOAuthVisible;
            labelOAuthToken.Visible = bOAuthVisible;
            buttonAcquireOAuthToken.Visible = bOAuthVisible;
            buttonAppRegistration.Visible = bOAuthVisible;
            if (bOAuthVisible)
            {
                // Ensure that we have our form loaded for application settings
                if (_oAuthAppRegForm is null)
                {
                    _oAuthAppRegForm = new Auth.FormAzureApplicationRegistration();
                }
            }

            // Hide/show certificate auth
            textBoxAuthCertificate.Visible = bCertAuthVisible;
            buttonChooseCertificate.Visible = bCertAuthVisible;
        }

        private string LoadTemplate(string ItemId = "", string FolderId = "", string TemplateName = "")
        {
            // Reads the XML template
            string sTemplateContent = String.Empty;
            FormReplaceTemplateFields oForm = new FormReplaceTemplateFields(ItemId, FolderId, TemplateName);
            sTemplateContent = oForm.ReadTemplate(this);
            oForm.Dispose();

            if (String.IsNullOrEmpty(sTemplateContent))
                return xmlEditorRequest.Text;

            this.Activate(); // To stop the main window from disappearing behind other applications (Windows 10 z-order issues)
            return sTemplateContent;
        }

        private void UpdateSOAPHeader()
        {
            // Update the SOAP header as required

            // We want to just update the ExchangeVersion and ApplicationImpersonation values, and leave anything else alone
            XmlDocument xmlRequest = new XmlDocument();
            try
            {
                xmlRequest.LoadXml(xmlEditorRequest.Text);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(this, String.Format("Failed to load request Xml: {0}{1}", Environment.NewLine, ex.Message), "Error updating header", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlNodeList headerNodeList = xmlRequest.GetElementsByTagName("Header", "http://schemas.xmlsoap.org/soap/envelope/");
            if (headerNodeList.Count==0)
                headerNodeList = xmlRequest.GetElementsByTagName("soap:Header");

            XmlNode headerNode;
            if (headerNodeList.Count>1)
            {
                // We have more than one header - we can't do anything here
                System.Windows.Forms.MessageBox.Show(this, "Multiple <Header> elements found.", "Error updating header", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (headerNodeList.Count < 1)
            {
                // We don't currently have a header, so add one
                headerNode = xmlRequest.CreateNode(XmlNodeType.Element, "Header", "http://schemas.xmlsoap.org/soap/envelope/");
            }
            else
            {
                headerNode = headerNodeList[0];
            }

            // Create RequestedServerVersion node
            XmlNode requestServerVersionNode = xmlRequest.CreateNode(XmlNodeType.Element, "RequestServerVersion", "http://schemas.microsoft.com/exchange/services/2006/types");
            if (comboBoxRequestServerVersion.SelectedIndex>0)
            {
                XmlAttribute xmlAttribute = xmlRequest.CreateAttribute("Version");
                xmlAttribute.Value = comboBoxRequestServerVersion.Text;
                requestServerVersionNode.Attributes.Append(xmlAttribute);
            }
            else
                requestServerVersionNode = null;

            // Create ExchangeImpersonation node
            XmlNode exchangeImpersonationNode = null;
            if (!String.IsNullOrEmpty(textBoxImpersonationSID.Text))
            {
                exchangeImpersonationNode = xmlRequest.CreateNode(XmlNodeType.Element, "ExchangeImpersonation", "http://schemas.microsoft.com/exchange/services/2006/types");
                XmlNode connectingSID = xmlRequest.CreateNode(XmlNodeType.Element, "ConnectingSID", "http://schemas.microsoft.com/exchange/services/2006/types");
                XmlNode impersonatedId = null;

                switch (comboBoxImpersonationMethod.SelectedIndex)
                {
                    case 0:
                        {
                            impersonatedId = xmlRequest.CreateNode(XmlNodeType.Element, "PrimarySmtpAddress", "http://schemas.microsoft.com/exchange/services/2006/types");
                            break;
                        }

                    case 1:
                        {
                            impersonatedId = xmlRequest.CreateNode(XmlNodeType.Element, "PrincipalName", "http://schemas.microsoft.com/exchange/services/2006/types");
                            break;
                        }

                    case 2:
                        {
                            impersonatedId = xmlRequest.CreateNode(XmlNodeType.Element, "SID", "http://schemas.microsoft.com/exchange/services/2006/types");
                            break;
                        }

                    default:
                        break;
                }
                if (impersonatedId == null)
                {
                    System.Windows.Forms.MessageBox.Show(this, "Invalid impersonation type", "Error updating header", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                impersonatedId.InnerText = textBoxImpersonationSID.Text;
                connectingSID.AppendChild(impersonatedId);
                exchangeImpersonationNode.AppendChild(connectingSID);
            }

            // Delete the existing Xml elements (if they exist)
            List<XmlNode> nodesToDelete = new List<XmlNode>();
            foreach (XmlNode xmlNode in headerNode.ChildNodes)
            {
                if (xmlNode.Name.ToLower().EndsWith("requestserverversion") || xmlNode.Name.ToLower().EndsWith("exchangeimpersonation"))
                {
                    nodesToDelete.Add(xmlNode);
                }
            }
            foreach (XmlNode xmlNode in nodesToDelete)
                headerNode.RemoveChild(xmlNode);

            // Add the updated Xml elements
            if (requestServerVersionNode != null)
            {
                // This should be the first element, so we need to insert it if there are other elements already present
                if (headerNode.HasChildNodes)
                {
                    headerNode.InsertBefore(requestServerVersionNode, headerNode.FirstChild);
                }
                else
                    headerNode.AppendChild(requestServerVersionNode);
            }
            if (exchangeImpersonationNode != null)
            {
                // This should be the last element, so we can just append
                headerNode.AppendChild(exchangeImpersonationNode);
            }

            if (headerNodeList.Count == 1)
            {
                // Replace the header
                if (headerNode.HasChildNodes)
                {
                    xmlRequest.FirstChild.NextSibling.ReplaceChild(headerNode, headerNodeList[0]);
                }
                else if (!headerNodeList[0].HasChildNodes)
                    xmlRequest.FirstChild.NextSibling.RemoveChild(headerNodeList[0]);
            }
            else
            {
                // Add a header (one doesn't currently exist)
                if (headerNode.HasChildNodes)
                {
                    XmlNode envelopeNode = xmlRequest.FirstChild.NextSibling;
                    if (envelopeNode.Name.ToLower().EndsWith("envelope"))
                    {
                        envelopeNode.InsertBefore(headerNode, envelopeNode.FirstChild);
                    }
                    else
                    {
                        // We don't appear to have a soap envelope, which is fatal
                        System.Windows.Forms.MessageBox.Show(this, "Couldn't find <envelope> in SOAP request.", "Error updating header", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Check if there is anything left in the header, and if not, remove it
                    
                }
            }

            // Apply the updated header to the document
            xmlEditorRequest.Text = xmlRequest.OuterXml;
        }


        private void autodiscoverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show warning...

        }

        private void radioButtonDefaultCredentials_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthUI();
        }

        private void radioButtonSpecificCredentials_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthUI();
        }

        private void checkBoxForceBasicAuth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthUI();
        }

        private void buttonLoadTemplate_Click(object sender, EventArgs e)
        {
            xmlEditorRequest.Text = LoadTemplate();
        }

        private void hTTPListenerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!NativeMethods.IsRunAsAdmin())
            {
                if (MessageBox.Show(this, "Creating an HTTP listener requires elevation - restart application?", "Elevation required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                // Launch SOAPe with elevated permissions
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Application.ExecutablePath;
                proc.Verb = "runas";

                try
                {
                    Process.Start(proc);
                }
                catch
                {
                    // The user refused the elevation.
                    // Do nothing and return directly ...
                    return;
                }

                Application.Exit();  // Quit itself
            }
            this.HTTPListener.Show();
        }


        private void textBoxImpersonationSID_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxImpersonationSID.Text))
            {
                AddHTTPRequestHeader("X-AnchorMailbox", textBoxImpersonationSID.Text);
            }
            else
                DeleteHTTPRequestHeader("X-AnchorMailbox");
            UpdateHTTPHeaderControls();
        }

        private void LoadCertificate(string Subject)
        {
            if (String.IsNullOrEmpty(Subject))
                return;

            // We look for a certificate with the same name in the user's store, and load it if it exists
            // We don't load certificates from file (user has to manually do this each time SOAPe starts)

            X509Store x509Store = null;
            try
            {
                x509Store = new X509Store("MY", StoreLocation.CurrentUser);
                x509Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            }
            catch { }

            if (x509Store == null)
                return;

            // Store opened ok, so now we read the certificates
            foreach (X509Certificate2 x509Cert in x509Store.Certificates)
            {
                try
                {
                    if (x509Cert.Subject.Equals(Subject))
                    {
                        // This is the certificate, so load it
                        _authCertificate = x509Cert;
                        textBoxAuthCertificate.Text = _authCertificate.Subject;
                        break;
                    }
                }
                catch { }
            }

            x509Store.Close();
        }

          private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _httpListener.Dispose();
                _logger = null;
                GC.Collect();
            }
            catch { }
        }

        private void OpenLogViewer()
        {
            if (_logViewer == null)
            {
                _logViewer = new FormLogViewer(_logger);
                _logViewer.Show();
                return;
            }

            if (_logViewer.HaveLoadedLog)
            {
                // Log viewer has another log (not SOAPe's) loaded, so we will open a new window attached to SOAPe's
                // (we only need to keep track of the last opened window, as it can't be reattached to the SOAPe log)
                _logViewer = new FormLogViewer(_logger);
                _logViewer.Show();
                return;
            }
            if (_logViewer.Visible)
            {
                _logViewer.Focus();
                return;
            }

            try
            {
                _logViewer.Show();
                return;
            }
            catch { }
            _logViewer = new FormLogViewer(_logger);
            _logViewer.Show();
        }

        private void logViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLogViewer();
        }

        private void checkBoxIgnoreCertErrors_CheckedChanged(object sender, EventArgs e)
        {
            _ignoreSSLErrors = checkBoxIgnoreCertErrors.Checked;
        }


        private void buttonHTTPHeadersClear_Click(object sender, EventArgs e)
        {
            listViewHTTPHeaders.Items.Clear();
            UpdateHTTPHeaderControls();
        }

        private void buttonHTTPHeaderRemove_Click(object sender, EventArgs e)
        {
            if (listViewHTTPHeaders.SelectedItems.Count!=1)
                return;

            listViewHTTPHeaders.Items[listViewHTTPHeaders.SelectedIndices[0]].Remove();
        }

        private void DeleteHTTPRequestHeader(string HeaderName)
        {
            try
            {
                ListViewItem[] items = listViewHTTPHeaders.Items.Find(HeaderName, false);
                if (items.Length > 0)
                    items[0].Remove();
            }
            catch { }
        }

        private void AddHTTPRequestHeader(string HeaderName, string HeaderValue)
        {
            DeleteHTTPRequestHeader(HeaderName);

            try
            {
                ListViewItem item = new ListViewItem(HeaderName);
                item.Name = HeaderName;
                item.SubItems.Add(HeaderValue);
                listViewHTTPHeaders.Items.Add(item);
            }
            catch { }
        }

        private void buttonHTTPHeaderAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxHTTPHeaderName.Text))
                return;

            AddHTTPRequestHeader(textBoxHTTPHeaderName.Text, textBoxHTTPHeaderValue.Text);
            textBoxHTTPHeaderName.Text = "";
            textBoxHTTPHeaderValue.Text = "";
            UpdateHTTPHeaderControls();
        }

        private void UpdateHTTPHeaderControls()
        {
            buttonHTTPHeadersClear.Enabled = (listViewHTTPHeaders.Items.Count > 0);
            buttonHTTPHeaderRemove.Enabled = (listViewHTTPHeaders.SelectedItems.Count > 0);
            buttonHTTPHeaderAdd.Enabled = !String.IsNullOrEmpty(textBoxHTTPHeaderName.Text);
        }

        private void UpdateHTTPCookieControls()
        {
            buttonHTTPCookiesClear.Enabled = (listViewHTTPCookies.Items.Count > 0);
            buttonHTTPCookieRemove.Enabled = (listViewHTTPCookies.SelectedItems.Count > 0);
            buttonHTTPCookieAdd.Enabled = !String.IsNullOrEmpty(textBoxHTTPCookieName.Text);
        }

        private void textBoxHTTPHeaderName_TextChanged(object sender, EventArgs e)
        {
            UpdateHTTPHeaderControls();
        }

        private void comboBoxRequestServerVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRequestServerVersion.SelectedIndex > 0)
                checkBoxUpdateEWSHeader.Checked = true;
        }

        public string GetImpersonationHeader()
        {
            string sHeader = ReadImpersonationTemplate();
            string sIDType = "";
            if (comboBoxImpersonationMethod.SelectedIndex < 0) return "";
            if (String.IsNullOrEmpty(textBoxImpersonationSID.Text)) return "";


            switch (comboBoxImpersonationMethod.Text.Substring(0, 1).ToLower())
            {
                case "p":
                    // Primary SMTP Address
                    sIDType = "PrimarySmtpAddress";
                    break;

                case "u":
                    // User Principal Name
                    sIDType = "PrincipalName";
                    break;

                case "s":
                    // SID
                    sIDType = "SID";
                    break;
            }
            if (String.IsNullOrEmpty(sIDType)) return "";
            textBoxImpersonationSID.Tag = sIDType;
            sHeader = sHeader.Replace("IMPERSONATIONTAG>", String.Format("{0}>", sIDType));
            sHeader = sHeader.Replace("%IMPERSONATIONID%", textBoxImpersonationSID.Text);
            return sHeader;
        }

        private string ReadImpersonationTemplate()
        {
            // Reads the XML template
            string sTemplate = "";
            try
            {
                Assembly oAssembly = Assembly.GetExecutingAssembly();
                StreamReader oReader = new StreamReader(oAssembly.GetManifestResourceStream(String.Format("{0}.EWSTools.EWSImpersonationHeader.xml", Application.ProductName)));
                sTemplate = oReader.ReadToEnd();
                oReader.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Unexpected error reading impersonation template: {0}", ex.Message), "Failed to apply impersonation", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            return sTemplate;
        }

        private void listViewHTTPHeaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxHTTPHeaderName.Text = listViewHTTPHeaders.SelectedItems[0].SubItems[0].Text;
                textBoxHTTPHeaderValue.Text = listViewHTTPHeaders.SelectedItems[0].SubItems[1].Text;
            }
            catch { }
            UpdateHTTPHeaderControls();
        }

        private void buttonHTTPCookiesAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxHTTPCookieName.Text))
                return;

            try
            {
                ListViewItem[] items = listViewHTTPCookies.Items.Find(textBoxHTTPCookieName.Text, false);
                if (items.Length > 0)
                    items[0].Remove();
            }
            catch { }

            try
            {
                ListViewItem item = new ListViewItem(textBoxHTTPCookieName.Text);
                item.Name = textBoxHTTPCookieName.Text;
                item.Tag = "User cookie";
                item.SubItems.Add(textBoxHTTPCookieValue.Text);
                listViewHTTPCookies.Items.Add(item);
                textBoxHTTPCookieName.Text = "";
                textBoxHTTPCookieValue.Text = "";
            }
            catch { }
        }

        private void listViewHTTPCookies_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxHTTPCookieName.Text = listViewHTTPCookies.SelectedItems[0].SubItems[0].Text;
                textBoxHTTPCookieValue.Text = listViewHTTPCookies.SelectedItems[0].SubItems[1].Text;
            }
            catch { }
            UpdateHTTPCookieControls();
        }

        private void textBoxHTTPCookieName_TextChanged(object sender, EventArgs e)
        {
            UpdateHTTPCookieControls();
        }

        private void buttonHTTPCookiesClear_Click(object sender, EventArgs e)
        {
            listViewHTTPCookies.Items.Clear();
            UpdateHTTPCookieControls();
        }

        private void buttonHTTPCookieRemove_Click(object sender, EventArgs e)
        {
            if (listViewHTTPCookies.SelectedItems.Count != 1)
                return;

            listViewHTTPCookies.Items[listViewHTTPCookies.SelectedIndices[0]].Remove();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog(this);
        }

        private void xmlEditorResponse_SendItemIdToTemplate(object sender, SendItemIdEventArgs e)
        {
            xmlEditorRequest.Text = LoadTemplate(e.ItemId, e.FolderId);
        }

        private void HighlightResponseGroupbox(bool ShowError, string AdditionalInfo = "", string ToolTip = "")
        {
            string title = "Response";
            if (!String.IsNullOrEmpty(AdditionalInfo))
                title += " - " + AdditionalInfo;

            if (groupBoxResponse.InvokeRequired)
            {
                groupBoxResponse.Invoke(new MethodInvoker(delegate()
                {
                    groupBoxResponse.Highlighted = ShowError;
                    groupBoxResponse.Text = title;
                    toolTips.SetToolTip(groupBoxResponse, ToolTip);
                    this.Refresh();
                }));
            }
            else
            {
                groupBoxResponse.Highlighted = ShowError;
                groupBoxResponse.Text = title;
                toolTips.SetToolTip(groupBoxResponse, ToolTip);
                this.Refresh();
            }
            
        }

        void xmlEditorResponse_XmlValidationComplete(object sender, XmlValidatedEventArgs e)
        {
            // This event occurs once the Xml in the response has been validated
            // If validation fails, we report it

            if (e.ValidationErrors.Count>0)
            {
                HighlightResponseGroupbox(true, "FAILED XML VALIDATION", String.Join(Environment.NewLine, e.ValidationErrors));
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormUserControlTest frm = new FormUserControlTest();
            frm.Show(this);
        }

        private void radioButtonOAuth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthUI();
        }

        private void UpdateUrl()
        {
            if (radioButtonUrlOffice365.Checked)
            {
                if (!textBoxURL.Text.Equals(radioButtonUrlOffice365.Tag))
                {
                    if (!String.IsNullOrEmpty(textBoxURL.Text))
                        radioButtonUrlCustom.Tag = textBoxURL.Text;
                    textBoxURL.Text = (string)radioButtonUrlOffice365.Tag;
                }
                textBoxURL.ReadOnly = true;
                return;
            }

            if (textBoxURL.Text.Equals((string)radioButtonUrlOffice365.Tag) || String.IsNullOrEmpty(textBoxURL.Text))
            {
                if (String.IsNullOrEmpty((string)radioButtonUrlCustom.Tag))
                {
                    textBoxURL.Text = "https://<server>/EWS/Exchange.asmx";
                    textBoxURL.SelectionStart = 8;
                    textBoxURL.SelectionLength = 8;
                    textBoxURL.Focus();
                }
                else
                {
                    textBoxURL.Text = (string)radioButtonUrlCustom.Tag;
                    if (textBoxURL.Text.StartsWith("https://",StringComparison.OrdinalIgnoreCase))
                    {
                        textBoxURL.SelectionStart = 8;
                        int serverEnd = textBoxURL.Text.IndexOf('/', 8);
                        if (serverEnd > 8)
                        {
                            textBoxURL.SelectionLength = serverEnd - 8;
                            textBoxURL.Focus();
                        }
                    }
                }
            }
            textBoxURL.ReadOnly = false;
        }

        private void textBoxURL_TextChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonUrlOffice365_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUrl();
        }

        private void radioButtonUrlCustom_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUrl();
        }

        private void radioButtonCertificateAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthUI();
        }

        private void buttonChooseCertificate_Click(object sender, EventArgs e)
        {
            Auth.FormChooseAuthCertificate oForm = new Auth.FormChooseAuthCertificate();
            DialogResult result = oForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            _authCertificate = oForm.Certificate;
            textBoxAuthCertificate.Text = _authCertificate.Subject;
        }

        private void buttonViewLogFile_Click(object sender, EventArgs e)
        {
            OpenLogViewer();
        }

        private void buttonCreateNewLogFile_Click(object sender, EventArgs e)
        {

        }

        private void buttonBrowseLogFolder_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(textBoxLogFolder.Text);
            }
            catch { }
        }

        private void buttonAcquireOAuthToken_Click(object sender, EventArgs e)
        {
            if (_oAuthAppRegForm.AcquireNativeAppToken())
                textBoxOAuthToken.Text = _oAuthAppRegForm.AccessToken;
        }

        private void buttonViewOtherLog_Click(object sender, EventArgs e)
        {
            FormLogViewer logViewer = new FormLogViewer();
            if (!logViewer.IsDisposed)
                logViewer.Show();
        }

        private void buttonAppRegistration_Click(object sender, EventArgs e)
        {
            _oAuthAppRegForm.TokenTextBox = textBoxOAuthToken; // So that the app reg form knows where to send any acquired tokens
            _oAuthAppRegForm.ShowDialog(this);
            this.Activate();
        }

        private void buttonUpdateEWSHeader_Click(object sender, EventArgs e)
        {
            UpdateSOAPHeader();
        }

        private void toolStripMenuItemAutoDiscover_Click(object sender, EventArgs e)
        {
            // Open Autodiscover form

            ClassTemplateReader oReader = new ClassTemplateReader();
            _autodiscoverForm = new FormEWSAutodiscover(this, textBoxURL, _logger);

            string sSMTPAddress = String.Empty;
            if (!String.IsNullOrEmpty(textBoxImpersonationSID.Text))
            {
                // If we are using impersonation, we perform autodiscover on the impersonated address
                if ((string)textBoxImpersonationSID.Tag == "PrimarySmtpAddress")
                {
                    sSMTPAddress = textBoxImpersonationSID.Text;
                }
                else
                    sSMTPAddress = "";
            }
            else if (radioButtonSpecificCredentials.Checked)
                if (textBoxUsername.Text.Contains("@"))
                    sSMTPAddress = textBoxUsername.Text;
            _autodiscoverForm.AutodiscoverSMTPAddress = sSMTPAddress;
            _autodiscoverForm.Show(this);
        }

        private void toolStripMenuItemConvertId_Click(object sender, EventArgs e)
        {
            xmlEditorRequest.Text = LoadTemplate("", "", "ConvertId");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Encoding.FormBase64 oForm = new Encoding.FormBase64();
            oForm.Show();
        }

        private void TryXmlRequest(string requestXml)
        {
            if (!String.IsNullOrEmpty(requestXml))
            {
                xmlEditorRequest.Text = requestXml;
                buttonSend_Click(this, null);
            }
        }

        private void EWSTestGetFolder(string DistinguishedFolderName)
        {
            Dictionary<string, string> fieldValues = new Dictionary<string, string>();
            fieldValues.Add("FolderId", $"<t:DistinguishedFolderId Id=\"{DistinguishedFolderName}\" />");
            TryXmlRequest(FormReplaceTemplateFields.RetrieveEWSRequest("GetFolder", fieldValues));
        }

        private void GetFolderInboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EWSTestGetFolder("inbox");
        }

        private void GetFolderCalendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EWSTestGetFolder("calendar");
        }

        private void GetFolderContactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EWSTestGetFolder("contacts");
        }

        private void EWSTestFindItem(string DistinguishedFolderName)
        {
            Dictionary<string, string> fieldValues = new Dictionary<string, string>();
            fieldValues.Add("BaseShape", "Default");
            fieldValues.Add("MaxEntriesReturned", "50");
            fieldValues.Add("Offset", "0");
            fieldValues.Add("Basepoint", "Beginning");
            fieldValues.Add("Folder", $"<t:DistinguishedFolderId Id=\"{DistinguishedFolderName}\" />");
            TryXmlRequest(FormReplaceTemplateFields.RetrieveEWSRequest("FindItem", fieldValues));
        }

        private void FindItemInboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EWSTestFindItem("inbox");
        }

        private void FindItemContactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EWSTestFindItem("contacts");
        }

        private void GetFolderByIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TryXmlRequest(LoadTemplate("", "", "GetFolder"));
        }

        private void GetItemByIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TryXmlRequest(LoadTemplate("", "", "GetItem"));
        }

        private void ConfigurationManagerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConfigurationManager.ClassFormConfig.ShowConfigurationManager(this);
        }
    }
}
