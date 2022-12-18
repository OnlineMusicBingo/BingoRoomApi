using MongoDB.Driver.Core.Configuration;

namespace BingoRoomApi.Models
{
    public static class BingoRoomDatabaseSettings
    {
        //public string ConnectionString { get; set; } = null!;

        //public string DatabaseName { get; set; } = null!;

        //public string BingoRoomsCollectionName { get; set; } = null!; 

        //public string ConnectionString { get; set; } = Environment.GetEnvironmentVariable("MONGODB_CONNSTRING");
        //private static readonly IConfiguration _config;
        private static string connectionString;

        /*public static BingoRoomDatabaseSettings(IConfiguration config)
        {
            _config = config;
        }*/

        public static void SetConnectionstring(string connString)
        {
            connectionString = connString;
            // Use the connection string to perform database operations
        }

        public static string ConnectionString { get { return connectionString; } }

        public static string DatabaseName { get; set; } = "bingo_app";

        public static string BingoRoomsCollectionName { get; set; } = "bingo_room";
    }
}
