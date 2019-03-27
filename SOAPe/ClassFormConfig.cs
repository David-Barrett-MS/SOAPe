/*
 * By David Barrett, Microsoft Ltd. 2018. Use at your own risk.  No warranties are given.
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;

namespace SOAPe
{
    class ClassFormConfig
    {
        private Form _form = null;
        private string _configFile = String.Empty;
        private bool _encryptData = true;
        private bool _storeButtonInfo = false;
        static Dictionary<string, string> _formsConfig = null;

        public ClassFormConfig(System.Windows.Forms.Form form)
        {
            Initialise(form);
        }

        public ClassFormConfig(Form form, bool Encrypt)
        {
            _encryptData = Encrypt;
            Initialise(form);
        }

        public ClassFormConfig(System.Windows.Forms.Form form, string configFile)
        {
            _configFile = configFile;
            Initialise(form);
        }

        private void _form_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFormValues(_configFile);
        }

        private void Initialise(Form form)
        {
            _form = form;
            // If no config file is specified, then we use one based on current user's SID
            try
            {
                if (String.IsNullOrEmpty(_configFile))
                {
                    _configFile = @".\" + System.Security.Principal.WindowsIdentity.GetCurrent().User.Value + ".dat";
                }
            }
            catch { }
            // If we can't get user's SID, then we just use a generic name
            if (String.IsNullOrEmpty(_configFile))
                _configFile = @".\config.dat";
            if (_formsConfig == null)
                ReadFormDataFromFile(_configFile);
            RestoreFormValues();
            _form.FormClosing += _form_FormClosing;
        }

        private string Decode(string FormData)
        {
            // Decode the data back to our form config

            byte[] encodedFormData = null;
            try
            {
                encodedFormData = System.Convert.FromBase64String(FormData);
                return System.Text.Encoding.UTF8.GetString(encodedFormData);
            }
            catch { }
            return String.Empty;
        }

        private string Encode(string FormData)
        {
            // Encode the data so that it is a single (long) line
            // Simplest way is to Base64 encode it...

            byte[] formDataBytes = System.Text.Encoding.UTF8.GetBytes(FormData);
            return System.Convert.ToBase64String(formDataBytes);
        }

        private void ReadFormDataFromFile(string ConfigFile)
        {
            // Read configuration data from the file to our dictionary object
            _formsConfig = new Dictionary<string, string>();
            if (!File.Exists(ConfigFile)) return;

            String appSettings = String.Empty;
            try
            {
                appSettings = System.Text.Encoding.Unicode.GetString(ProtectedData.Unprotect(File.ReadAllBytes(ConfigFile), null,
                                                             DataProtectionScope.CurrentUser));
            }
            catch { }

            // Config files that support multiple forms start with v2, so we just check for the v identifier (any present implies support)

            if (!appSettings.StartsWith("FormConfigv"))
            {
                // Try reading unencrypted
                try
                {
                    appSettings = System.Text.Encoding.Unicode.GetString(File.ReadAllBytes(ConfigFile));
                }
                catch { }
            }
            if (!appSettings.StartsWith("FormConfigv"))
                return;


            using (StringReader reader = new StringReader(appSettings))
            {
                string sLine = "";
                while (sLine != null)
                {
                    // Each "line" should be a complete configuration blob for a single form
                    sLine = reader.ReadLine();
                    if (!String.IsNullOrEmpty(sLine))
                    {
                        if (!sLine.StartsWith("FormConfig"))
                        {
                            int splitLocation = sLine.IndexOf(':');
                            if (splitLocation > 0)
                            {
                                string formName = sLine.Substring(0, splitLocation);
                                string formData = sLine.Substring(splitLocation + 1);
                                if (!String.IsNullOrEmpty(formName))
                                {
                                    _formsConfig.Add(formName, Decode(formData));
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool StoreButtonInfo
        {
            get { return _storeButtonInfo; }
            set { _storeButtonInfo = value; }
        }

        private void SaveControlProperties(Control control, ref StringBuilder appSettings)
        {
            // Write the control's properties to our config file

            if (control.Tag != null)
            {
                if (control.Tag.Equals("NoConfigSave"))
                    return;  // This control isn't being stored
            }

            if (!_storeButtonInfo && (control.GetType().ToString() == "System.Windows.Forms.Button"))
                return;

            appSettings.AppendLine(control.Name + ":Text:" + control.Text);

            PropertyInfo prop = control.GetType().GetProperty("SelectedIndex", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
                appSettings.AppendLine(control.Name + ":SelectedIndex:" + prop.GetValue(control));

            prop = control.GetType().GetProperty("Checked", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
                appSettings.AppendLine(control.Name + ":Checked:" + prop.GetValue(control));
        }

        private void RecurseControls(Control ParentControl, ref StringBuilder appSettings)
        {
            // Recursive routine to write control properties to our config file

            SaveControlProperties(ParentControl, ref appSettings);
            if (!ParentControl.HasChildren)
                return;

            foreach (Control control in ParentControl.Controls)
            {
                RecurseControls(control, ref appSettings);
            }
        }

        private void SaveFormValues(string Filename)
        {
            // Read and save all our control's values

            StringBuilder appSettings = new StringBuilder("FormConfig:");
            appSettings.AppendLine();

            foreach (Control control in _form.Controls)
                RecurseControls(control, ref appSettings);

            if (_formsConfig.ContainsKey(_form.Name))
                _formsConfig.Remove(_form.Name);

            _formsConfig.Add(_form.Name, Encode(appSettings.ToString()));

            StringBuilder allAppSettings = new StringBuilder("FormConfigv2");
            allAppSettings.AppendLine();

            foreach (string formName in _formsConfig.Keys)
            {
                allAppSettings.Append(formName);
                allAppSettings.Append(":");
                allAppSettings.AppendLine(Encode(_formsConfig[formName]));
            }

            if (_encryptData)
            {
                File.WriteAllBytes(Filename, ProtectedData.Protect(System.Text.Encoding.Unicode.GetBytes(allAppSettings.ToString()), null, DataProtectionScope.CurrentUser));
            }
            else
                File.WriteAllBytes(Filename, System.Text.Encoding.Unicode.GetBytes(allAppSettings.ToString()));
        }

        private void RestoreFormValues()
        {
            // Read our saved control values from the file, and restore

            String appSettings = String.Empty;
            if (_formsConfig.ContainsKey(_form.Name))
                appSettings = Decode(_formsConfig[_form.Name]);
            if (String.IsNullOrEmpty(appSettings)) return;

            if (!appSettings.StartsWith("FormConfig:"))
                return;


            using (StringReader reader = new StringReader(appSettings))
            {
                string sLine = "";
                while (sLine != null)
                {
                    sLine = reader.ReadLine();

                    if (!String.IsNullOrEmpty(sLine))
                    {
                        string[] controlSetting = sLine.Split(':');
                        if (controlSetting.Length > 3)
                        {
                            for (int i = 3; i < controlSetting.Length; i++)
                            {
                                controlSetting[2] = controlSetting[2] + ":" + controlSetting[i];
                            }
                        }

                        Control control = null;
                        try
                        {
                            control = _form.Controls.Find(controlSetting[0].Trim(), true)[0];
                        }
                        catch { }
                        if (control != null)
                        {
                            bool bRestore = true;
                            if (control.Tag != null)
                            {
                                bRestore = !control.Tag.Equals("NoConfigSave");
                            }
                            if (bRestore)
                            {
                                PropertyInfo prop = control.GetType().GetProperty(controlSetting[1].Trim(), BindingFlags.Public | BindingFlags.Instance);
                                if (prop != null && prop.CanWrite)
                                {
                                    try
                                    {
                                        switch (prop.PropertyType.Name.ToString())
                                        {
                                            case "Int32":
                                                prop.SetValue(control, Convert.ToInt32(controlSetting[2]));
                                                break;

                                            case "Boolean":
                                                prop.SetValue(control, Convert.ToBoolean(controlSetting[2]));
                                                break;

                                            default:
                                                prop.SetValue(control, controlSetting[2]);
                                                break;
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
