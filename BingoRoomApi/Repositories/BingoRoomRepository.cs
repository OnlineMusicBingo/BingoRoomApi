using BingoRoomApi.Data;
using BingoRoomApi.Interfaces;
using BingoRoomApi.Models;
using MongoDB.Driver;

namespace BingoRoomApi.Repositories
{
    public class BingoRoomRepository : IBingoRoomRepository
    {
        private readonly IMongoCollection<BingoRoom> _bingoRoomCollection;

        public BingoRoomRepository(IBingoAppDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _bingoRoomCollection = database.GetCollection<BingoRoom>("bingo_room");
        }

        public async Task<List<BingoRoom>> GetAsync() =>
            await _bingoRoomCollection.Find(_ => true).ToListAsync();
        
        public async Task<BingoRoom?> GetAsync(string id) =>
            await _bingoRoomCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(BingoRoom newBingoRoom) =>
            await _bingoRoomCollection.InsertOneAsync(newBingoRoom);

        public async Task UpdateAsync(string id, BingoRoom updatedBingoRoom) =>
            await _bingoRoomCollection.ReplaceOneAsync(x => x.Id == id, updatedBingoRoom);

        public async Task RemoveAsync(string id) =>
            await _bingoRoomCollection.DeleteOneAsync(x => x.Id == id);
    
    }
}
