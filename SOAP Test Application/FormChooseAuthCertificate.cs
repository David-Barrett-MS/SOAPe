using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Cryptography.X509Certificates;

namespace SOAPe
{
    public partial class FormChooseAuthCertificate : Form
    {
        private X509Certificate2 _certificate = null;
        public FormChooseAuthCertificate()
        {
            InitializeComponent();

            PopulateCertificates();
            UpdateUI();
        }

        private void PopulateCertificates()
        {
            // Populate combobox with any valid certificates found in their store

            X509Store x509Store = null; 
            comboBoxStoreCertificates.Items.Clear();
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
                    comboBoxStoreCertificates.Items.Add(x509Cert);
                }
                catch { }
            }

            x509Store.Close();
        }

        public X509Certificate2 Certificate
        {
            get { return _certificate; }
        }

        private void comboBoxStoreCertificates_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _certificate = (X509Certificate2)comboBoxStoreCertificates.SelectedItem;
            }
            catch { }
            ShowCertificateInfo();
        }

        private void ShowCertificateInfo()
        {
            listBoxCertificateInfo.Items.Clear();
            if (_certificate == null)
                return;

            try
            {
                byte[] rawData = Certificate.RawData;
                listBoxCertificateInfo.Items.Add(String.Format("Content type: {0}", X509Certificate2.GetCertContentType(rawData)));
                listBoxCertificateInfo.Items.Add(String.Format("Friendly name: {0}", _certificate.FriendlyName));
                listBoxCertificateInfo.Items.Add(String.Format("Subject: {0}", _certificate.Subject));
                listBoxCertificateInfo.Items.Add(String.Format("Verified: {0}", _certificate.Verify()));
                listBoxCertificateInfo.Items.Add(String.Format("Simple name: {0}", _certificate.GetNameInfo(X509NameType.SimpleName, true)));
                listBoxCertificateInfo.Items.Add(String.Format("Signature algorithm: {0}", _certificate.SignatureAlgorithm.FriendlyName));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Unable to read certificate: {0}{1}", Environment.NewLine, ex.Message), "Certificate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            buttonOK.Enabled = (_certificate != null);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (_certificate == null)
            {
                System.Windows.Forms.MessageBox.Show("Please select a certificate", "No certificate chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonBrowseForCertificate_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDialog = new OpenFileDialog();
            oDialog.Filter = "Certificates (*.pfx)|*.pfx|All Files|*.*";
            oDialog.DefaultExt = "pfx";
            oDialog.Title = "Select certificate to use (must have access to private key)";
            oDialog.CheckFileExists = true;
            if (oDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                _certificate = (X509Certificate2)X509Certificate2.CreateFromCertFile(oDialog.FileName);
                textBoxCertificateFile.Text = oDialog.FileName;
                ShowCertificateInfo();
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147024810)
                {
                    // File is password protected
                    try
                    {
                        byte[] certData = System.IO.File.ReadAllBytes(oDialog.FileName);
                        _certificate = new X509Certificate2(certData, textBoxPassword.Text);
                        textBoxCertificateFile.Text = oDialog.FileName;
                        ShowCertificateInfo();
                        textBoxPassword.Focus();
                    }
                    catch (Exception ex2)
                    {
                        System.Windows.Forms.MessageBox.Show(String.Format("Unable to load certificate: {0}{1}", Environment.NewLine, ex2.Message), "Load failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _certificate = null;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(String.Format("Unable to load certificate: {0}{1}", Environment.NewLine, ex.Message), "Load failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _certificate = null;
                }
            }
        }

        private void UpdateUI()
        {
            bool bSelectFromStoreEnabled = radioButtonSelectFromStore.Checked;

            textBoxCertificateFile.Enabled = !bSelectFromStoreEnabled;
            buttonBrowseForCertificate.Enabled = !bSelectFromStoreEnabled;
            textBoxPassword.Enabled = !bSelectFromStoreEnabled;
            comboBoxStoreCertificates.Enabled = bSelectFromStoreEnabled;

            buttonOK.Enabled = (_certificate != null);
        }

        private void radioButtonSelectFromStore_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void radioButtonLoadFromFile_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

    }
}
