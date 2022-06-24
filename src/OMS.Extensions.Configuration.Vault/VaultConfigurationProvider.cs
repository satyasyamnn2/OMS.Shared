using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;
using VaultSharp.V1.SecretsEngines;

namespace OMS.Extensions.Configuration.Vault
{
    public class VaultConfigurationProvider: ConfigurationProvider
    {
        private IVaultClient _vaultClient;
        private VaultConfigurationSource _source;
        #nullable enable
        private ILogger? _logger;
        private VaultOptions _options;

        public VaultConfigurationProvider(VaultConfigurationSource source, ILogger? logger)
        {
            _source = source;
            _logger = logger;
            _options = _source.Options;
        }

        public override void Load()
        {
            if (_vaultClient == null)
            {
                IAuthMethodInfo authMethod = new TokenAuthMethodInfo(_options.RootToken);
                VaultClientSettings settings = new VaultClientSettings(_options.VaultUri, authMethod);
                _vaultClient = new VaultClient(settings);
            }
            LoadData().ConfigureAwait(true);
        }


        private async Task LoadData()
        {
            var kv2SecretsEngine = new SecretsEngine
            {
                Type = SecretsEngineType.KeyValueV2,
                Config = new Dictionary<string, object>
                {
                    {"version", _source.Options.Version}
                },
                Path = _source.Options.BasePath
            };

            var paths = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretPathsAsync("", kv2SecretsEngine.Path).ConfigureAwait(false);
            foreach (string path in paths.Data.Keys)
            {
                if (_options.SubPathToUse.Equals(path))
                {
                    Secret<SecretData> secretData = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path, null, kv2SecretsEngine.Path).ConfigureAwait(false);
                    IDictionary<string, object> data = secretData.Data.Data;
                    foreach (string key in data.Keys)
                    {
                        Set(key, data[key].ToString());
                    }
                }
            }
        }
    }
}
