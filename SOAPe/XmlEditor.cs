/*
 * By David Barrett, Microsoft Ltd. 2014. Use at your own risk.  No warranties are given.
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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Data;

namespace SOAPe
{
    public partial class XmlEditor : UserControl
    {
        private Point _cursorTracking;  // Used when we need to track the mouse pointer position
        private ClassSyntaxHighlighter _syntaxHighlighter;
        private bool _syntaxHighlight = true;
        private bool _indentXml = true;
        private bool _sendItemIdToTemplateEnabled = false;
        private bool _addExtendedPropertyEnabled = false;
        private bool _updating = false; // Used to suppress TextChanged handling
        private bool _validated = false; // Whether we have validated the Xml or not
        private List<string> _validationErrors; // List of any validation errors

        public delegate void SendItemIdEventHandler(object sender, SendItemIdEventArgs e);
        public event SendItemIdEventHandler SendItemIdToTemplate;

        public delegate void XmlValidatedEventHandler(object sender, XmlValidatedEventArgs e);
        public event XmlValidatedEventHandler XmlValidationComplete;

        public XmlEditor()
        {
            InitializeComponent();
            _validationErrors = new List<string>();
            _syntaxHighlighter = new ClassSyntaxHighlighter(this);
            richTextBoxXml.BackColor = this.BackColor;
            richTextBoxXml.ForeColor = this.ForeColor;
            richTextBoxXml.DetectUrls = false; // This seems not to work - URLs are still being detected.  This breaks syntax highlighting.
            SendItemIdToTemplateToolStripMenuItem.Visible = _sendItemIdToTemplateEnabled;
            _addExtendedPropertyEnabled = this.ReadOnly;
            addExtendedPropertyToolStripMenuItem.Visible = _addExtendedPropertyEnabled;
        }

        public XmlEditor(bool IndentXml): this()
        {
            _indentXml = IndentXml;
        }

        protected virtual void OnXmlValidationComplete(XmlValidatedEventArgs e)
        {
            XmlValidatedEventHandler handler = XmlValidationComplete;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnSendItemIdToTemplate(SendItemIdEventArgs e)
        {
            SendItemIdEventHandler handler = SendItemIdToTemplate;
            if (handler != null)
                handler(this, e);
        }

        public bool IndentXml
        {
            get { return _indentXml; }
            set
            {
                if (value == _indentXml)
                    return;
                _indentXml = value;
                if (!_indentXml)
                    richTextBoxXml.Text = (string)richTextBoxXml.Tag;
                ApplySyntaxHighlight();
            }
        }

        public bool XmlIsValid
        {
            get
            {
                if (_validated)
                    return (_validationErrors.Count == 0);
                return true;
            }
        }

        public bool SendItemIdToTemplateEnabled
        {
            get { return _sendItemIdToTemplateEnabled; }
            set {
                _sendItemIdToTemplateEnabled = value;
                SendItemIdToTemplateToolStripMenuItem.Visible = _sendItemIdToTemplateEnabled;
            }
        }

        public bool AddExtendedPropertyEnabled
        {
            get { return _addExtendedPropertyEnabled; }
            set
            {
                _addExtendedPropertyEnabled = value;
                addExtendedPropertyToolStripMenuItem.Visible = _addExtendedPropertyEnabled;
            }
        }

        public int SelectionStart
        {
            get { return richTextBoxXml.SelectionStart; }
            set { richTextBoxXml.SelectionStart = value; }
        }

        public int SelectionLength
        {
            get { return richTextBoxXml.SelectionLength; }
            set { richTextBoxXml.SelectionLength = value; }
        }

        public string Rtf
        {
            get { return richTextBoxXml.Rtf; }
            set
            {
                richTextBoxXml.Rtf = value;
                _validated = false;
            }
        }

        public override string Text
        {
            get
            {
                if (richTextBoxXml.InvokeRequired)
                {
                    string sText = "";
                    richTextBoxXml.Invoke(new MethodInvoker(delegate()
                        {
                            sText = richTextBoxXml.Text;
                        }));
                    return sText;
                }
                return richTextBoxXml.Text;
            }
            set
            {
                _updating = true;
                try
                {
                    richTextBoxXml.Invoke(new MethodInvoker(delegate ()
                    {
                        richTextBoxXml.Rtf = "";
                        richTextBoxXml.Text = value;
                        richTextBoxXml.Tag = value; // We store this so that we can restore original text if indenting is turned off
                    }));
                    _validated = false;
                    ApplySyntaxHighlight();
                }
                catch { }
                _updating = false;
            }
        }

        public bool ReadOnly
        {
            get { return richTextBoxXml.ReadOnly; }
            set 
            {
                Color backColour = richTextBoxXml.BackColor;
                richTextBoxXml.ReadOnly = value;
                richTextBoxXml.BackColor = backColour;
            }
        }

        public bool SyntaxHighlight
        {
            get { return _syntaxHighlight; }
            set
            {
                if (value == _syntaxHighlight) return;
                _syntaxHighlight = value;
                bool bUpdating = _updating;
                _updating = true;
                ApplySyntaxHighlight();
                _updating = bUpdating; // Do not want to set to false in case it is set elsewhere, so we maintain its existing value
                xmlFormattingToolStripMenuItem.Enabled=_syntaxHighlight;
            }
        }

        private void ApplySyntaxHighlight()
        {
            if (!_syntaxHighlight)
                return;
            bool bUpdating = _updating;
            _updating = true;
            if (richTextBoxXml.InvokeRequired)
            {
                richTextBoxXml.Invoke(new MethodInvoker(delegate()
                {
                    if (_syntaxHighlight)
                    {
                        if (richTextBoxXml.Text.StartsWith(" "))
                        {
                            // Valid Xml does not start with a space... Check for this and trim if necessary
                            if (richTextBoxXml.Text.TrimStart().ToLower().StartsWith("<?xml"))
                                richTextBoxXml.Text = richTextBoxXml.Text.TrimStart();
                        }
                        _syntaxHighlighter.SyntaxHighlightIfValidXml(_indentXml);
                    }
                    else
                    {
                        richTextBoxXml.Clear();
                        richTextBoxXml.Text = (string)richTextBoxXml.Tag;
                    }
                    richTextBoxXml.Update();
                }));
            }
            else
            {
                if (richTextBoxXml.IsHandleCreated)
                {
                    if (_syntaxHighlight)
                    {
                        if (richTextBoxXml.Text.StartsWith(" "))
                        {
                            // Valid Xml does not start with a space... Check for this and trim if necessary
                            if (richTextBoxXml.Text.TrimStart().ToLower().StartsWith("<?xml"))
                                richTextBoxXml.Text = richTextBoxXml.Text.TrimStart();
                        }
                        _syntaxHighlighter.SyntaxHighlightIfValidXml(_indentXml);
                    }
                    else
                    {
                        richTextBoxXml.Clear();
                        richTextBoxXml.Text = (string)richTextBoxXml.Tag;
                    }
                    richTextBoxXml.Update();
                }
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(ValidateXmlProcess), null);
            _updating = bUpdating; // Do not want to set to false in case it is set elsewhere, so we maintain its existing value
        }

        void ValidateXmlProcess(object e)
        {
            // Validate the Xml, which we do on a worker thread
            _syntaxHighlighter.XmlIsValid(out _validationErrors);
            _validated = true;
            OnXmlValidationComplete(new XmlValidatedEventArgs(_validationErrors));
        }

        private void LogMousePosition(Point CursorPos)
        {
            _cursorTracking = CursorPos;
        }

        private void MoveTextboxCursor(RichTextBox TextBox)
        {
            try
            {
                int iCharPos = TextBox.GetCharIndexFromPosition(_cursorTracking);
                TextBox.SelectionStart = iCharPos;
                TextBox.SelectionLength = 1;
            }
            catch { }
        }

        private SendItemIdEventArgs ReadItemId(RichTextBox SourceTextBox)
        {
            // Read the ItemId at the current cursor position and copy to clipboard
            bool bIsItemId = true;
            try
            {
                string sText = SourceTextBox.Text;
                int iCaret = SourceTextBox.SelectionStart;
                int iItemIdStart = sText.LastIndexOf("itemid", iCaret, iCaret, System.StringComparison.CurrentCultureIgnoreCase);
                if (iItemIdStart < 0)
                    iItemIdStart = sText.IndexOf("itemid", iCaret, iCaret, System.StringComparison.CurrentCultureIgnoreCase);

                if (iItemIdStart < 0)
                {
                    // No ItemId found, let's look for FolderId
                    iItemIdStart = sText.LastIndexOf("folderid", iCaret, iCaret, System.StringComparison.CurrentCultureIgnoreCase);
                    if (iItemIdStart < 0)
                        iItemIdStart = sText.IndexOf("folderid", iCaret, iCaret, System.StringComparison.CurrentCultureIgnoreCase);
                    if (iItemIdStart < 0) return new SendItemIdEventArgs("","");
                    bIsItemId = false;
                }

                // We've found an Id, so now read it (and ChangeKey if present)

                iItemIdStart = sText.LastIndexOf("<", iItemIdStart, iItemIdStart);
                if (iItemIdStart < 0) return new SendItemIdEventArgs("", "");
                int iItemIdEnd = sText.IndexOf("/>", iItemIdStart) + 2;
                if (iItemIdEnd < 0) return new SendItemIdEventArgs("", "");

                string sFullId = sText.Substring(iItemIdStart, iItemIdEnd - iItemIdStart);
                string sIdOnly = "";
                string sChangeKey = "";

                if (!String.IsNullOrEmpty(sFullId))
                {
                    // Find and extract the Id
                    int iIdStart = sFullId.ToLower().IndexOf("id=");
                    if (iIdStart > 0)
                    {
                        iIdStart = sFullId.IndexOf("\"", iIdStart) + 1;
                        int iIdEnd = sFullId.IndexOf("\"", iIdStart);
                        if (iIdEnd > iIdStart)
                        {
                            sIdOnly = sFullId.Substring(iIdStart, iIdEnd - iIdStart);
                            iItemIdStart = iItemIdStart + iIdStart;
                            iItemIdEnd = iItemIdStart + sIdOnly.Length;
                        }
                    }

                    // Find and extract the ChangeKey
                    int iChangeKeyStart = sFullId.ToLower().IndexOf("changekey=");
                    if (iChangeKeyStart > 0)
                    {
                        iChangeKeyStart = sFullId.IndexOf("\"", iChangeKeyStart) + 1;
                        int iChangeKeyEnd = sFullId.IndexOf("\"", iChangeKeyStart);
                        if (iChangeKeyEnd > iChangeKeyStart)
                        {
                            sChangeKey = sFullId.Substring(iChangeKeyStart, iChangeKeyEnd - iChangeKeyStart);
                            iChangeKeyStart = iChangeKeyStart + iIdStart;
                            iItemIdEnd = iChangeKeyStart + sIdOnly.Length;
                        }
                    }
                }

                if (bIsItemId)
                    return new SendItemIdEventArgs(sIdOnly, "", sChangeKey);

                return new SendItemIdEventArgs("", sIdOnly, sChangeKey);
            }
            catch { }
            return new SendItemIdEventArgs("", "");
        }
        private void CopyItemId(RichTextBox SourceTextBox, bool RemoveChangeKey = false, bool IDOnly = false)
        {
            // Read the ItemId at the current cursor position and copy to clipboard
            try
            {
                string sText = SourceTextBox.Text;
                int iCaret = SourceTextBox.SelectionStart;
                int iItemIdStart = sText.LastIndexOf("id ", iCaret, iCaret, System.StringComparison.CurrentCultureIgnoreCase);
                if (iItemIdStart < 0)
                {
                    // No ItemId found, let's look for FolderId
                    iItemIdStart = sText.LastIndexOf("folderid", iCaret, iCaret, System.StringComparison.CurrentCultureIgnoreCase);
                    if (iItemIdStart < 0) return;
                }
                iItemIdStart = sText.LastIndexOf("<", iItemIdStart, iItemIdStart);
                if (iItemIdStart < 0) return;
                int iItemIdEnd = sText.IndexOf("/>", iItemIdStart) + 2;
                if (iItemIdEnd < 0) return;

                string sItemId = sText.Substring(iItemIdStart, iItemIdEnd - iItemIdStart);
                if (!String.IsNullOrEmpty(sItemId))
                {
                    if (IDOnly)
                    {
                        // We only want the actual ID (none of the XML)
                        int iIdStart = sItemId.ToLower().IndexOf("id=");
                        if (iIdStart > 0)
                        {
                            iIdStart = sItemId.IndexOf("\"", iIdStart) + 1;
                            int iIdEnd = sItemId.IndexOf("\"", iIdStart);
                            if (iIdEnd > iIdStart)
                            {
                                sItemId = sItemId.Substring(iIdStart, iIdEnd - iIdStart);
                                iItemIdStart = iItemIdStart + iIdStart;
                                iItemIdEnd = iItemIdStart + sItemId.Length;
                            }
                        }
                    }
                    else if (RemoveChangeKey)
                    {
                        // Remove the ChangeKey from the ItemId
                        int iChangeKeyStart = sItemId.ToLower().IndexOf("changekey");
                        if (iChangeKeyStart > 0)
                        {
                            int iChangeKeyEnd = sItemId.IndexOf("\"", iChangeKeyStart);
                            iChangeKeyEnd = sItemId.IndexOf("\"", iChangeKeyEnd + 1);
                            if (iChangeKeyEnd > iChangeKeyStart)
                                sItemId = sItemId.Substring(0, iChangeKeyStart) + (sItemId.Substring(iChangeKeyEnd + 1));
                        }
                    }
                }
                SourceTextBox.SelectionStart = iItemIdStart;
                SourceTextBox.SelectionLength = iItemIdEnd - iItemIdStart;
                SourceTextBox.Focus();

                // Now copy to clipboard
                System.Windows.Forms.Clipboard.SetText(sItemId);
            }
            catch { }
        }

        private void SelectXMLElementAtCursor(RichTextBox TextBox)
        {
            // Select the XML element at the cursor position
            try
            {
                string sText = TextBox.Text;
                int iStartPos = TextBox.SelectionStart;
                iStartPos = sText.LastIndexOf("<", iStartPos);
                if (sText.Substring(iStartPos + 1, 1) == "/")
                {
                    // This is a closing element, so find next...
                    iStartPos = sText.LastIndexOf("<", iStartPos - 1);
                }
                int iEndPos = sText.IndexOf("/>", iStartPos);
                if (sText.IndexOf(">", iStartPos) < iEndPos)
                    iEndPos = -1;
                if (iEndPos < 0)
                {
                    // Need to find closing element
                    iEndPos = sText.IndexOf(">", iStartPos);
                    if (sText.IndexOf(" ", iStartPos) < iEndPos)
                        iEndPos = sText.IndexOf(" ", iStartPos);
                    string sTag = sText.Substring(iStartPos + 1, iEndPos - iStartPos - 1);
                    string sClosingTag = "</" + sTag;
                    iEndPos = sText.IndexOf(sClosingTag, iStartPos) + sClosingTag.Length;
                    if (iEndPos > 0)
                        iEndPos = sText.IndexOf(">", iEndPos);
                }
                else
                    iEndPos++;
                if (iEndPos > iStartPos)
                {
                    TextBox.SelectionStart = iStartPos;
                    TextBox.SelectionLength = iEndPos - iStartPos + 1;
                    TextBox.Focus();
                }
            }
            catch { }
        }

        private void selectXMLElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveTextboxCursor(richTextBoxXml);
            SelectXMLElementAtCursor(richTextBoxXml);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxXml.Copy();
        }

        private void copyItemIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveTextboxCursor(richTextBoxXml);
            CopyItemId(richTextBoxXml);
        }

        private void richTextBoxXml_MouseUp(object sender, MouseEventArgs e)
        {
            LogMousePosition(new Point(e.X, e.Y));
        }

        private void richTextBoxXml_MouseDown(object sender, MouseEventArgs e)
        {
            LogMousePosition(new Point(e.X, e.Y));
        }

        private void copyItemIdwithoutChangeKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveTextboxCursor(richTextBoxXml);
            CopyItemId(richTextBoxXml, true);
        }

        private void copyItemIdIdOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveTextboxCursor(richTextBoxXml);
            CopyItemId(richTextBoxXml, true, true);
        }

        private void contextMenuLogXml_Opening(object sender, CancelEventArgs e)
        {
            // Enable or disable commands as appropriate
            bool bCutCopyAvailable = (richTextBoxXml.SelectedText.Length > 0);
            cutToolStripMenuItem.Enabled = (bCutCopyAvailable && !richTextBoxXml.ReadOnly);
            copyToolStripMenuItem.Enabled = bCutCopyAvailable;

            bool bPasteAvailable = System.Windows.Forms.Clipboard.ContainsText() && !richTextBoxXml.ReadOnly;
            PasteToolStripMenuItem.Enabled = bPasteAvailable;

            syntaxHighlightingToolStripMenuItem.Checked = _syntaxHighlight;
            xmlFormattingToolStripMenuItem.Checked = _indentXml;
            LogMousePosition(new Point(contextMenuLogXml.Left, contextMenuLogXml.Top));
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxXml.Cut();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxXml.Paste();
        }

        private void syntaxHighlightingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SyntaxHighlight = syntaxHighlightingToolStripMenuItem.Checked;
            if (!SyntaxHighlight)
            {
                // Remove syntax highlighting
                richTextBoxXml.Text = richTextBoxXml.Text;  // This clears all the RTF formatting
            }
            else
                ApplySyntaxHighlight();
        }

        private void xmlFormattingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.IndentXml = xmlFormattingToolStripMenuItem.Checked;
        }

        private void XmlEditor_BackColorChanged(object sender, EventArgs e)
        {
            richTextBoxXml.BackColor = this.BackColor;
        }

        private void XmlEditor_ForeColorChanged(object sender, EventArgs e)
        {
            richTextBoxXml.ForeColor = this.ForeColor;
        }

        private void richTextBoxXml_TextChanged(object sender, EventArgs e)
        {
            if (_updating)
                return;
            ApplySyntaxHighlight();
        }

        private void selectXmlValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // First of all we select the Xml element, then we will look within it for the value
            try
            {
                MoveTextboxCursor(richTextBoxXml);
                SelectXMLElementAtCursor(richTextBoxXml);
                string xmlElement = richTextBoxXml.SelectedText;
                if ((xmlElement.Length - xmlElement.Replace("<", "").Length) > 2) return; // Nested Xml

                int xmlElementStart = richTextBoxXml.SelectionStart;
                int xmlElementEnd = xmlElementStart + richTextBoxXml.SelectionLength;
                int xmlValueStart = xmlElement.IndexOf(">");
                if (xmlValueStart < 0) return;
                xmlValueStart++;
                int xmlValueEnd = xmlElement.IndexOf("<", xmlValueStart);
                if (xmlValueEnd < 0) xmlValueEnd = xmlElement.Length;

                // Now select the Xml value
                richTextBoxXml.SelectionStart = richTextBoxXml.SelectionStart + xmlValueStart;
                richTextBoxXml.SelectionLength = xmlValueEnd - xmlValueStart;
                richTextBoxXml.Focus();
            }
            catch { }
        }

        private void selectXmlAttributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Select the XML attribute value at the cursor position

            try
            {
                MoveTextboxCursor(richTextBoxXml);
                string sText = richTextBoxXml.Text;
                int iStartPos = richTextBoxXml.SelectionStart;
                string sQuote = "";
                for (int i = iStartPos; i > 0; i--)
                {
                    char c = sText[i];
                    if (c.Equals('\"') || c.Equals('\''))
                    {
                        // Check if this is the start of the attribute
                        int j = i;
                        while (j > 0)
                        {
                            j--;
                            if (sText[j].Equals('='))
                            {
                                // We're going to assume this is the start of the attribute
                                // There are some cases where this assumption is bad, but these should be fairly rare (I may look into a more robust algorithm in the future)
                                iStartPos = i;
                                sQuote = c.ToString();
                                break;
                            }
                            if (!sText[j].Equals(" "))
                                break;
                        }
                    }
                    if (!sQuote.Equals(""))
                        break;
                }

                if (!sQuote.Equals(""))
                {
                    int iEndPos = sText.IndexOf(sQuote[0], iStartPos + 1) - 1;
                    if (iEndPos > iStartPos)
                    {
                        richTextBoxXml.SelectionStart = iStartPos + 1;
                        richTextBoxXml.SelectionLength = iEndPos - iStartPos;
                        richTextBoxXml.Focus();
                    }
                }
            }
            catch { }
        }

        private void SendItemIdToTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Read the ItemId, then open template selector specifying ItemId

            SendItemIdEventArgs sItemId = ReadItemId(richTextBoxXml);
            if (String.IsNullOrEmpty(sItemId.FolderId) && String.IsNullOrEmpty(sItemId.ItemId))
            {
                System.Windows.Forms.MessageBox.Show("Unable to read ItemId.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OnSendItemIdToTemplate(sItemId);
        }

        private void validateXmlincludingCharacterTestingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // We test the Xml for validity
            StringBuilder validXml = new StringBuilder(); ;
            List<Exception> validationErrors = new List<Exception>();

            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ConformanceLevel = ConformanceLevel.Document;
                settings.IgnoreWhitespace = true;
                settings.IgnoreComments = true;
                settings.CheckCharacters = true;
                using (XmlReader xmlReader = XmlReader.Create(new StringReader(richTextBoxXml.Text)))
                {
                    while (xmlReader.Read())
                    {
                        validXml.Append(xmlReader.ReadOuterXml());
                    }
                }
            }
            catch (XmlException ex)
            {
                // Invalid XML, so don't highlight
                validationErrors.Add(ex);
            }

            if (validationErrors.Count > 0)
            {
                List<string> errorList = new List<string>();
                foreach (XmlException error in validationErrors)
                {
                    errorList.Add(error.Message);
                }
                MessageBox.Show(this, String.Join(Environment.NewLine, errorList), "XML failed validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show(this, "No issues found.", "XML appears to be valid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void addExtendedPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!XmlIsValid)
            {
                MessageBox.Show(this, "The Xml is not valid.  Please create a valid request before adding an extended property.", "Invalid Xml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FormExtendedPropertySelector frm = new FormExtendedPropertySelector();
            if (frm.ShowDialog(this) == DialogResult.OK)
                AddExtendedPropertyToRequest(frm.ExtendedPropertyXml(), frm.ExtendedPropertyXmlDescription());

            frm.Dispose();
        }

        /// <summary>
        /// Add the given extended property to the displayed XML
        /// </summary>
        /// <param name="ExtendedPropertyXml">Full XML definition of the extended property</param>
        private void AddExtendedPropertyToRequest(string ExtendedPropertyXml, string PropertyDescription)
        {
            if (String.IsNullOrEmpty(ExtendedPropertyXml))
                return;

            string PropXmlWithNS = $"<t:{ExtendedPropertyXml.Substring(1)}";

            if (richTextBoxXml.Text.Contains(ExtendedPropertyXml) || richTextBoxXml.Text.Contains(PropXmlWithNS))
            { 
                MessageBox.Show(this, "This extended property is already in the request.", "Extended property already exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Console.WriteLine(ExtendedPropertyXml);
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.LoadXml(richTextBoxXml.Text);
            }
            catch
            {
                MessageBox.Show(this, "The Xml is not valid.  Please create a valid request before adding an extended property.", "Invalid Xml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //  Check for FolderShape, ItemShape, or AttachmentShape elements
            string[] shapeElements = { "FolderShape", "ItemShape", "AttachmentShape" };
            XmlNodeList xmlShapeNode = null;
            foreach (string shapeElement in shapeElements)
            {
                xmlShapeNode = xmlDoc.GetElementsByTagName(shapeElement);
                if (xmlShapeNode.Count > 0)
                    break;
            }

            if (xmlShapeNode.Count > 0)
            {
                // We've found a shape element, so add the extended property to it
                XmlNode xmlShape = xmlShapeNode[0];
                XmlNode xmlExtendedPropertyParentNode = null;
                foreach (XmlNode xmlChild in xmlShape.ChildNodes)
                {
                    if (xmlChild.Name == "AdditionalProperties")
                    {
                        xmlExtendedPropertyParentNode = xmlChild;
                        break;
                    }
                }
                if (xmlExtendedPropertyParentNode==null)
                    xmlExtendedPropertyParentNode = xmlShape.AppendChild(xmlDoc.CreateNode(XmlNodeType.Element, "AdditionalProperties", "http://schemas.microsoft.com/exchange/services/2006/types"));

                XmlDocument xmlExtendedPropertyDoc = new XmlDocument();
                
                xmlExtendedPropertyDoc.LoadXml(ExtendedPropertyXml);
                XmlNode newPropNode = xmlExtendedPropertyParentNode.OwnerDocument.ImportNode(xmlExtendedPropertyDoc.DocumentElement, false);
                xmlExtendedPropertyParentNode.AppendChild(newPropNode);
                richTextBoxXml.Text = xmlDoc.OuterXml;
                return;
            }

            MessageBox.Show(this, "Currently only Shape elements are supported for adding extended properties.", "Unsupported operation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class SendItemIdEventArgs : EventArgs
    {
        public SendItemIdEventArgs(string ItemId, string FolderId)
        {
            this.ItemId = ItemId;
            this.FolderId = FolderId;
        }

        public SendItemIdEventArgs(string ItemId, string FolderId, string ChangeKey): this(ItemId, FolderId)
        {
            this.ChangeKey = ChangeKey;
        }

        public string ItemId { get; }

        public string FolderId { get; }

        public string ChangeKey { get; }
    }

    public class XmlValidatedEventArgs: EventArgs
    {
        private List<string> _validationErrors;

        public XmlValidatedEventArgs(List<string> ValidationErrors)
        {
            _validationErrors = ValidationErrors;
        }

        public bool IsValid
        {
            get { return (_validationErrors.Count == 0); }
        }

        public List<string> ValidationErrors
        {
            get { return _validationErrors; }
        }
    }
}
