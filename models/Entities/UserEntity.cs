using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace models.Entities
{
    public class UserEntity
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        [BsonElement("Email")]
        public string? Email { get; set; }

        [BsonElement("Username")]
        public string? Username { get; set; }

        [BsonElement("TotalTaskXp")]
        public int TotalTaskXp { get; set; }

        [BsonElement("TodayXp")]
        public int TodayXp { get; set; }


    }
}
