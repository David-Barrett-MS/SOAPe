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
