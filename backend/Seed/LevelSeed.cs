using models.Entities;
using MongoDB.Driver;
using WebApplication1.Seed.SeedData;
using WebApplication1.services;

namespace WebApplication1.Seed
{
    public class LevelSeed
    {
        public IMongoCollection<LevelSeedEntity> _level;
        public LevelSeed(MongoDBService _mongo)
        {
            _level = _mongo.GetCollection<LevelSeedEntity>("LevelDetails");
        }
        public async Task LevelAsync()
        {
            try
            {

                foreach (var level in LevelSeedData.Data)
                {
                    var filter = Builders<LevelSeedEntity>.Filter.Eq(x => x.Level, level.Level);
                    var checkIfExist = await (await _level.FindAsync(filter)).AnyAsync();
                    if (!checkIfExist)
                    {
                        await _level.InsertOneAsync(level);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("aasdasdas"+ex.ToString());
            }
        }
    }
}
