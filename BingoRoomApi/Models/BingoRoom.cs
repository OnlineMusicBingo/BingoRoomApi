using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BingoRoomApi.Models
{
    public class BingoRoom
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string BingoRoomName { get; set; } = null!;

        public string? OwnerId { get; set; }

        public string? Participants { get; set; } = null!;

        public DateTime StartDateTime { get; set; }

        public string MusicGenre { get; set; } = null!;

        public string MusicListUrl { get; set; } = null!;

    }
}
