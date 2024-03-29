﻿
namespace SOAPe
{
    partial class FormExtendedPropertySelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExtendedPropertySelector));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxPropertyType = new System.Windows.Forms.ComboBox();
            this.comboBoxKnownMAPIProperties = new System.Windows.Forms.ComboBox();
            this.radioButtonMAPIProperty = new System.Windows.Forms.RadioButton();
            this.radioButtonNamedProperty = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonPropertyName = new System.Windows.Forms.RadioButton();
            this.radioButtonPropertyId = new System.Windows.Forms.RadioButton();
            this.textBoxPropertyName = new System.Windows.Forms.TextBox();
            this.textBoxPropertyId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPropertySetId = new System.Windows.Forms.ComboBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxPropertyType);
            this.groupBox1.Controls.Add(this.comboBoxKnownMAPIProperties);
            this.groupBox1.Controls.Add(this.radioButtonMAPIProperty);
            this.groupBox1.Controls.Add(this.radioButtonNamedProperty);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Property Type";
            // 
            // comboBoxPropertyType
            // 
            this.comboBoxPropertyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPropertyType.FormattingEnabled = true;
            this.comboBoxPropertyType.Location = new System.Drawing.Point(193, 44);
            this.comboBoxPropertyType.Name = "comboBoxPropertyType";
            this.comboBoxPropertyType.Size = new System.Drawing.Size(253, 21);
            this.comboBoxPropertyType.TabIndex = 3;
            // 
            // comboBoxKnownMAPIProperties
            // 
            this.comboBoxKnownMAPIProperties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKnownMAPIProperties.FormattingEnabled = true;
            this.comboBoxKnownMAPIProperties.Location = new System.Drawing.Point(105, 18);
            this.comboBoxKnownMAPIProperties.Name = "comboBoxKnownMAPIProperties";
            this.comboBoxKnownMAPIProperties.Size = new System.Drawing.Size(341, 21);
            this.comboBoxKnownMAPIProperties.TabIndex = 2;
            // 
            // radioButtonMAPIProperty
            // 
            this.radioButtonMAPIProperty.AutoSize = true;
            this.radioButtonMAPIProperty.Location = new System.Drawing.Point(6, 19);
            this.radioButtonMAPIProperty.Name = "radioButtonMAPIProperty";
            this.radioButtonMAPIProperty.Size = new System.Drawing.Size(93, 17);
            this.radioButtonMAPIProperty.TabIndex = 1;
            this.radioButtonMAPIProperty.TabStop = true;
            this.radioButtonMAPIProperty.Text = "MAPI Property";
            this.radioButtonMAPIProperty.UseVisualStyleBackColor = true;
            this.radioButtonMAPIProperty.CheckedChanged += new System.EventHandler(this.radioButtonMAPIProperty_CheckedChanged);
            // 
            // radioButtonNamedProperty
            // 
            this.radioButtonNamedProperty.AutoSize = true;
            this.radioButtonNamedProperty.Location = new System.Drawing.Point(6, 45);
            this.radioButtonNamedProperty.Name = "radioButtonNamedProperty";
            this.radioButtonNamedProperty.Size = new System.Drawing.Size(181, 17);
            this.radioButtonNamedProperty.TabIndex = 0;
            this.radioButtonNamedProperty.TabStop = true;
            this.radioButtonNamedProperty.Text = "Named (custom) property of type:";
            this.radioButtonNamedProperty.UseVisualStyleBackColor = true;
            this.radioButtonNamedProperty.CheckedChanged += new System.EventHandler(this.radioButtonNamedProperty_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonPropertyName);
            this.groupBox2.Controls.Add(this.radioButtonPropertyId);
            this.groupBox2.Controls.Add(this.textBoxPropertyName);
            this.groupBox2.Controls.Add(this.textBoxPropertyId);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBoxPropertySetId);
            this.groupBox2.Location = new System.Drawing.Point(3, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 106);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Property Definition";
            // 
            // radioButtonPropertyName
            // 
            this.radioButtonPropertyName.AutoSize = true;
            this.radioButtonPropertyName.Location = new System.Drawing.Point(9, 73);
            this.radioButtonPropertyName.Name = "radioButtonPropertyName";
            this.radioButtonPropertyName.Size = new System.Drawing.Size(95, 17);
            this.radioButtonPropertyName.TabIndex = 7;
            this.radioButtonPropertyName.TabStop = true;
            this.radioButtonPropertyName.Text = "PropertyName:";
            this.radioButtonPropertyName.UseVisualStyleBackColor = true;
            this.radioButtonPropertyName.CheckedChanged += new System.EventHandler(this.radioButtonPropertyName_CheckedChanged);
            // 
            // radioButtonPropertyId
            // 
            this.radioButtonPropertyId.AutoSize = true;
            this.radioButtonPropertyId.Location = new System.Drawing.Point(9, 47);
            this.radioButtonPropertyId.Name = "radioButtonPropertyId";
            this.radioButtonPropertyId.Size = new System.Drawing.Size(76, 17);
            this.radioButtonPropertyId.TabIndex = 6;
            this.radioButtonPropertyId.TabStop = true;
            this.radioButtonPropertyId.Text = "PropertyId:";
            this.radioButtonPropertyId.UseVisualStyleBackColor = true;
            this.radioButtonPropertyId.CheckedChanged += new System.EventHandler(this.radioButtonPropertyId_CheckedChanged);
            // 
            // textBoxPropertyName
            // 
            this.textBoxPropertyName.Location = new System.Drawing.Point(110, 72);
            this.textBoxPropertyName.Name = "textBoxPropertyName";
            this.textBoxPropertyName.Size = new System.Drawing.Size(336, 20);
            this.textBoxPropertyName.TabIndex = 5;
            // 
            // textBoxPropertyId
            // 
            this.textBoxPropertyId.Location = new System.Drawing.Point(91, 46);
            this.textBoxPropertyId.Name = "textBoxPropertyId";
            this.textBoxPropertyId.Size = new System.Drawing.Size(355, 20);
            this.textBoxPropertyId.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "PropertySetId:";
            // 
            // comboBoxPropertySetId
            // 
            this.comboBoxPropertySetId.FormattingEnabled = true;
            this.comboBoxPropertySetId.Location = new System.Drawing.Point(86, 19);
            this.comboBoxPropertySetId.Name = "comboBoxPropertySetId";
            this.comboBoxPropertySetId.Size = new System.Drawing.Size(360, 21);
            this.comboBoxPropertySetId.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAdd.Location = new System.Drawing.Point(380, 190);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(299, 190);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormExtendedPropertySelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 219);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormExtendedPropertySelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Extended Property";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxKnownMAPIProperties;
        private System.Windows.Forms.RadioButton radioButtonMAPIProperty;
        private System.Windows.Forms.RadioButton radioButtonNamedProperty;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonPropertyName;
        private System.Windows.Forms.RadioButton radioButtonPropertyId;
        private System.Windows.Forms.TextBox textBoxPropertyName;
        private System.Windows.Forms.TextBox textBoxPropertyId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPropertySetId;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxPropertyType;
    }
}