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
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using SOAPe.EWSTools;
using System.Threading;

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
        private ClassFormConfig _formConfig = null;
        private Auth.FormAzureApplicationRegistration _oAuthAppRegForm=null;

        public FormMain(bool DebugLogging)
        {
            InitializeComponent();
            _formConfig = new ClassFormConfig(this);

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
            _logger.DebugLog("Initialisation complete");
            /*
            if (System.Diagnostics.Debugger.IsAttached)
            {
                FormUserControlTest oForm = new FormUserControlTest();
                oForm.Show(this);
            }
             */
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

        private void LogCredentials()
        {
            StringBuilder sCredentialInfo = new StringBuilder();
            if (radioButtonDefaultCredentials.Checked)
            {
                sCredentialInfo.AppendLine("Using default credentials");
                sCredentialInfo.Append("Username: ");
                sCredentialInfo.AppendLine(Environment.UserName);
                sCredentialInfo.Append("Domain: ");
                sCredentialInfo.AppendLine(Environment.UserDomainName);
            }
            else if (radioButtonCertificateAuthentication.Checked)
            {
                sCredentialInfo.AppendLine("Using certificate");
                if (_authCertificate != null)
                {
                    sCredentialInfo.Append("Subject: ");
                    sCredentialInfo.AppendLine(_authCertificate.Subject);
                }
                else
                    sCredentialInfo.AppendLine("NO CERTIFICATE SPECIFIED");
            }
            else
            {
                sCredentialInfo.AppendLine("Using specific credentials");
                if (checkBoxForceBasicAuth.Checked)
                    sCredentialInfo.AppendLine("Forcing BASIC AUTH");
                sCredentialInfo.Append("Username: ");
                sCredentialInfo.AppendLine(textBoxUsername.Text);
                sCredentialInfo.Append("Domain: ");
                sCredentialInfo.AppendLine(textBoxDomain.Text);
            }
            _logger.Log(sCredentialInfo.ToString(), "Request Credentials");
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
            bool bBasicAuthExistingSetting = (checkBoxForceBasicAuth.Checked && !radioButtonNoAuth.Checked);
            ClassSOAP oSOAP = null;
            oSOAP = null;
            LogCredentials();
            if (radioButtonDefaultCredentials.Checked || (radioButtonSpecificCredentials.Checked && !checkBoxForceBasicAuth.Checked))
            {
                oSOAP = new ClassSOAP(textBoxURL.Text, CurrentCredentials, _logger);
            }
            else if (radioButtonNoAuth.Checked)
            {
                oSOAP = new ClassSOAP(textBoxURL.Text, _logger);
            }
            else if (radioButtonCertificateAuthentication.Checked)
            {
                oSOAP = new ClassSOAP(textBoxURL.Text, _authCertificate, _logger);
            }
            else
                oSOAP = new ClassSOAP(textBoxURL.Text, textBoxUsername.Text, textBoxPassword.Text, _logger);

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
            textBoxDomain.Enabled = bUserCredsVisible && !checkBoxForceBasicAuth.Checked;
            checkBoxForceBasicAuth.Visible = bUserCredsVisible;
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

        private void convertIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEWSConvertID oForm = new FormEWSConvertID(textBoxURL.Text, CurrentCredentials, _logger, this);
            oForm.Show();
        }

        private string LoadTemplate(string ItemId = "", string FolderId = "")
        {
            // Reads the XML template
            string sTemplateContent = "";
            FormReplaceTemplateFields oForm = new FormReplaceTemplateFields(ItemId, FolderId);
            sTemplateContent = oForm.ReadTemplate(this);
            oForm.Dispose();

            if (String.IsNullOrEmpty(sTemplateContent))
                return xmlEditorRequest.Text;

            return sTemplateContent;
        }


        private void UpdateSOAPHeader()
        {
            // Add or replace the existing SOAP header with the new one
            string sSOAPRequest = xmlEditorRequest.Text;

            if (sSOAPRequest.ToLower().Contains("<soap:header"))
            {
                // A header already exists, so we need to remove it
                int iHeaderStart = sSOAPRequest.ToLower().IndexOf("<soap:header");
                int iHeaderEnd = sSOAPRequest.ToLower().IndexOf("</soap:header>") + 14;
                while (sSOAPRequest[iHeaderEnd] == '\n' || sSOAPRequest[iHeaderEnd] == '\r')
                    iHeaderEnd++;
                if (iHeaderStart > 0 && iHeaderEnd > 0)
                    sSOAPRequest = sSOAPRequest.Substring(0, iHeaderStart) + sSOAPRequest.Substring(iHeaderEnd);
            }

            string sHeader = EWSHeader();

            if (!String.IsNullOrEmpty(sHeader))
            {
                // Add the SOAP header to this request
                // Need to inject it just before the <soap:Body> tag
                int iBodyTag = sSOAPRequest.ToLower().IndexOf("<soap:body");
                if (iBodyTag > 0)
                {
                    sSOAPRequest = sSOAPRequest.Substring(0, iBodyTag) + sHeader + sSOAPRequest.Substring(iBodyTag);
                    xmlEditorRequest.Text = sSOAPRequest;
                }
                else
                    System.Windows.Forms.MessageBox.Show("Unable to apply impersonation header - please enter valid request", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string EWSHeader()
        {
            // Prepare the EWS header based on version and impersonation settings
            string sHeader = "";

            if (comboBoxRequestServerVersion.Text != "Not set")
            {
                // Set the Exchange version
                sHeader+= "<RequestServerVersion Version=\"" + comboBoxRequestServerVersion.Text + "\" xmlns=\"http://schemas.microsoft.com/exchange/services/2006/types\" />\r\n";
            }

            string sImpersonation = GetImpersonationHeader();
            if (!String.IsNullOrEmpty(sImpersonation))
                sHeader += sImpersonation;

            if (!String.IsNullOrEmpty(sHeader))
                sHeader = "<soap:Header>\r\n" + sHeader + "</soap:Header>\r\n";

            return sHeader;
        }

        private void autodiscoverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show warning...
            ClassTemplateReader oReader = new ClassTemplateReader();

            try
            {
                _autodiscoverForm.Credentials = CurrentCredentials;
            }
            catch
            {
                _autodiscoverForm = new FormEWSAutodiscover(CurrentCredentials, textBoxURL, _logger);
            }
            string sSMTPAddress = _userSMTPAddress;
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

        private void base64ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Encoding.FormBase64 oForm = new Encoding.FormBase64();
            oForm.Show();
        }


        private void buttonLoadTemplate_Click(object sender, EventArgs e)
        {
            xmlEditorRequest.Text = LoadTemplate();
        }

        private void hTTPListenerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.HTTPListener.Show();
        }


        private void textBoxImpersonationSID_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxImpersonationSID.Text))
                checkBoxUpdateEWSHeader.Checked = true;
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

        private void buttonHTTPHeaderAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxHTTPHeaderName.Text))
                return;

            try
            {
                ListViewItem[] items = listViewHTTPHeaders.Items.Find(textBoxHTTPHeaderName.Text, false);
                if (items.Length > 0)
                    items[0].Remove();
            }
            catch { }

            try
            {
                ListViewItem item = new ListViewItem(textBoxHTTPHeaderName.Text);
                item.Name = textBoxHTTPHeaderName.Text;
                item.SubItems.Add(textBoxHTTPHeaderValue.Text);
                listViewHTTPHeaders.Items.Add(item);
                textBoxHTTPHeaderName.Text = "";
                textBoxHTTPHeaderValue.Text = "";
            }
            catch { }
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

        private string GetImpersonationHeader()
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
                textBoxURL.Text = (string)radioButtonUrlCustom.Tag;
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
            _oAuthAppRegForm.ShowDialog();
        }
    }
}
