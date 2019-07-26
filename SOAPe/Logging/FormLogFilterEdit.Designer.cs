namespace SOAPe
{
    partial class FormLogFilterEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogFilterEdit));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.radioButtonMatchAny = new System.Windows.Forms.RadioButton();
            this.radioButtonMatchAll = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxFilters = new System.Windows.Forms.ListBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxMatchValue = new System.Windows.Forms.TextBox();
            this.comboBoxWHEREClause = new System.Windows.Forms.ComboBox();
            this.comboBoxField = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.radioButtonMatchAny);
            this.groupBox1.Controls.Add(this.radioButtonMatchAll);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listBoxFilters);
            this.groupBox1.Location = new System.Drawing.Point(12, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 162);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Active Filters";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(9, 130);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // radioButtonMatchAny
            // 
            this.radioButtonMatchAny.AutoSize = true;
            this.radioButtonMatchAny.Checked = true;
            this.radioButtonMatchAny.Location = new System.Drawing.Point(304, 133);
            this.radioButtonMatchAny.Name = "radioButtonMatchAny";
            this.radioButtonMatchAny.Size = new System.Drawing.Size(43, 17);
            this.radioButtonMatchAny.TabIndex = 11;
            this.radioButtonMatchAny.TabStop = true;
            this.radioButtonMatchAny.Text = "Any";
            this.radioButtonMatchAny.UseVisualStyleBackColor = true;
            // 
            // radioButtonMatchAll
            // 
            this.radioButtonMatchAll.AutoSize = true;
            this.radioButtonMatchAll.Location = new System.Drawing.Point(262, 133);
            this.radioButtonMatchAll.Name = "radioButtonMatchAll";
            this.radioButtonMatchAll.Size = new System.Drawing.Size(36, 17);
            this.radioButtonMatchAll.TabIndex = 10;
            this.radioButtonMatchAll.Text = "All";
            this.radioButtonMatchAll.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Match:";
            // 
            // listBoxFilters
            // 
            this.listBoxFilters.FormattingEnabled = true;
            this.listBoxFilters.Location = new System.Drawing.Point(9, 19);
            this.listBoxFilters.Name = "listBoxFilters";
            this.listBoxFilters.Size = new System.Drawing.Size(338, 108);
            this.listBoxFilters.TabIndex = 0;
            this.listBoxFilters.SelectedIndexChanged += new System.EventHandler(this.listBoxFilters_SelectedIndexChanged);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(290, 264);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 1;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(209, 264);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonRemove);
            this.groupBox2.Controls.Add(this.buttonAdd);
            this.groupBox2.Controls.Add(this.textBoxMatchValue);
            this.groupBox2.Controls.Add(this.comboBoxWHEREClause);
            this.groupBox2.Controls.Add(this.comboBoxField);
            this.groupBox2.Location = new System.Drawing.Point(12, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(353, 79);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter";
            // 
            // buttonRemove
            // 
            this.buttonRemove.Enabled = false;
            this.buttonRemove.Location = new System.Drawing.Point(292, 46);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(55, 23);
            this.buttonRemove.TabIndex = 5;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(292, 17);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(55, 23);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // textBoxMatchValue
            // 
            this.textBoxMatchValue.Location = new System.Drawing.Point(9, 48);
            this.textBoxMatchValue.Name = "textBoxMatchValue";
            this.textBoxMatchValue.Size = new System.Drawing.Size(277, 20);
            this.textBoxMatchValue.TabIndex = 3;
            // 
            // comboBoxWHEREClause
            // 
            this.comboBoxWHEREClause.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWHEREClause.FormattingEnabled = true;
            this.comboBoxWHEREClause.Items.AddRange(new object[] {
            "Equals[=]",
            "Doesn\'t equal[!=]",
            "Is greater than[>]",
            "Is less than[<]",
            "Is greater than or equal to[>=]",
            "Is less than or equal to[<=]",
            "Like[LIKE]",
            "Not like[NOT LIKE]",
            "In[IN]",
            "Not In[NOT IN]"});
            this.comboBoxWHEREClause.Location = new System.Drawing.Point(126, 19);
            this.comboBoxWHEREClause.Name = "comboBoxWHEREClause";
            this.comboBoxWHEREClause.Size = new System.Drawing.Size(160, 21);
            this.comboBoxWHEREClause.TabIndex = 2;
            // 
            // comboBoxField
            // 
            this.comboBoxField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxField.FormattingEnabled = true;
            this.comboBoxField.Items.AddRange(new object[] {
            "Description[Tag]",
            "Thread Id[Tid]",
            "Date/time[Time]",
            "Version[Version]",
            "Application[Application]",
            "SOAP Method[SOAPMethod]",
            "Size[Size]",
            "Data[Data]"});
            this.comboBoxField.Location = new System.Drawing.Point(9, 19);
            this.comboBoxField.Name = "comboBoxField";
            this.comboBoxField.Size = new System.Drawing.Size(111, 21);
            this.comboBoxField.TabIndex = 0;
            // 
            // FormLogFilterEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 294);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLogFilterEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Define log filter";
            this.Load += new System.EventHandler(this.FormLogFilterEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxMatchValue;
        private System.Windows.Forms.ComboBox comboBoxWHEREClause;
        private System.Windows.Forms.ComboBox comboBoxField;
        private System.Windows.Forms.ListBox listBoxFilters;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.RadioButton radioButtonMatchAny;
        private System.Windows.Forms.RadioButton radioButtonMatchAll;
        private System.Windows.Forms.Label label1;
    }
}