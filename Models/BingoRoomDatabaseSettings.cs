namespace BingoRoomApi.Models
{
    public class BingoRoomDatabaseSettings
    {
        //public string ConnectionString { get; set; } = null!;

        //public string DatabaseName { get; set; } = null!;

        //public string BingoRoomsCollectionName { get; set; } = null!; 
        
        public string ConnectionString { get; set; } = System.Environment.GetEnvironmentVariable("MONGODB_CONNSTRING");

        public string DatabaseName { get; set; } = "bingo_app";

        public string BingoRoomsCollectionName { get; set; } = "bingo_room";
    }
}
