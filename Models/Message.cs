using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApp.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("SenderId")]
        public string SenderId { get; set; }

        [BsonElement("ReceiverId")]
        public string? ReceiverId { get; set; }

        [BsonElement("Content")]
        public string Content { get; set; }

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [BsonElement("IsPrivate")]
        public bool IsPrivate { get; set; }
    }
}