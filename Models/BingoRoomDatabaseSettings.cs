using MongoDB.Driver.Core.Configuration;

namespace BingoRoomApi.Models
{
    public class BingoRoomDatabaseSettings
    {
        //public string ConnectionString { get; set; } = null!;

        //public string DatabaseName { get; set; } = null!;

        //public string BingoRoomsCollectionName { get; set; } = null!; 

        //public string ConnectionString { get; set; } = Environment.GetEnvironmentVariable("MONGODB_CONNSTRING");
        private readonly IConfiguration _config;
        private string connectionString;

        public BingoRoomDatabaseSettings(IConfiguration config)
        {
            _config = config;
        }

        public void SetConnectionstring()
        {
            connectionString = _config.GetValue<string>("BingoRoomDBConnString");
            // Use the connection string to perform database operations
        }

        public string ConnectionString { get { return connectionString; } }

        public string DatabaseName { get; set; } = "bingo_app";

        public string BingoRoomsCollectionName { get; set; } = "bingo_room";
    }
}
