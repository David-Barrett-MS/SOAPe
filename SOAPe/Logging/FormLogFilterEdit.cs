/*
 * By David Barrett, Microsoft Ltd. 2016. Use at your own risk.  No warranties are given.
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
using System.Text;
using System.Windows.Forms;

namespace SOAPe
{
    public partial class FormLogFilterEdit : Form
    {
        private bool _cancel = false;
        public FormLogFilterEdit(string[] CurrentFilters)
        {
            InitializeComponent();
            ReadFilters(CurrentFilters);
        }

        private void ReadFilters(string[] Filters)
        {
            if (Filters == null)
                return;
            if (Filters.Length < 2)
                return;

            if (Filters[0] == " AND ")
            {
                radioButtonMatchAll.Checked = true;
            }
            else
                radioButtonMatchAny.Checked = true;

            listBoxFilters.Items.Clear();
            for (int i=1; i<Filters.Length; i++)
            {
                listBoxFilters.Items.Add(Filters[i]);
            }
        }

        private void FormLogFilterEdit_Load(object sender, EventArgs e)
        {
            InitialiseCombo(comboBoxField);
            InitialiseCombo(comboBoxWHEREClause);
        }

        private void InitialiseCombo(ComboBox combo)
        {
            // Initialise the combobox (we process the list and extract anything within [], which is then moved to the Tag

            string[] keywords = new string[combo.Items.Count];
            for (int i = 0; i < combo.Items.Count; i++)
            {
                string item = (string)combo.Items[i];
                string tag = item;
                if (item.Contains("["))
                {
                    tag = item.Substring(item.IndexOf("[") + 1).Trim();
                    item = item.Substring(0, item.IndexOf("["));
                    if (tag.EndsWith("]"))
                        tag = tag.Substring(0,tag.Length - 1);
                    keywords[i] = tag;
                    combo.Items[i] = item;
                }
            }
            combo.Tag = keywords;
        }

        public string[] GetFilters(string[] filters, IWin32Window owner = null)
        {
            _cancel = false;
            ReadFilters(filters);
            this.ShowDialog(owner);
            if (_cancel)
                return filters;

            string[] newFilters = new string[listBoxFilters.Items.Count+1];
            if (radioButtonMatchAll.Checked)
            {
                newFilters[0] = " AND ";
            }
            else
                newFilters[0] = " OR ";

            for (int i=1; i<=listBoxFilters.Items.Count; i++)
            {
                newFilters[i] = listBoxFilters.Items[i-1].ToString();
            }
            return newFilters;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _cancel = true;
            this.Hide();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private string ExtractEntity(string Element)
        {
            try
            {
                int iEntityStart = Element.IndexOf("[") + 1;
                string sEntity = Element.Substring(iEntityStart);
                sEntity = sEntity.Substring(0, sEntity.Length - 1);
                return sEntity;
            }
            catch { }
            return String.Empty;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            StringBuilder sFilter = new StringBuilder("(");
            try
            {
                string sField = ((string[])comboBoxField.Tag)[comboBoxField.SelectedIndex];
                sFilter.Append(String.Format("[{0}] ", sField));

                sFilter.Append(((string[])comboBoxWHEREClause.Tag)[comboBoxWHEREClause.SelectedIndex]);
                sFilter.Append(" ");

                string sDelimiter = "'";
                if (sField.Equals("Time"))
                    sDelimiter = "#";

                sFilter.Append(sDelimiter);
                sFilter.Append(textBoxMatchValue.Text);
                sFilter.Append(sDelimiter);
                sFilter.Append(")");
            } catch
            {
                return;
            }

            listBoxFilters.Items.Add(sFilter.ToString());
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxFilters.SelectedIndex < 0)
                return;
            try
            {
                listBoxFilters.Items.RemoveAt(listBoxFilters.SelectedIndex);
            }
            catch { }
        }

        private void listBoxFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRemove.Enabled = (listBoxFilters.SelectedIndex >= 0);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listBoxFilters.Items.Clear();
        }


    }
}
