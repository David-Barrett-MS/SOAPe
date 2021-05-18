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

using System.Text;

namespace SOAPe
{
    class ClassSyntaxHighlightColours
    {
        private string _tagColour = @"\red0\green0\blue255";              // Blue
        private string _tagNameColour = @"\red163\green21\blue21";        // Crimson
        private string _attributeNameColour = @"\red253\green52\blue0";    // Red
        private string _attributeValueColour = @"\red0\green0\blue255";   // Blue
        private string _commentTextColour = @"\red\green128\blue0";      // Green
        private string _errorTextColour = @"\red255\green255\blue255";    // White
        private string _errorTextHighlight = @"\red255\green0\blue0";     // Red

        public string ColourTable
        {
            get
            {
                StringBuilder sColourTable = new StringBuilder(@"{\colortbl");
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
                return @"\cf0 ";
            }
        }

        public string RTFTagColour
        {
            get
            {
                return @"\cf1 ";
            }
        }

        public string RTFTagNameColour
        {
            get
            {
                return @"\cf2 ";
            }
        }

        public string RTFAttributeNameColour
        {
            get
            {
                return @"\cf3 ";
            }
        }

        public string RTFAttributeValueColour
        {
            get
            {
                return @"\cf4 ";
            }
        }

        public string RTFCommentColour
        {
            get
            {
                return @"\cf5 ";
            }
        }

        public string RTFErrorTextColour
        {
            get
            {
                return @"\cf6 ";
            }
        }

        public string RTFErrorTextHighlightColour
        {
            get
            {
                return @"\cf7 ";
            }
        }
    }
}
