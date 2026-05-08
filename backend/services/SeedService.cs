using AutoMapper;
using models.Dtos.EmoteSeedDtos;
using models.Dtos.PetDtos;
using models.Entities;
using MongoDB.Driver;

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
        public async Task<List<EmoteInfoDto>> GetEmotes(PetDto dto )
        {
            try
            {
                var filter = Builders<EmoteSeedEntity>.Filter.And(
                    Builders<EmoteSeedEntity>.Filter.Lte(t => t.Unlock_Level, dto.Pet_Level),
                    Builders<EmoteSeedEntity>.Filter.Eq(t => t.Pet_Type, dto.Pet_Type));
                List<EmoteInfoDto> dtoList = new List<EmoteInfoDto>();
                var listOfEmotes = await _emotes.Find(filter).ToListAsync();
                foreach (var emote in listOfEmotes)
                {
                   EmoteInfoDto EmoteInfoDto =  _mapper.Map<EmoteInfoDto>(emote);
                    dtoList.Add(EmoteInfoDto);
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
