using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Entities
{
    public class LogEntity
    {

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid TaskId { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserId { get; set; }

        [BsonElement("ExpGranted")]
        public int ExpGranted { get ; set; }

        [BsonElement("CompletedAt")]
        public DateTime ExpGrantedDate { get; set; }
    }
}
