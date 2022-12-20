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
            _config = config;
        }

        public SecretClient SecretClient
        {
            get
            {
                return new SecretClient(
                    
                    new Uri($"https://10.224.0.4/"),
                    new DefaultAzureCredential()); ;
            }
        }
    }
}
