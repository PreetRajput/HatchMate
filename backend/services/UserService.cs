using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using models.Dtos;
using models.Entities;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace WebApplication1.services // Replace with your Service project namespace
{
    public class UserService
    {
        // Dependencies (JWT service, Mapper, and MongoDB collection)
        private readonly JWTservice _jwt;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<UserEntity> _users;

        public UserService(JWTservice jwt, IMapper mapper, MongoDBService mongo)
        {
            _jwt = jwt;
            _users = mongo.GetCollection<UserEntity>("UserDetails"); 
            _mapper = mapper;
        }

        public async Task<UserAuthResponseDto> UpsertAndAuthenticate(UserEmailDto userEmailDto)
        {
            var filter = Builders<UserEntity>.Filter.Eq(u => u.Email, userEmailDto.Email);
            var userEntity = await _users.Find(filter).FirstOrDefaultAsync();
            var isNewUser = false;
            if (userEntity == null)
            {
                isNewUser = true;
                Console.WriteLine("new user");
                userEntity = _mapper.Map<UserEntity>(source: userEmailDto);

                userEntity.Id = Guid.NewGuid();
                
                var newtoken = _jwt.GenerateToken(userEntity.Id, userEntity.Email);

                return new UserAuthResponseDto
                {
                    Token = newtoken,
                    IsNewUser = isNewUser,
                };
            }

            Console.WriteLine("not a new user");

            isNewUser = false;
            
            var token = _jwt.GenerateToken(userEntity.Id, userEntity.Email);

            return new UserAuthResponseDto
            {
                Token = token,
                IsNewUser = isNewUser,
            };
        }

        public async Task CreateOneEntity(UserAppRelatedInfoDto dto, string email, Guid userId)
        {

            try
            {

                var entity = _mapper.Map<UserEntity>(dto);
                entity.Id = userId;
                entity.Email = email;

                 await _users.InsertOneAsync(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return; 
            }
        }
        public async Task<PetNameDto> GetPetName(Guid userId)
        {
            Console.WriteLine("userservice run");
            var Filter = Builders<UserEntity>.Filter.Eq(u => u.Id, userId);
            var userInfo = await _users.Find(Filter).FirstOrDefaultAsync();
            if (userInfo == null)
            {
                return null;
            }
            Console.WriteLine(userInfo);

            var pet = _mapper.Map<PetNameDto>(userInfo);
            Console.WriteLine(pet.PetName);
            Console.WriteLine(pet);
            return pet;
        }

        // --- Helper Function ---
    }
}