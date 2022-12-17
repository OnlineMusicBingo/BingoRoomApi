using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace BingoRoomApi
{
    public class KeyVaultManagement
    {
        private readonly IConfiguration _config;

        public KeyVaultManagement(IConfiguration config)
        {
            _config = config;
        }

        public SecretClient SecretClient
        {
            get
            {
                return new SecretClient(
                                 new Uri($"https://{this._config["KeyVaultName"]}.vault.azure.net/"),
                                 new DefaultAzureCredential());
            }
        }
    }
}
