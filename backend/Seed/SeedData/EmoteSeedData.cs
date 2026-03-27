using models.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Seed.SeedData
{
    public static class EmoteSeedData
    {
        public static List<EmoteSeedEntity> Data = new()
        {
            new EmoteSeedEntity
            {
                Pet_Type = "Duck",
                Icon= "xyz.png",
                Animation = "yay.anim",
                Unlock_Level = 5
            }
        };
    }
}
