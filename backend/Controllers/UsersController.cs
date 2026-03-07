using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using models.Dtos;
using models.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public readonly TaskService _taskService;
        public UsersController(IMapper mapper, UserService userService, TaskService taskService)
        {
            Console.WriteLine("Constructor START");
            try
            {
                _mapper = mapper;
                _userService = userService;
                _taskService = taskService;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Constructor failed: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }

            _taskService = taskService;
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserAppRelatedInfoDto dto)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            Console.WriteLine($"UserId from token: {userIdValue}");
            if (userIdValue == null || email == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdValue);
            Console.WriteLine($"guid: {userId}");

            await _userService.CreateOneEntity(dto, email, userId);
            
            return Ok();
        }

        [Authorize]
        [HttpGet("pet")]
        public async Task<PetNameDto?> GetPet()
        {
            Console.Write("getpet run");
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(string.IsNullOrEmpty(userIdValue))
            {
                Console.WriteLine("no token valid");
                return null;
            }

            var userId = Guid.Parse(userIdValue);

            var pet = await _userService.GetPetName(userId);

                Console.WriteLine("uyuguyg", pet);
            return pet;
           
        }
    }
}

