namespace BingoRoomApi.Interfaces
{
    public interface IBingoAppDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
