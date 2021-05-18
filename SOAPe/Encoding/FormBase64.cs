/*
 * By David Barrett, Microsoft Ltd. 2012. Use at your own risk.  No warranties are given.
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
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SOAPe.Encoding
{
    public partial class FormBase64 : Form
    {
        public FormBase64()
        {
            InitializeComponent();
        }

        private System.Text.Encoding SelectedEncoding()
        {
            if (radioButtonEncodingASCII.Checked)
                return System.Text.Encoding.ASCII;

            if (radioButtonEncodingUTF8.Checked)
                return System.Text.Encoding.UTF8;

            return System.Text.Encoding.Unicode;
        }

        private void ShowText(string Text)
        {
            if (checkBoxLineLength.Checked)
            {
                StringBuilder txt = new StringBuilder();
                int iLineLength = (int)numericUpDownLineLength.Value;
                for (int i = 0; i < Text.Length; i += iLineLength)
                {
                    if (i+iLineLength>Text.Length)
                    {
                        txt.Append(Text.Substring(i) + Environment.NewLine);
                    }
                    else
                        txt.Append(Text.Substring(i, iLineLength) + Environment.NewLine);
                }
            }
            else
                textBoxBase64.Text = Text;
        }

        private void buttonEncodeText_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] SourceText;
                if (radioButtonEncodingBinary.Checked)
                {
                    // Encoding binary, so need to read and convert the text
                    int iLength = textBoxText.Text.Length / 2;
                    SourceText = new byte[iLength];
                    for (int i=0; i<iLength; i++)
                    {
                        SourceText[i] = Convert.ToByte(textBoxText.Text.Substring(i * 2, 2), 16);
                    }
                }
                else
                    SourceText = SelectedEncoding().GetBytes(textBoxText.Text);
                ShowText(System.Convert.ToBase64String(SourceText));
            }
            catch (Exception ex)
            {
                textBoxBase64.Text = "Error occurred: " + ex.Message;
            }
        }

        private void buttonDecodeToText_Click(object sender, EventArgs e)
        {
            byte[] SourceEncoding=null;
            try
            {
                SourceEncoding = System.Convert.FromBase64String(textBoxBase64.Text);
            }
            catch (Exception ex)
            {
                textBoxText.Text = "Error occurred: " + ex.Message;
                return;
            }
            try
            {
                if (radioButtonEncodingBinary.Checked)
                {
                    // Binary - represent as hex
                    string sHex = "";
                    for (int i = 0; i < SourceEncoding.Length; i++)
                        sHex = sHex + GetHexString(SourceEncoding[i]);
                    textBoxText.Text = sHex;
                }
                else
                    textBoxText.Text = SelectedEncoding().GetString(SourceEncoding);
            }
            catch (Exception ex)
            {
                textBoxText.Text = "Error occurred: " + ex.Message;
            }
        }

        private string GetHexString(byte b)
        {
            Char[] cHexDigits = "0123456789ABCDEFGH".ToCharArray();
            string sHex = "";

            byte lower = (byte)(b & (byte)15);
            byte upper = (byte)((b & (byte)0xF0) >> 4);
            sHex += cHexDigits[upper];
            sHex += cHexDigits[lower];
            return sHex;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonDecodeToFile_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog oSaveDialog = new SaveFileDialog();
                oSaveDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
                oSaveDialog.FilterIndex = 2;
                oSaveDialog.RestoreDirectory = true;

                if (oSaveDialog.ShowDialog() != DialogResult.OK)
                    return;

                Stream oStream = oSaveDialog.OpenFile();
                if (!(oStream == null))
                {
                    byte[] Data = System.Convert.FromBase64String(textBoxBase64.Text);
                    oStream.Write(Data, 0, Data.Length);
                    oStream.Close();
                }
                oSaveDialog.Dispose();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error occurred: " + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void buttonEncodeFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog oOpenDialog = new OpenFileDialog();
                oOpenDialog.Filter = "All files (*.*)|*.*";
                oOpenDialog.FilterIndex = 1;
                oOpenDialog.RestoreDirectory = true;

                if (oOpenDialog.ShowDialog() != DialogResult.OK)
                    return;

                textBoxBase64.Text = "";
                this.Refresh();
                Stream oStream = oOpenDialog.OpenFile();
                byte[] data=new byte[oStream.Length];
                oStream.Read(data, 0, Convert.ToInt32(oStream.Length));
                oStream.Close();
                textBoxBase64.Text = Convert.ToBase64String(data);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error occurred: " + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void checkBoxLineLength_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownLineLength.Enabled = checkBoxLineLength.Checked;
        }
    }
}
