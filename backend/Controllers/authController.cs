using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using models.Dtos.GitHubDtos;
using models.Dtos.UserDtos;
using models.Entities;
using MongoDB.Driver;
using WebApplication1.services;

[ApiController]
[AllowAnonymous]
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
    public GitHubConfigDto GetGitHubConfig()
    {
        Console.WriteLine("abasbcascb");

        return new GitHubConfigDto
        {
            ClientId = _gitHubService.GetClientId(),
            RedirectUri = _gitHubService.GetRedirectUri(),
            Scope = _gitHubService.GetScope(),
            AuthUrl = _gitHubService.GetAuthorizationUrl()
        };
    }

    [HttpPost("github-login")]
    public async Task<UserAuthResponseDto?> GitHubLogin([FromBody] UserEmailDto user)
    {
        UserAuthResponseDto? response = await _userService.UpsertAndAuthenticate(user);
        if (response != null)
            return response;

        return null;
    }

    [HttpPost("githubCollect")]
    public async Task<UserInfoDto?> gettinSomeEmail([FromBody] GitHubCodeDto dto)
    {
        try
        {
            Console.WriteLine("Processing GitHub code exchange");
            GitHubTokenDto? token = await _gitHubService.ExchangeCodeForTokenAsync(dto);
            UserInfoDto? response = await _gitHubService.ExchangeTokenForInfo(token);
            if (response != null) return response;
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in gettinSomeEmail: " + ex.Message);
            throw;
        }
    }
}
