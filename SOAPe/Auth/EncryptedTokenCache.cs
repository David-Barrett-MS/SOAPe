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
                this.Deserialize(File.Exists(cacheFilePath) ?
                     ProtectedData.Unprotect(File.ReadAllBytes(cacheFilePath), null,
                                             DataProtectionScope.CurrentUser)
                                                            : null);
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
                this.Deserialize(File.Exists(cacheFilePath) ?
                                 ProtectedData.Unprotect(File.ReadAllBytes(cacheFilePath), null,
                                                         DataProtectionScope.CurrentUser)
                                                            : null);
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
                    // reflect changes in the persistent store
                    File.WriteAllBytes(cacheFilePath, ProtectedData.Protect(this.Serialize(), null, DataProtectionScope.CurrentUser));
                    // once the write operation took place, restore the HasStateChanged bit to false
                    this.HasStateChanged = false;
                }
            }
        }
    }
}
