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
using System.Reflection;
using System.IO;

namespace SOAPe
{
    class ClassTemplateReader
    {
        public string ReadTextTemplate(string TemplateName, string Path = "")
        {
            // Reads a text template
            try
            {
                if (!String.IsNullOrEmpty(Path))
                    if (!Path.EndsWith("."))
                        Path += ".";
                return ReadTemplate("SOAPe." + Path + TemplateName + ".txt");
            }
            catch { }
            return "";
        }

        public string ReadXMLTemplate(string TemplateName, string Path="")
        {
            // Reads the XML template
            try
            {
                if (!String.IsNullOrEmpty(Path))
                    if (!Path.EndsWith("."))
                        Path += ".";
                return ReadTemplate("SOAPe." + Path + TemplateName + ".xml");
            }
            catch { }
            return "";
        }

        private string ReadTemplate(string Template)
        {
            try
            {
                Assembly oAssembly = Assembly.GetExecutingAssembly();
                StreamReader oReader = new StreamReader(oAssembly.GetManifestResourceStream(Template));
                string sTemplateContent = oReader.ReadToEnd();
                oReader.Close();
                return sTemplateContent;
            }
            catch { }
            return "";
        }
    }
}
