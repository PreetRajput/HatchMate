using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using models.Dtos.GitHubDtos;
using models.Dtos.UserDtos;
using models.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.services;

[ApiController]
[Route("api/[controller]")]
public class authController : ControllerBase
{
    private readonly IMongoCollection<UserEntity> _users;
    private readonly IMapper _mapper;
    private readonly UserService _userService;
    private readonly GitHubService _gitHubService;
    
    public authController(MongoDBService mongoService, IMapper mapper, UserService userService, GitHubService gitHubService)
    {
        try
        {
            _users = mongoService.GetCollection<UserEntity>("UserDetails");
            _mapper = mapper;
            _userService = userService;
            _gitHubService = gitHubService;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Constructor failed: " + ex.Message);
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }

    [HttpGet("github-config")]
    public IActionResult GetGitHubConfig()
    {
        return Ok(new
        {
            ClientId = _gitHubService.GetClientId(),
            RedirectUri = _gitHubService.GetRedirectUri(),
            Scope = _gitHubService.GetScope(),
            AuthUrl = _gitHubService.GetAuthorizationUrl()
        });
    }

    [HttpPost("github-login")]
    public async Task<IActionResult> GitHubLogin([FromBody] UserEmailDto user)
    {
        UserAuthResponseDto response = await _userService.UpsertAndAuthenticate(user);
        if (response != null)
            return Ok(response);

        return Unauthorized();
    }
    
    [HttpPost("githubCollect")] 
    public async Task<UserInfoDto> gettinSomeEmail([FromBody] GitHubCodeDto dto)
    {
        Console.WriteLine("Processing GitHub code exchange");
        GitHubTokenDto token = await _gitHubService.ExchangeCodeForTokenAsync(dto);
        UserInfoDto response = await _gitHubService.ExchangeTokenForInfo(token);
        return response;
    }
}
