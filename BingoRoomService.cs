﻿using BingoRoomApi.Models;
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

        public async Task<List<BingoRoom>> GetAsync() =>
            await _bingoRoomCollection.Find(_ => true).ToListAsync();

        public async Task<BingoRoom?> GetAsync(string id) =>
            await _bingoRoomCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(BingoRoom newBingoRoom) =>
            await _bingoRoomCollection.InsertOneAsync(newBingoRoom);

        public async Task UpdateAsync(string id, BingoRoom updatedBingoRoom)=>
            await _bingoRoomCollection.ReplaceOneAsync(x => x.Id == id, updatedBingoRoom);

        public async Task RemoveAsync(string id) => 
            await _bingoRoomCollection.DeleteOneAsync(x => x.Id == id);
    }
}
