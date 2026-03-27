using AutoMapper;
using models.Dtos.EmoteSeedDtos;
using models.Entities;
using MongoDB.Driver;
using WebApplication1.services;

namespace WebApplication1.services
{
    public class SeedService
    {
        public readonly IMongoCollection<EmoteSeedEntity> _emotes;
        public IMapper _mapper;

        public SeedService(MongoDBService mongo, IMapper mapper)
        {
            _emotes = mongo.GetCollection<EmoteSeedEntity>("EmoteDetails");
            _mapper = mapper;
        }
        public async Task<List<EmoteInfoDto>> GetEmotes(int petLevel, string pet_type )
        {
            try
            {

                var filter = Builders<EmoteSeedEntity>.Filter.And(
                    Builders<EmoteSeedEntity>.Filter.Lte(t => t.Unlock_Level, petLevel),
                    Builders<EmoteSeedEntity>.Filter.Eq(t => t.Pet_Type, pet_type));
                List<EmoteInfoDto> dtoList = new List<EmoteInfoDto>();
                var listOfEmotes = await _emotes.Find(filter).ToListAsync();
                foreach (var emote in listOfEmotes)
                {
                   EmoteInfoDto dto =  _mapper.Map<EmoteInfoDto>(emote);
                    dtoList.Add(dto);
                }
                return dtoList;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
