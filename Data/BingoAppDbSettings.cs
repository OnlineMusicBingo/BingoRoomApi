using BingoRoomApi.Interfaces;

namespace BingoRoomApi.Data
{
    public class BingoAppDbSettings : IBingoAppDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
