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
                Pet_Type = "DragonWarrior",
                Icon= "pets/DragonWarrior/walk/walk_03.png",
                Animation = [
                    "pets/DragonWarrior/walk/walk_01.png",
                    "pets/DragonWarrior/walk/walk_02.png",
                    "pets/DragonWarrior/walk/walk_03.png",
                    "pets/DragonWarrior/walk/walk_04.png",
                    "pets/DragonWarrior/walk/walk_05.png",
                    "pets/DragonWarrior/walk/walk_06.png",
                    ],
                Unlock_Level = 0
            },
              new EmoteSeedEntity
            {
                Pet_Type = "DragonWarrior",
                Icon= "pets/DragonWarrior/die/die_005.png",
                Animation = [
                    "pets/DragonWarrior/die/die_001.png",
                    "pets/DragonWarrior/die/die_002.png",
                    "pets/DragonWarrior/die/die_003.png",
                    "pets/DragonWarrior/die/die_004.png",
                    "pets/DragonWarrior/die/die_005.png",
                    "pets/DragonWarrior/die/die_006.png",
                    "pets/DragonWarrior/die/die_007.png",
                    "pets/DragonWarrior/die/die_008.png",
                    "pets/DragonWarrior/die/die_009.png",
                    "pets/DragonWarrior/die/die_010.png",
                    ],
                Unlock_Level = 5
            },
              new EmoteSeedEntity
            {
                Pet_Type = "DragonWarrior",
                Icon= "pets/DragonWarrior/dizzy/dizzy_03.png",
                Animation = [
                    "pets/DragonWarrior/dizzy/dizzy_01.png",
                    "pets/DragonWarrior/dizzy/dizzy_02.png",
                    "pets/DragonWarrior/dizzy/dizzy_03.png",
                    ],
                Unlock_Level = 10
            },
            new EmoteSeedEntity
            {
                Pet_Type = "DragonWarrior",
                Icon= "pets/DragonWarrior/strike/strike_03.png",
                Animation = [
                    "pets/DragonWarrior/strike/strike_01.png",
                    "pets/DragonWarrior/strike/strike_02.png",
                    "pets/DragonWarrior/strike/strike_03.png",
                    "pets/DragonWarrior/strike/strike_04.png",
                    "pets/DragonWarrior/strike/strike_05.png",
                    ],
                Unlock_Level = 15
            },
             new EmoteSeedEntity
            {
                Pet_Type = "cow",
                Icon= "pets/cow/cute/c2.png",
                Animation = [
                    "pets/cow/cute/c1.png",
                    "pets/cow/cute/c2.png",
                    "pets/cow/cute/c3.png",
                    "pets/cow/cute/c4.png",
                    ],
                Unlock_Level = 0
            },
              new EmoteSeedEntity
            {
                Pet_Type = "cow",
                Icon= "pets/cow/feed/f3.png",
                Animation = [
                    "pets/cow/feed/f1.png",
                    "pets/cow/feed/f2.png",
                    "pets/cow/feed/f3.png",
                    "pets/cow/feed/f4.png",
                    ],
                Unlock_Level = 5
            },
               new EmoteSeedEntity
            {
                Pet_Type = "cow",
                Icon= "pets/cow/sleep/s2.png",
                Animation = [
                    "pets/cow/sleep/s1.png",
                    "pets/cow/sleep/s2.png",
                    ],
                Unlock_Level = 10
            },
                new EmoteSeedEntity
            {
                Pet_Type = "cow",
                Icon= "pets/cow/walk/w3.png",
                Animation = [
                    "pets/cow/walk/w1.png",
                    "pets/cow/walk/w2.png",
                    "pets/cow/walk/w3.png",
                    "pets/cow/walk/w4.png",
                    ],
                Unlock_Level = 15
            },


        };
    }
}
