using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OMS.Extensions.Configuration.Vault;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class VaultConfigurationExtensions
    {
        #nullable enable
        public static IConfigurationBuilder AddVaultConfiguration(this IConfigurationBuilder configuration, Func<VaultOptions> options, ILogger? logger = null)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (options == null) throw new ArgumentNullException(nameof(options));

            VaultOptions vaultOptions = options();
            configuration.Add(new VaultConfigurationSource(vaultOptions, logger));
            return configuration;
        }
    }
}
