using models.Entities;
using MongoDB.Driver;
using WebApplication1.Seed.SeedData;
using WebApplication1.services;

namespace WebApplication1.Seed
{
    public class EmoteSeed
    {
        public readonly IMongoCollection<EmoteSeedEntity> _emotes;
        public EmoteSeed(MongoDBService mongo)
        {
            _emotes = mongo.GetCollection<EmoteSeedEntity>("EmoteDetails");
        }
        public async Task EmoteSeedAsync()
        {
            foreach(var emote in EmoteSeedData.Data)
            {
                var filter = Builders<EmoteSeedEntity>.Filter.Eq(t=> t.Pet_Type, emote.Pet_Type);
                var checkIfExist = await _emotes.Find(filter).AnyAsync();
                if (!checkIfExist)
                {
                    await _emotes.InsertOneAsync(emote);
                }
            }

        }
    }
}
