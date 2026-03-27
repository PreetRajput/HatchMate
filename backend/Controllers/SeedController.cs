using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using models.Dtos.EmoteSeedDtos;
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

        [HttpGet]
        [Authorize]
        public async Task<List<EmoteInfoDto>> Get([FromBody] int pet_level, [FromBody] string pet_type)
        {
            try
            {
                return await _seedService.GetEmotes(pet_level, pet_type);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                return null;
            }
        }


    }
}
