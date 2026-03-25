using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Entities
{
    public class TaskEntity
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        
        
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserId { get; set; }


        [BsonElement("isCompleted")]
        public bool IsCompleted { get; set; }


        [BsonElement("Task")]
        public string? Task { get; set; }

        [BsonElement("IsGranted")]

        public bool IsGranted { get; set; }
    }
}
