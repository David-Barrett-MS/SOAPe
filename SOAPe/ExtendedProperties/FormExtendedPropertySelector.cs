﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOAPe.ExtendedProperties;
using System.Reflection;

namespace SOAPe
{
    public partial class FormExtendedPropertySelector : Form
    {
        static KnownExtendedProperties knownExtendedProperties = null;

        public FormExtendedPropertySelector()
        {
            InitializeComponent();
            if (knownExtendedProperties == null)
                knownExtendedProperties = new KnownExtendedProperties();
            InitPropertyCombo();
            radioButtonMAPIProperty.Checked = true;
            InitPropertySetIdCombo();
            InitPropertyTypeCombo();
        }

        private void InitPropertyCombo()
        {
            comboBoxKnownMAPIProperties.DataSource = null;
            comboBoxKnownMAPIProperties.DisplayMember = "Value";
            comboBoxKnownMAPIProperties.ValueMember = "Key";
            comboBoxKnownMAPIProperties.DataSource = new BindingSource(knownExtendedProperties.PropertyDictionary, null);
            //comboBoxKnownMAPIProperties.Items.Clear();
            //foreach (ExtendedPropertyDefinition propertyDefinition in knownExtendedProperties.PropertyDictionary.Keys)
            //{                
            //    comboBoxKnownMAPIProperties.Items.Add(knownExtendedProperties.PropertyDictionary[propertyDefinition].CanonicalNames);
            //}
        }

        private void InitPropertySetIdCombo()
        {
            comboBoxPropertySetId.Items.Clear();
            Type propType = typeof(DefaultExtendedPropertySet);
            FieldInfo[] defaultPropSets = propType.GetFields();
            foreach (FieldInfo defaultPropSet in defaultPropSets)
                if (defaultPropSet.IsLiteral)
                    comboBoxPropertySetId.Items.Add(defaultPropSet.GetValue(null));
        }

        private void InitPropertyTypeCombo()
        {
            comboBoxPropertyType.Items.Clear();
            Type mapiPropType = typeof(MapiPropertyType);
            FieldInfo[] propTypes = mapiPropType.GetFields();
            foreach (FieldInfo propType in propTypes)
                if (propType.IsLiteral)
                    comboBoxPropertyType.Items.Add(propType.GetValue(null));
        }

        private void UpdatePropertyType()
        {
            comboBoxKnownMAPIProperties.Enabled = radioButtonMAPIProperty.Checked;
            comboBoxPropertyType.Enabled = radioButtonNamedProperty.Checked;
            comboBoxPropertySetId.Enabled = radioButtonNamedProperty.Checked;
            radioButtonPropertyId.Enabled = radioButtonNamedProperty.Checked;
            radioButtonPropertyName.Enabled = radioButtonNamedProperty.Checked;
            textBoxPropertyId.Enabled = radioButtonNamedProperty.Checked && radioButtonPropertyId.Checked;
            textBoxPropertyName.Enabled = radioButtonNamedProperty.Checked && radioButtonPropertyName.Checked;
        }

        private void radioButtonMAPIProperty_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePropertyType();
        }

        private void radioButtonNamedProperty_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePropertyType();
        }

        private void radioButtonPropertyId_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePropertyType();
        }

        private void radioButtonPropertyName_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePropertyType();
        }

        /// <summary>
        /// Return a description of the currently selected property as an Xml comment
        /// </summary>
        /// <returns>A description of the currently selected property as an Xml comment</returns>
        public string ExtendedPropertyXmlDescription()
        {
            if (radioButtonMAPIProperty.Checked)
            {
                if (comboBoxKnownMAPIProperties.SelectedIndex > -1)
                {
                    string mapiProp = ((KeyValuePair<ExtendedPropertyDefinition, KnownExtendedPropertyInfo>)comboBoxKnownMAPIProperties.SelectedItem).Value.ToString();
                    return $"<!-- {mapiProp} -->";
                }
            }
            return "";
        }

        /// <summary>
        /// Return the Xml for the currently selected property
        /// </summary>
        /// <returns>Xml for the currently selected property</returns>
        public string ExtendedPropertyXml()
        {
            // If a custom MAPI property is selected, we return the Xml for that
            if (radioButtonMAPIProperty.Checked)
            {
                if (comboBoxKnownMAPIProperties.SelectedIndex > -1)
                {
                    string mapiProp = ((KeyValuePair<ExtendedPropertyDefinition, KnownExtendedPropertyInfo>)comboBoxKnownMAPIProperties.SelectedItem).Value.ToString();
                    return knownExtendedProperties.GetDefinitionByCanonicalNames(mapiProp).Xml;
                }
                return "";
            }

            // Custom property.  We just build the Xml from the provided information - no validation is done
            StringBuilder xml = new StringBuilder("<ExtendedFieldURI");
            if (comboBoxPropertySetId.SelectedIndex > -1)
                xml.Append($" DistinguishedPropertySetId=\"{comboBoxPropertySetId.SelectedItem}\"");
            else
                xml.Append($" PropertySetId=\"{comboBoxPropertySetId.Text}\"");

            if (radioButtonPropertyId.Checked)
                xml.Append($" PropertyId=\"{textBoxPropertyId.Text}\"");
            else
                xml.Append($" PropertyName=\"{textBoxPropertyName.Text}\"");

            if (comboBoxPropertyType.SelectedIndex>-1)
                xml.Append($" PropertyType=\"{comboBoxPropertyType.SelectedItem}\"");

            xml.Append(" xmlns=\"http://schemas.microsoft.com/exchange/services/2006/types\"");

            xml.Append("/>");
            return xml.ToString();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Close();
            //textBoxPropertyName.Text = ExtendedPropertyXml();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
