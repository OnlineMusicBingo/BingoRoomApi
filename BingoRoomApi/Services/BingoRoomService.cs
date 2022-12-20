using BingoRoomApi.Data;
using BingoRoomApi.Interfaces;
using BingoRoomApi.Models;
using BingoRoomApi.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BingoRoomApi.Services
{
    public class BingoRoomService : IBingoRoomService
    {
        private readonly IBingoRoomRepository _bingoRoomRepository;

        public BingoRoomService(IBingoRoomRepository bingoRoomRepository)
        {
            _bingoRoomRepository = bingoRoomRepository;
        }

        public async Task<List<BingoRoom>> GetAsync() =>
            await _bingoRoomRepository.GetAsync();

        public async Task<BingoRoom?> GetAsync(string id) =>
            await _bingoRoomRepository.GetAsync(id);

        public async Task CreateAsync(BingoRoom newBingoRoom) =>
            await _bingoRoomRepository.CreateAsync(newBingoRoom);

        public async Task UpdateAsync(string id, BingoRoom updatedBingoRoom) =>
            await _bingoRoomRepository.UpdateAsync(id, updatedBingoRoom);

        public async Task RemoveAsync(string id) =>
            await _bingoRoomRepository.RemoveAsync(id);
    }
}
