/*
 * By David Barrett, Microsoft Ltd. 2018. Use at your own risk.  No warranties are given.
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Reflection;
using System.IdentityModel.Tokens.Jwt;

namespace SOAPe.Auth
{
    public partial class FormTokenViewer : Form
    {
        private TokenCache _tokenCache;

        public FormTokenViewer(TokenCache tokenCache)
        {
            InitializeComponent();
            _tokenCache = tokenCache;
            ShowTokens();
        }

        private void ShowTokens()
        {
            // List all our cached tokens

            listBoxTokens.Items.Clear();
            if (_tokenCache.Count < 1)
                return;

            foreach (TokenCacheItem item in _tokenCache.ReadItems())
            {
                listBoxTokens.Items.Add(item);
            }
        }

        private void ShowTokenInfo()
        {
            // Show selected info from the currently selected token

            richTextBoxTokenInfo.Clear();
            if (listBoxTokens.Items.Count < 1 || listBoxTokens.SelectedIndex < 0)
                return;

            try
            {
                TokenCacheItem tokenCacheItem = (TokenCacheItem)listBoxTokens.SelectedItem;
                string elementToView = listBoxTokenElementToShow.Text.Replace(" ", "");
                PropertyInfo prop = tokenCacheItem.GetType().GetProperty(elementToView, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null)
                {
                    string tokenElement = (string)prop.GetValue(tokenCacheItem).ToString();
                    if (radioButtonViewRaw.Checked)
                    {
                        // Show raw value
                        richTextBoxTokenInfo.Text = tokenElement;
                    }
                    else
                    {
                        // Attempt to decode token
                        try
                        {
                            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                            JwtSecurityToken token = (JwtSecurityToken)handler.ReadToken(tokenElement);
                            richTextBoxTokenInfo.Text = token.ToString();
                        }
                        catch
                        {
                            richTextBoxTokenInfo.Text = tokenElement;
                        }
                    }
                }
            }
            catch { }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBoxTokens_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTokenInfo();
        }

        private void comboBoxTokenElementToShow_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonViewRaw_CheckedChanged(object sender, EventArgs e)
        {
            ShowTokenInfo();
        }

        private void radioButtonViewAsJSON_CheckedChanged(object sender, EventArgs e)
        {
            ShowTokenInfo();
        }

        private void ListBoxTokenElementToShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTokenInfo();
        }
    }
}
