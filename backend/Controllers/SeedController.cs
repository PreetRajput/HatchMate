using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using models.Dtos.EmoteSeedDtos;
using models.Dtos.PetDtos;
using models.Entities;
using MongoDB.Driver;
using WebApplication1.services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController :ControllerBase
    {
        public readonly SeedService _seedService;
        SeedController(SeedService seedService)
        {
            _seedService = seedService;
        }

        [HttpPost]
        [Authorize]
        public async Task<List<EmoteInfoDto>> post([FromBody] PetDto Dto)
        {
            try
            {
                return await _seedService.GetEmotes(Dto);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                return null;
            }
        }


    }
}
