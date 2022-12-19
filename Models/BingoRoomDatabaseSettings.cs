using MongoDB.Driver.Core.Configuration;

namespace BingoRoomApi.Models
{
    public static class BingoRoomDatabaseSettings
    {
        private static string connectionString;

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
