using AutoFixture;
using BingoRoomApi.Controllers;
using BingoRoomApi.Interfaces;
using BingoRoomApi.Models;
using FluentAssertions;
using Moq;
using MongoDB.Bson;

namespace BingoRoomApi.Tests.IntegrationTests
{
    public class BingoRoomApiTest
    {
        private readonly IFixture iFixture;
        private readonly Mock<IBingoRoomService> bingoRoomServiceMock;
        private readonly BingoRoomController systemUnderTest;

        public BingoRoomApiTest()
        {
            iFixture = new Fixture();
            bingoRoomServiceMock = iFixture.Freeze<Mock<IBingoRoomService>>();
            systemUnderTest = new BingoRoomController(bingoRoomServiceMock.Object);
        }

        [Fact]
        public async Task GetBingoRoomByIdTest()
        {
            //Arrange
            var bingoroomDataMock = iFixture.Create<BingoRoom>();//creating a bingoroom
            var id = iFixture.Create<Guid>();

            //Act
            var result = await systemUnderTest.Get(id.ToString()).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            bingoRoomServiceMock.Verify(x => x.GetAsync(id.ToString()), Times.Once());
        }

        [Fact]
        public async Task GetBingoRoomListTest()
        {
            //Arrange
            var bingoroomDataMock = iFixture.Create<List<BingoRoom>>();//creating a bingoroom list

            //Act
            var result = await systemUnderTest.Get().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            bingoRoomServiceMock.Verify(x => x.GetAsync(), Times.Once());

        }

        [Fact]
        public async Task CreateBingoRoomWithoutUserIdTest()
        {
            //Arrange
            var bingoRoomId = ObjectId.GenerateNewId().ToString();
            BingoRoom bingoRoom = new BingoRoom{ Id = bingoRoomId, BingoRoomName = "Test_BingoRoom", OwnerId = "ownerid", Participants = "participants", StartDateTime = DateTime.Parse("2022-12-12T12:12Z"), MusicGenre = "musicGenre", MusicListUrl = "musicListUrl" };

            //Act
            var result = await systemUnderTest.Post(bingoRoom).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            bingoRoomServiceMock.Verify(x => x.CreateAsync(bingoRoom), Times.Never());
        }

        [Fact]
        public async Task DeleteBingoRoomTest()
        {
            //Arrange
            var bingoRoomId = ObjectId.GenerateNewId().ToString();

            //Act
            var result = await systemUnderTest.Delete(bingoRoomId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            bingoRoomServiceMock.Verify(x => x.RemoveAsync(bingoRoomId), Times.Never());
        }

        [Fact]
        public async Task UpdateBingoRoomTest()
        {
            //Arrange
            var bingoRoomId = ObjectId.GenerateNewId().ToString();
            BingoRoom bingoRoom = new BingoRoom { Id = bingoRoomId, BingoRoomName = "Test_BingoRoom", OwnerId = "ownerid", Participants = "participants", StartDateTime = DateTime.Parse("2022-12-12T12:12Z"), MusicGenre = "musicGenre", MusicListUrl = "musicListUrl" };

            //Act
            var result = await systemUnderTest.Update(bingoRoomId, bingoRoom);

            //Assert
            result.Should().NotBeNull();
            bingoRoomServiceMock.Verify(x => x.UpdateAsync(bingoRoomId, bingoRoom), Times.Never());
        }
    }
}
