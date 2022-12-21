using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Net;
using System.Net.NetworkInformation;

namespace BingoRoomApi
{
    public class KeyVaultManagement
    {
        private readonly IConfiguration _config;

        public KeyVaultManagement(IConfiguration config)
        {
            this._config = config;
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
