/*
 * By David Barrett, Microsoft Ltd. 2012 - 2014. Use at your own risk.  No warranties are given.
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
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.IO;
using System.Security.Cryptography;

namespace SOAPe.Auth
{
    public class EncryptedTokenCache : TokenCache
    {
        private static readonly object fileLock = new object();
        private string cacheFilePath;

        // Initializes the cache against a local file.
        // If the file is already present, it loads its content in the ADAL cache
        public EncryptedTokenCache(string filePath = @".\TokenCache.dat")
        {
            cacheFilePath = filePath;
            this.AfterAccess = AfterAccessNotification;
            this.BeforeAccess = BeforeAccessNotification;
            lock (fileLock)
            {
                try
                {
                    this.DeserializeMsalV3(File.Exists(cacheFilePath) ?
                         ProtectedData.Unprotect(File.ReadAllBytes(cacheFilePath), null,
                         DataProtectionScope.CurrentUser) : null);
                }
                catch { }
            }
        }

        // Empties the persistent store.
        public override void Clear()
        {
            base.Clear();
            File.Delete(cacheFilePath);
        }

        // Triggered right before ADAL needs to access the cache.
        // Reload the cache from the persistent store in case it changed since the last access.
        void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            lock (fileLock)
            {
                try
                {
                    this.DeserializeMsalV3(File.Exists(cacheFilePath) ?
                        ProtectedData.Unprotect(File.ReadAllBytes(cacheFilePath), null,
                        DataProtectionScope.CurrentUser) : null);
                }
                catch { }
            }
        }

        // Triggered right after ADAL accessed the cache.
        void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (this.HasStateChanged)
            {
                lock (fileLock)
                {
                    try
                    {
                        // reflect changes in the persistent store
                        File.WriteAllBytes(cacheFilePath, ProtectedData.Protect(this.SerializeMsalV3(), null, DataProtectionScope.CurrentUser));
                        // once the write operation took place, restore the HasStateChanged bit to false
                        this.HasStateChanged = false;
                    }
                    catch { }
                }
            }
        }
    }
}
