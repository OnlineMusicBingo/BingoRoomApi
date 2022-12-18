using BingoRoomApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BingoRoomApi
{
    public class BingoRoomService
    {
        private readonly ILogger<BingoRoomService> _logger;

        private readonly IMongoCollection<BingoRoom> _bingoRoomCollection;

        public BingoRoomService(ILogger<BingoRoomService> logger)
        {
            _logger = logger;
            var mongoClient = new MongoClient(
                BingoRoomDatabaseSettings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                BingoRoomDatabaseSettings.DatabaseName);

            _bingoRoomCollection = mongoDatabase.GetCollection<BingoRoom>(
                BingoRoomDatabaseSettings.BingoRoomsCollectionName);
        }

        public async Task<List<BingoRoom>> GetAsync() {
            try 
            {
                return await _bingoRoomCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex){
                _logger.LogError(ex, "An error occurred.");
                return null;
            }
        }

        public async Task<BingoRoom?> GetAsync(string id)
        {
            try
            {
                return await _bingoRoomCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                return null;
            }
        }

        public async Task CreateAsync(BingoRoom newBingoRoom)
        {
            try
            {
                await _bingoRoomCollection.InsertOneAsync(newBingoRoom);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
            }
        }

        public async Task UpdateAsync(string id, BingoRoom updatedBingoRoom)
        {
            try
            {
                await _bingoRoomCollection.ReplaceOneAsync(x => x.Id == id, updatedBingoRoom);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
            }
        }

        public async Task RemoveAsync(string id)
        {
            try
            {
                await _bingoRoomCollection.DeleteOneAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
            }
        }
    }
}
