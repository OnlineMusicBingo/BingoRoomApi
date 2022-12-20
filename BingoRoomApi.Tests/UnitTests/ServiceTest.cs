using Moq;
using BingoRoomApi.Services;
using BingoRoomApi.Interfaces;
using BingoRoomApi.Models;
using MongoDB.Bson;

namespace BingoRoomApi.Tests.UnitTests
{
    public class ServiceTest
    {
        private IBingoRoomService _service;
        private Mock<IBingoRoomRepository> _mockRepository;

        public ServiceTest()
        {
            _mockRepository = new Mock<IBingoRoomRepository>();
            _service = new BingoRoomService(_mockRepository.Object);
        }

        [Fact]
        public async Task TestGetAsyncAll()
        {
            // Arrange
            var bingoRoomId = ObjectId.GenerateNewId().ToString();
            var bingoRoomList = new List<BingoRoom>
            {
                new BingoRoom { Id = bingoRoomId, BingoRoomName = "Test_BingoRoom", OwnerId = "ownerid", Participants = "participants", StartDateTime = DateTime.Parse("2022-12-12T12:12Z"), MusicGenre = "musicGenre", MusicListUrl = "musicListUrl" }
            };

            _mockRepository.Setup(x => x.GetAsync())
                .ReturnsAsync(bingoRoomList);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.Equal(bingoRoomList, result);
        }

        [Fact]
        public async Task TestGetAsync()
        {
            // Arrange
            var bingoRoomId = ObjectId.GenerateNewId().ToString();
            var bingoRoom = new BingoRoom { Id = bingoRoomId, BingoRoomName = "Test_BingoRoom", OwnerId = "ownerid", Participants = "participants", StartDateTime = DateTime.Parse("2022-12-12T12:12Z"), MusicGenre = "musicGenre", MusicListUrl = "musicListUrl" }; 

            _mockRepository.Setup(x => x.GetAsync(bingoRoomId))
                .ReturnsAsync(bingoRoom);

            // Act
            var result = await _service.GetAsync(bingoRoomId);

            // Assert
            Assert.Equal(bingoRoom, result);
        }

        [Fact]
        public async void TestCreateAsync()
        {
            // Arrange
            var bingoRoomId = ObjectId.GenerateNewId().ToString();
            var bingoRoom = new BingoRoom { Id = bingoRoomId, BingoRoomName = "Test_BingoRoom", OwnerId = "ownerid", Participants = "participants", StartDateTime = DateTime.Parse("2022-12-12T12:12Z"), MusicGenre = "musicGenre", MusicListUrl = "musicListUrl" };

            _mockRepository.Setup(x => x.CreateAsync(bingoRoom))
                .Returns(Task.CompletedTask);

            // Act
            await _service.CreateAsync(bingoRoom);

            // Assert
            _mockRepository.Verify(x => x.CreateAsync(bingoRoom), Times.Once);
        }

        [Fact]
        public async void TestUpdateAsync()
        {
            // Arrange
            var bingoRoomId = ObjectId.GenerateNewId().ToString();
            var bingoRoom = new BingoRoom { Id = bingoRoomId, BingoRoomName = "Test_BingoRoom", OwnerId = "ownerid", Participants = "participants", StartDateTime = DateTime.Parse("2022-12-12T12:12Z"), MusicGenre = "musicGenre", MusicListUrl = "musicListUrl" };

            _mockRepository.Setup(x => x.UpdateAsync(bingoRoomId, bingoRoom))
                .Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(bingoRoomId, bingoRoom);

            // Assert
            _mockRepository.Verify(x => x.UpdateAsync(bingoRoomId, bingoRoom), Times.Once);
        }

        [Fact]
        public async void TestDeleteAsync()
        {
            // Arrange
            var bingoRoomId = ObjectId.GenerateNewId().ToString();

            _mockRepository.Setup(x => x.RemoveAsync(bingoRoomId))
                .Returns(Task.CompletedTask);

            // Act
            await _service.RemoveAsync(bingoRoomId);

            // Assert
            _mockRepository.Verify(x => x.RemoveAsync(bingoRoomId), Times.Once);
        }
    }
}