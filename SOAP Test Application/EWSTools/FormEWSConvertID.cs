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
using System.Net.Security;
using System.Net;
using System.IO;
using System.Reflection;



namespace SOAPe.EWSTools
{
    public partial class FormEWSConvertID : Form
    {
        private string _EWSURL = "";
        private ICredentials _credentials = null;
        private string _ConvertIDTemplate = "";
        

        public FormEWSConvertID(string EWSURL, ICredentials Credentials, IWin32Window Parent=null)
        {
            if (Parent != null)
                this.Owner = (System.Windows.Forms.Form)Parent;
            InitializeComponent();
            
            _EWSURL = EWSURL;
            _credentials = Credentials;
            PopulateFormats(comboBoxSourceFormat);
            comboBoxSourceFormat.SelectedIndex = 1;
            PopulateFormats(comboBoxTargetFormat);
            comboBoxTargetFormat.SelectedIndex = 2;
            ReadTemplate();
        }

        private void ReadTemplate()
        {
            // Reads the XML template
            Assembly oAssembly = Assembly.GetExecutingAssembly();
            StreamReader oReader = new StreamReader(oAssembly.GetManifestResourceStream("SOAPe.EWSTools.EWSConvertIDTemplate.xml"));
            _ConvertIDTemplate = oReader.ReadToEnd();
            oReader.Close();
        }

        private void PopulateFormats(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add("EntryId");
            comboBox.Items.Add("EwsId");
            comboBox.Items.Add("EwsLegacyId");
            comboBox.Items.Add("HexEntryId");
            comboBox.Items.Add("OwaId");
            comboBox.Items.Add("StoreId");
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            string sSourceID = textBoxSourceID.Text;
            if (String.IsNullOrEmpty(sSourceID)) return;

            string sRequest = _ConvertIDTemplate;

            // Replace placeholders in template
            sRequest = sRequest.Replace("%DESTINATIONFORMAT%", comboBoxTargetFormat.Text);
            sRequest = sRequest.Replace("%SOURCEFORMAT%", comboBoxSourceFormat.Text);
            sRequest = sRequest.Replace("%SOURCEID%", textBoxSourceID.Text);
            sRequest = sRequest.Replace("%MAILBOX%", textBoxMailbox.Text);  // We don't care about the SMTP address, and it is not important

            // Now send request
            ClassSOAP oSOAP = new ClassSOAP(_EWSURL, _credentials,null);
            string sError = "";
            string sResponse = oSOAP.SendRequest(sRequest, out sError);

            // Now extract the converted ID
            // This is a very (very very) bad method of doing this, but it is a quick implementation
            if (sResponse.Contains("<m:ConvertIdResponseMessage ResponseClass=\"Success\"><m:ResponseCode>NoError</m:ResponseCode>"))
            {
                // We have a valid response
                int iIDStart = sResponse.IndexOf("Id=\"");
                int iIDEnd = sResponse.IndexOf(" ", iIDStart);
                string sID = sResponse.Substring(iIDStart, iIDEnd-iIDStart);
                sID = sID.Substring(4);
                sID = sID.Substring(0, sID.Length - 1);
                textBoxConvertedID.Text = sID;
                textBoxConvertedID.Focus();
                textBoxConvertedID.SelectAll();
            }
            else
            {
                textBoxConvertedID.Text = "Error occurred: " + sResponse.Trim() ;
            }
        }
    }
}
