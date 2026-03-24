using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using models.Dtos.PetDtos;
using models.Entities;
using MongoDB.Driver;
using System.Security.Claims;
using WebApplication1.services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        public PetService _petService { get; set; }

        public PetController(PetService petService)
        {
            _petService = petService;
        }

        [HttpPost]
        [Authorize]
        public async Task postPetInfo([FromBody] PetInfoDto dto)
        {
            try
            {
                Console.WriteLine("Entered ");
                var UserIdVal = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var UserId = Guid.Parse(UserIdVal);
                await _petService.PostingPetData(dto, UserId);
                Console.WriteLine("exikt");
            }
            catch (Exception ex)
            {
                Console.WriteLine("this is an error", ex.ToString());
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<PetNameDto> getPetInfo()
        {
            try
            {
                var UserIdVal = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var UserId = Guid.Parse(UserIdVal);
                Console.WriteLine("getPetiNFO");
                return await _petService.RetrievingPetInfo(UserId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("get pet ",ex.ToString());
                return null;
            }
        }
    }
}
