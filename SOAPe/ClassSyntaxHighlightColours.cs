/*
 * By David Barrett, Microsoft Ltd. 2012. Use at your own risk.  No warranties are given.
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

namespace SOAPe
{
    class ClassSyntaxHighlightColours
    {
        private string _tagColour = "\\red0\\green0\\blue255";              // Blue
        private string _tagNameColour = "\\red163\\green21\\blue21";        // Crimson
        private string _attributeNameColour = "\\red253\\green52\\blue0";    // Red
        private string _attributeValueColour = "\\red0\\green0\\blue255";   // Blue
        private string _commentTextColour = "\\red0\\green128\\blue0";      // Green
        private string _errorTextColour = "\\red255\\green255\\blue255";    // White
        private string _errorTextHighlight = "\\red255\\green0\\blue0";     // Red

        public string ColourTable
        {
            get
            {
                StringBuilder sColourTable = new StringBuilder("{\\colortbl");
                //sColourTable.Append(";" + _defaultColour);
                sColourTable.Append(";" + _tagColour);
                sColourTable.Append(";" + _tagNameColour);
                sColourTable.Append(";" + _attributeNameColour);
                sColourTable.Append(";" + _attributeValueColour);
                sColourTable.Append(";" + _commentTextColour);
                sColourTable.Append(";" + _errorTextColour);
                sColourTable.Append(";" + _errorTextHighlight);
                sColourTable.Append(";}");
                return sColourTable.ToString();
            }
        }

        public string RTFDefaultColour
        {
            get
            {
                return "\\cf0 ";
            }
        }

        public string RTFTagColour
        {
            get
            {
                return "\\cf1 ";
            }
        }

        public string RTFTagNameColour
        {
            get
            {
                return "\\cf2 ";
            }
        }

        public string RTFAttributeNameColour
        {
            get
            {
                return "\\cf3 ";
            }
        }

        public string RTFAttributeValueColour
        {
            get
            {
                return "\\cf4 ";
            }
        }

        public string RTFCommentColour
        {
            get
            {
                return "\\cf5 ";
            }
        }

        public string RTFErrorTextColour
        {
            get
            {
                return "\\cf6 ";
            }
        }

        public string RTFErrorTextHighlightColour
        {
            get
            {
                return "\\cf7 ";
            }
        }
    }
}
