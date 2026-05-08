using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Entities
{
    public class EmoteSeedEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Pet_Type")]
        public string Pet_Type { get; set; }

        [BsonElement("Icon")]
        public string Icon { get; set; }

        [BsonElement("Animation")]
        public List<string> Animation = new();
        
        [BsonElement("Unlock_Level")]
        public int Unlock_Level { get; set; }
    }
}
