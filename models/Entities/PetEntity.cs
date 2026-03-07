using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Entities
{
    public class PetEntity
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]

        public Guid UserId { get; set; }

        [BsonElement("PetName")]
        public string? PetName { get; set; }

        [BsonElement("PetNum")]

        public int PetNum { get; set; }
    }
}
