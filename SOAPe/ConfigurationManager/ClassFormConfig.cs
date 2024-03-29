﻿/*
 * By David Barrett, Microsoft Ltd. 2018. Use at your own risk.  No warranties are given.
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;

namespace SOAPe.ConfigurationManager
{
    class ClassFormConfig
    {
        private Form _form = null;
        private string _configFile = String.Empty;
        private bool _encryptData = true;
        private bool _doNotStoreConfig = false;  // Used to control whether we want to store anything at all
        private static Dictionary<string, string> _formsConfig = null;
        private static Dictionary<string, Dictionary<string,string>> _configurationSets = null;
        private static string _selectedConfiguration = "Default";
        private List<string> _controlTypesExcludedFromRecursion = new List<string>();
        private static ConfigurationManager.FormConfigurationManager _formConfigurationManager = null;
        public static event EventHandler ConfigChanged = delegate { };
        public static event EventHandler UpdateConfig = delegate { };
        public static event EventHandler UpdateAndSaveConfig = delegate { };

        public bool StoreButtonInfo { get; set; } = false;
        public bool StoreLabelInfo { get; set; } = false;
        public bool StoreCheckBoxState { get; set; } = true;
        public bool StoreRadioButtonState { get; set; } = true;

        public ClassFormConfig(System.Windows.Forms.Form form, bool DoNotApply = false)
        {
            Initialise(form, DoNotApply);
        }

        public ClassFormConfig(Form form, bool Encrypt, bool DoNotApply = false)
        {
            _encryptData = Encrypt;
            Initialise(form, DoNotApply);
        }

        public ClassFormConfig(System.Windows.Forms.Form form, string configFile)
        {
            _configFile = configFile;
            Initialise(form, false);
        }

        public static string ActiveConfiguration
        {
            get { return _selectedConfiguration; }
        }

        public static bool ApplyConfiguration(string ConfigName="")
        {
            if (!String.IsNullOrEmpty(ConfigName))
            {
                if (_configurationSets.ContainsKey(ConfigName))
                {
                    _selectedConfiguration = ConfigName;
                }
                else
                    return false;
            }
            _formsConfig = _configurationSets[_selectedConfiguration];
            ConfigChanged(null, null);
            return true;
        }

        public static bool SaveNewConfiguration(string ConfigName)
        {
            // Save our current configuration as a new configuration set with the given name
            if (String.IsNullOrEmpty(ConfigName))
                return false;

            if (_configurationSets.ContainsKey(ConfigName))
                return false;

            _selectedConfiguration = ConfigName;
            _formsConfig = new Dictionary<string, string>();
            UpdateConfig(null, null);
            return true;
        }

        public static bool DeleteConfiguration(string ConfigName)
        {
            if (_configurationSets.Count < 2)
                return false; // Can't delete last configuration set

            if (!_configurationSets.ContainsKey(ConfigName))
                return false;

            _configurationSets.Remove(ConfigName);
            if (_selectedConfiguration.Equals(ConfigName))
            {
                _selectedConfiguration = _configurationSets.Keys.ToList<string>()[0];
                _formsConfig = _configurationSets[_selectedConfiguration];
                ConfigChanged(null, null);
            }
            return true;
        }

        public static bool RenameConfiguration(string currentName, string newName)
        {
            if (!_configurationSets.ContainsKey(currentName))
                return false;

            if (_configurationSets.ContainsKey(newName))
                return false;

            _configurationSets.Add(newName, _configurationSets[currentName]);
            _configurationSets.Remove(currentName);
            if (_selectedConfiguration.Equals(currentName))
                _selectedConfiguration = newName;
            return true;
        }

        public static List<string> ConfigurationSetNames()
        {
            return _configurationSets.Keys.ToList<string>();
        }

        public void AddControlTypeRecurseExclusion(string ExcludedType)
        {
            // If controls contain other controls, this can cause odd issues with the recursion process,
            // particularly with multiple instances of user controls that include another control.
            // Any types added here won't be recursed
            _controlTypesExcludedFromRecursion.Add(ExcludedType);
        }

        public List<Control> ExcludedControls { get; } = new List<Control>();

        public bool SaveEnabled
        {
            get { return !_doNotStoreConfig; }
            set { _doNotStoreConfig = !value; }
        }

        public bool SaveConfiguration()
        {
            if (_doNotStoreConfig)
                return false;
            if (!String.IsNullOrEmpty(_configFile))
            {
                SaveConfiguration(_configFile);
                return true;
            }
            return false;
        }

        public static void SaveActiveConfiguration()
        {
            UpdateAndSaveConfig(null, null);
        }

        private void _form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_doNotStoreConfig)
                SaveConfiguration(_configFile);
            ClassFormConfig.ConfigChanged -= ClassFormConfig_ConfigChanged;
            ClassFormConfig.UpdateConfig -= ClassFormConfig_UpdateConfig;
            ClassFormConfig.UpdateAndSaveConfig -= ClassFormConfig_UpdateAndSaveConfig;
        }

        public bool ConfigSavedToDisk
        {
            get { return File.Exists(_configFile); }
        }

        private void Initialise(Form form, bool DoNotApply)
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
            if (!DoNotApply)
                RestoreFormValues();
            _form.FormClosing += _form_FormClosing;
            ClassFormConfig.ConfigChanged += ClassFormConfig_ConfigChanged;
            ClassFormConfig.UpdateConfig += ClassFormConfig_UpdateConfig;
            ClassFormConfig.UpdateAndSaveConfig += ClassFormConfig_UpdateAndSaveConfig;
        }

        private void ClassFormConfig_UpdateAndSaveConfig(object sender, EventArgs e)
        {
            UpdateFormValues();
            if (!_doNotStoreConfig)
                SaveConfiguration(_configFile);
        }

        private void ClassFormConfig_UpdateConfig(object sender, EventArgs e)
        {
            // This event occurs when we need to save the config
            UpdateFormValues();
        }

        private void ClassFormConfig_ConfigChanged(object sender, EventArgs e)
        {
            // We get this event when the configuration has changed and we need to reapply it to the form
            RestoreFormValues();
        }

        private static string Decode(string FormData)
        {
            // Decode the data so that we understand it...

            byte[] encodedFormData = null;
            try
            {
                encodedFormData = System.Convert.FromBase64String(FormData);
                return System.Text.Encoding.UTF8.GetString(encodedFormData);
            }
            catch { }
            return String.Empty;
        }

        private static string Encode(string FormData)
        {
            // Encode the data so that it is a single (long) line
            // Simplest way is to Base64 encode it...

            byte[] formDataBytes = System.Text.Encoding.UTF8.GetBytes(FormData);
            return System.Convert.ToBase64String(formDataBytes);
        }

        private static string Encode(Dictionary<String, String> configSetData)
        {
            StringBuilder encodedData = new StringBuilder();
            foreach (string formName in configSetData.Keys)
            {
                encodedData.Append(formName);
                encodedData.Append(":");
                encodedData.AppendLine(Encode(configSetData[formName]));
            }
            return Encode(encodedData.ToString());
        }

        private static void ReadFormDataFromFile(string ConfigFile)
        {
            // Read configuration data from the file
            _configurationSets = new Dictionary<string, Dictionary<string, string>>();
            _formsConfig = new Dictionary<string, string>();
            if (!File.Exists(ConfigFile))
            {
                _configurationSets.Add("Default", _formsConfig);
                _selectedConfiguration = "Default";
                return;
            }

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
            {
                _configurationSets.Add("Default", _formsConfig);
                _selectedConfiguration = "Default";
                return;
            }

            if (appSettings.StartsWith("FormConfigv3"))
            {
                // v3 adds multiple configuration support
                ReadAllConfigurations(appSettings);
                return;
            }

            // Assuming version 2 (multiple forms but single configuration support)
            // We'll be saving in v3 so we just make this configuraiton the default set
            _formsConfig = ReadFormsConfiguration(appSettings);
            _configurationSets.Add("Default", _formsConfig);
        }

        private static void ReadAllConfigurations(string MultipleConfigurationData)
        {
            // v3 format:
            // Configuration name:ConfigurationData(Base64 encoded)
            _configurationSets = new Dictionary<string, Dictionary<string, string>>();
            using (StringReader reader = new StringReader(MultipleConfigurationData))
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
                                string configName = sLine.Substring(0, splitLocation);
                                string configData = sLine.Substring(splitLocation + 1);
                                if (!String.IsNullOrEmpty(configName))
                                {
                                    _configurationSets.Add(configName, ReadFormsConfiguration(Decode(configData)));
                                }
                            }
                        }
                    }
                }
            }
            if (_configurationSets.ContainsKey(_selectedConfiguration))
            {
                _formsConfig = _configurationSets[_selectedConfiguration];
                return;
            }

            if (_configurationSets.Count==0)
            {
                _selectedConfiguration = "Default";
                _formsConfig = new Dictionary<string, string>(); 
                _configurationSets.Add(_selectedConfiguration, _formsConfig);
                return;
            }
            _selectedConfiguration = _configurationSets.Keys.ToList<string>()[0];
            _formsConfig = _configurationSets[_selectedConfiguration];
        }

        private static Dictionary<string, string> ReadFormsConfiguration(string ConfigurationData)
        {
            Dictionary<string, string> formsConfigurationData = new Dictionary<string, string>();
            using (StringReader reader = new StringReader(ConfigurationData))
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
                                    formsConfigurationData.Add(formName, Decode(formData));
                                }
                            }
                        }
                    }
                }
            }
            return formsConfigurationData;
        }

        public static void ShowConfigurationManager(Form Owner = null)
        {
            if (_formConfigurationManager == null)
                _formConfigurationManager = new ConfigurationManager.FormConfigurationManager();
            if (_formConfigurationManager.Visible)
                return;
            try
            {
                _formConfigurationManager.Show(Owner);
            }
            catch (ObjectDisposedException)
            {
                _formConfigurationManager = new ConfigurationManager.FormConfigurationManager();
                _formConfigurationManager.Show(Owner);
            }
            _formConfigurationManager.SideLoadToForm(Owner);
        }


        private void SaveControlProperties(Control control, ref StringBuilder appSettings)
        {
            // Write the control's properties to our config file

            if ((control is Label) && !StoreLabelInfo) return;
            if ((control is Button) && !StoreButtonInfo) return;
            if ((control is RadioButton) && !StoreRadioButtonState) return;
            if ((control is CheckBox) && !StoreCheckBoxState) return;

            if (ExcludedControls.Contains(control)) return;
            if (control.Tag != null)
            {
                if (control.Tag.Equals("NoConfigSave"))
                    return;  // This control isn't being stored
            }
            else
            {
                if ( (control is RadioButton) || (control is CheckBox) )
                    control.Tag = "NoTextSave"; // We don't want to store the text for radio buttons or checkboxes
            }

            if (control.Tag != null)
            {
                if (!control.Tag.Equals("NoTextSave"))
                    appSettings.AppendLine(control.Name + ":Text:" + Encode(control.Text));
            }
            else
                appSettings.AppendLine(control.Name + ":Text:" + Encode(control.Text));

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

            if ( _controlTypesExcludedFromRecursion.Contains(ParentControl.GetType().ToString()) )// == "SOAPe.XmlEditor")
                return;  // Don't recurse into this control

            foreach (Control control in ParentControl.Controls)
            {
                RecurseControls(control, ref appSettings);
            }
        }

        private void SaveConfiguration(string Filename)
        {
            UpdateFormValues();
            StringBuilder allAppConfig = new StringBuilder("FormConfigv3");
            allAppConfig.AppendLine();

            foreach (String configSetName in _configurationSets.Keys)
            {
                allAppConfig.Append(configSetName);
                allAppConfig.Append(":");
                allAppConfig.AppendLine(Encode(_configurationSets[configSetName]));
            }

            if (_encryptData)
            {
                File.WriteAllBytes(Filename, ProtectedData.Protect(System.Text.Encoding.Unicode.GetBytes(allAppConfig.ToString()), null, DataProtectionScope.CurrentUser));
            }
            else
                File.WriteAllBytes(Filename, System.Text.Encoding.Unicode.GetBytes(allAppConfig.ToString()));
        }

        private void UpdateFormValues()
        {
            // Read and save all our control's values

            StringBuilder appSettings = new StringBuilder("FormConfig:");
            appSettings.AppendLine();

            foreach (Control control in _form.Controls)
                RecurseControls(control, ref appSettings);

            if (_formsConfig.ContainsKey(_form.Name))
                _formsConfig.Remove(_form.Name);

            _formsConfig.Add(_form.Name, Encode(appSettings.ToString()));
            _configurationSets[_selectedConfiguration] = _formsConfig;

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
                            if (!String.IsNullOrEmpty(controlSetting[0]))
                            {
                                Control[] matchingControls = _form.Controls.Find(controlSetting[0].Trim(), true);
                                if (matchingControls.Length > 1)
                                {
                                    foreach (Control matchingControl in matchingControls)
                                    {
                                        if (matchingControl.Name == controlSetting[0].Trim())
                                        {
                                            control = matchingControl;
                                            break;
                                        }
                                    }
                                }
                                else if (matchingControls.Length == 1)
                                    control = matchingControls[0];
                            }
                        }
                        catch { }
                        if (control != null)
                        {
                            bool bRestore = true;
                            if (ExcludedControls.Contains(control))
                            {
                                bRestore = false;
                            }
                            else
                            {
                                if (control.Tag != null)
                                {
                                    bRestore = !control.Tag.Equals("NoConfigSave");
                                }
                            }
                            if (bRestore)
                            {
                                PropertyInfo prop = control.GetType().GetProperty(controlSetting[1].Trim(), BindingFlags.Public | BindingFlags.Instance);
                                if (controlSetting[1].Trim().Equals("Text"))
                                {
                                    controlSetting[2] = Decode(controlSetting[2]);
                                }
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
