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
                    
                    new Uri($"10.224.0.4"),
                    new DefaultAzureCredential()); ;
            }
        }
    }
}
