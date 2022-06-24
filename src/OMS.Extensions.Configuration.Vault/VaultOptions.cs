using System;

namespace OMS.Extensions.Configuration.Vault
{
    public class VaultOptions
    {
        public string VaultUri { get; set; }
        public string RootToken { get; set; }
        public string BasePath { get; set; }
        public string SubPathToUse { get; set; }
        public int Version { get; set; }
    }
}
