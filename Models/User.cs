using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApp.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("PasswordHash")]
        public string PasswordHash { get; set; }

        [BsonElement("ConnectionId")]
        public string? ConnectionId { get; set; }
    }
}