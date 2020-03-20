/*
 * By David Barrett, Microsoft Ltd. 2013-2014. Use at your own risk.  No warranties are given.
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
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace SOAPe
{
    public sealed class ClassSyntaxHighlighter: IDisposable
    {
        private XmlEditor _xmlEditor = null;
        private ClassSyntaxHighlightColours _syntaxColours;
        private RichTextBox _rtb;

        public ClassSyntaxHighlighter(XmlEditor xmlEditor)
        {
            _xmlEditor = xmlEditor;
            _syntaxColours = new ClassSyntaxHighlightColours();
            _rtb = new RichTextBox(); // Used for temporary processing
        }

        public void Dispose()
        {
            if (_rtb != null)
                _rtb.Dispose();
            _rtb = null;
        }

        public string RTFColourTable
        {
            get { return _syntaxColours.ColourTable; }
        }

        private string AddColourTable(string RTF)
        {
            // We need to add the colour table for the syntax highlighting.  Replace any existing colour table.
            int iCTableStart = RTF.IndexOf(@"{\colortbl;");

            if (iCTableStart != -1)
            {
                // Need to replace existing colour table
                int iCTableEnd = RTF.IndexOf('}', iCTableStart);
                RTF = RTF.Remove(iCTableStart, iCTableEnd - iCTableStart);
                RTF = RTF.Insert(iCTableStart, _syntaxColours.ColourTable);
            }
            else
            {
                // Adding new colour table
                int iRTFLoc = RTF.IndexOf(@"\rtf");
                if (iRTFLoc < 0)
                {
                    RTF = RTF.Insert(0, @"{\rtf\ansi\deff0" + _syntaxColours.ColourTable);
                    RTF += "}";
                }
                else
                {
                    int iInsertLoc = RTF.IndexOf('{', iRTFLoc);
                    if (iInsertLoc == -1) iInsertLoc = RTF.IndexOf('}', iRTFLoc) - 1;
                    RTF = RTF.Insert(iInsertLoc, _syntaxColours.ColourTable);
                }
            }
            return RTF;
        }

        public bool SyntaxHighlightIfValidXml(bool Indent=false)
        {
            if (String.IsNullOrEmpty(_xmlEditor.Text))
                return true;

            XmlDocument xml = new XmlDocument();

            try
            {
                xml.LoadXml(_xmlEditor.Text);
            }
            catch
            {
                // Invalid XML, so don't highlight
                return false;
            }
            SyntaxHighlight(Indent);
            return true;
        }

        public bool XmlIsValid(out List<string> ValidationErrors, bool ShowResults = false)
        {
            // We test the Xml for validity

            ValidationErrors = new List<string>();
            if (String.IsNullOrEmpty(_xmlEditor.Text))
                return true;

            StringBuilder validXml = new StringBuilder();

            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ConformanceLevel = ConformanceLevel.Document;
                settings.IgnoreWhitespace = true;
                settings.IgnoreComments = true;
                settings.CheckCharacters = true;
                using (XmlReader xmlReader = XmlReader.Create(new StringReader(_xmlEditor.Text)))
                {
                    while (xmlReader.Read())
                    {
                        validXml.Append(xmlReader.ReadOuterXml());
                    }
                }
            }
            catch (XmlException ex)
            {
                ValidationErrors.Add(ex.Message);
            }

            if (ValidationErrors.Count > 0)
            {
                if (ShowResults)
                    System.Windows.Forms.MessageBox.Show(String.Join(Environment.NewLine, ValidationErrors), "XML failed validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            
            if (ShowResults)
                System.Windows.Forms.MessageBox.Show("No issues found.", "XML appears to be valid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }


        public void SyntaxHighlight(bool Indent=false)
        {
            // Add XML syntax highlighting

            int iSelectionStart = _xmlEditor.SelectionStart;
            int iSelectionLength = _xmlEditor.SelectionLength;

            
            string sRTF = null;
            if (Indent)
            {
                _rtb.Text = this.Indent(_xmlEditor.Text);
            }
            else
                _rtb.Text = _xmlEditor.Text;
            sRTF = _rtb.Rtf;
            
            StringBuilder sHighlightedRTF = new StringBuilder("");
            bool bWithinTag = false;
            bool bWithinTagName = false;
            bool bWithinQuotes = false;
            bool bWithinComment = false;

            for (int i = 0; i < sRTF.Length; i++)
            {
                bool bAppended = false; // Keep track of whether we append this character or not
                if (bWithinTagName)
                {
                    if (sRTF[i] == ' ')
                    {
                        // After tag name, anything else within the tag must be an attribute
                        sHighlightedRTF.Append(_syntaxColours.RTFAttributeNameColour);
                        bWithinTagName = false;
                    }
                }
                else if (bWithinTag)
                {
                    if (sRTF[i] == '"')
                    {
                        if (bWithinQuotes)
                        {
                            sHighlightedRTF.Append(sRTF[i]);
                            sHighlightedRTF.Append(_syntaxColours.RTFAttributeNameColour);
                            bAppended = true;
                            bWithinQuotes = false;
                        }
                        else
                        {
                            sHighlightedRTF.Append(_syntaxColours.RTFAttributeValueColour);
                            bWithinQuotes = true;
                        }
                    }
                }

                if (sRTF[i] == '<')
                {
                    if (!bWithinComment)
                    {
                        // Opening tag
                        bWithinTag = true;

                        if (sRTF[i + 1] == '!')
                        {
                            //Check for comments tags 
                            if ( (sRTF[i + 2] == '-') && (sRTF[i + 3] == '-') )
                            {
                                sHighlightedRTF.Append(_syntaxColours.RTFCommentColour);
                                bWithinComment = true;
                            }
                            else
                            {
                                sHighlightedRTF.Append(_syntaxColours.RTFTagNameColour);
                                bWithinTagName = true;
                            }
                        }
                        if (!bWithinComment)
                        {
                            // Update tag colour
                            sHighlightedRTF.Append(_syntaxColours.RTFTagColour);
                            sHighlightedRTF.Append(sRTF[i]);
                            bAppended = true;
                            if (sRTF[i + 1] == '?' || sRTF[i + 1] == '/')
                            {
                                i++;
                                sHighlightedRTF.Append(sRTF[i]);
                            }
                            sHighlightedRTF.Append(_syntaxColours.RTFTagNameColour);
                            bWithinTagName = true;
                        }
                    }
                }

                bool bClosingTag = false;
                if (sRTF[i] == '>') bClosingTag = true;
                if (i < sRTF.Length-1)
                {
                    if (sRTF[i + 1] == '>')
                    {
                        if (sRTF[i] == '?' || sRTF[i] == '/')
                            bClosingTag = true;
                    }
                }

                if (bClosingTag)
                {
                    // We've got a closing tag
                    if (bWithinComment)
                    {
                        if (sRTF[i - 1] == '-' && sRTF[i - 2] == '-')
                        {
                            sHighlightedRTF.Append(sRTF[i]);
                            sHighlightedRTF.Append(_syntaxColours.RTFDefaultColour);
                            bAppended = true;
                            bWithinComment = false;
                            bWithinTag = false;
                        }
                    }

                    if (bWithinTag)
                    {
                        sHighlightedRTF.Append(_syntaxColours.RTFTagColour);
                        if ((sRTF[i] == '/') || (sRTF[i] == '?'))
                        {
                            sHighlightedRTF.Append(sRTF[i++]);
                        }
                        sHighlightedRTF.Append(sRTF[i]);
                        sHighlightedRTF.Append(_syntaxColours.RTFDefaultColour);
                        bAppended = true;
                        bWithinTagName = false;
                        bWithinTag = false;
                    }
                }

                if (!bAppended) sHighlightedRTF.Append(sRTF[i]);
            }

            _xmlEditor.Rtf = AddColourTable(sHighlightedRTF.ToString());
            _xmlEditor.SelectionStart = iSelectionStart;
            _xmlEditor.SelectionLength = iSelectionLength;
        }

        public string Indent(string xml)
        {
            string sIndentedXml;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                using (System.IO.MemoryStream xmlStream = new System.IO.MemoryStream())
                {
                    using (XmlTextWriter xmlWriter = new XmlTextWriter(new System.IO.StreamWriter(xmlStream)))
                    {
                        xmlWriter.Formatting = Formatting.Indented;
                        xmlDoc.Save(xmlWriter);
                        using (System.IO.StreamReader xmlReader = new System.IO.StreamReader(xmlStream))
                        {
                            xmlStream.Position = 0;
                            sIndentedXml = xmlReader.ReadToEnd();
                        }
                    }
                }
                return sIndentedXml;
            }
            catch
            {
                return xml;
            }
        }
    }
}
