using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Entities
{
    public class LevelSeedEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int Level { get; set; }
        public int RequiredXp { get; set; }
    }
}
