using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OMS.Extensions.Configuration.Vault
{
    public class VaultConfigurationSource : IConfigurationSource
    {
        #nullable enable
        private readonly ILogger? _logger;
        public VaultOptions Options { get; private set; }        
        public VaultConfigurationSource(VaultOptions options, ILogger? logger)
        {
            _logger = logger;
            Options = options;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new VaultConfigurationProvider(this, _logger);
        }
    }
}
