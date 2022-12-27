using BingoRoomApi.Models;

namespace BingoRoomApi.Interfaces
{
    public interface IBingoRoomService
    {
        Task<List<BingoRoom>> GetAsync();
        Task<BingoRoom?> GetAsync(string id);
        Task CreateAsync(BingoRoom newBingoRoom);
        Task UpdateAsync(string id, BingoRoom updatedBingoRoom);
        Task RemoveAsync(string id);
        Task<bool> GetUserIsOwnerOfBingoRoom(string bingoRoomId, string userId);
    }
}
