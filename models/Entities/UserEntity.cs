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

        [BsonElement("PetName")]
        public string? PetName { get; set; }

        [BsonElement("PetNum")]
        public int PetNum { get; set; }


    }
}
