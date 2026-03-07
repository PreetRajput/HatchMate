using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using models.Dtos;
using models.Entities;
using MongoDB.Driver;

namespace WebApplication1.services
{
    public class PetService
    {
        public IMongoCollection<PetEntity> _pet;
        public IMapper _mapper;
        public PetService(MongoDBService mongo, IMapper mapper)
        {
            _pet = mongo.GetCollection<PetEntity>("PetDetails");
            _mapper = mapper;
        }
        public async Task PostingPetData(PetInfoDto dto, Guid UserId)
        {
            try
            {
                var PetEntity = _mapper.Map<PetEntity>(dto);
                var PetId = Guid.NewGuid();
                PetEntity.Id = PetId;
                PetEntity.UserId = UserId;
                Console.WriteLine(PetEntity.Id +"huhuhhuhu"+ PetEntity.UserId);
                await _pet.InsertOneAsync(PetEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task<PetNameDto> RetrievingPetInfo(Guid Id)
        {
            try
            {
                var filter = Builders<PetEntity>.Filter.Where(x => x.UserId == Id);
                var res= await _pet.Find(filter).FirstOrDefaultAsync();
                if (res == null)
                    return null;
                return _mapper.Map<PetNameDto>(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" get pet error {ex.Message}");
                return null;
            }
        }

    }
}
