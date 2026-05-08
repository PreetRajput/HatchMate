using models.Dtos.GitHubDtos;
using models.Dtos.UserDtos;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebApplication1.services
{
    public class GitHubService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;
        private readonly string _clientSecret;
        private readonly string _clientId;
        private readonly string _redirectUri;

        public GitHubService(IConfiguration config)
        {
            _config = config;
            _client = new HttpClient();
            
            // Load configuration values
            _clientId = _config["GitHub:ClientId"] ?? throw new InvalidOperationException("GitHub:ClientId is not configured");
            _clientSecret = _config["GitHub:ClientSecret"] ?? throw new InvalidOperationException("GitHub:ClientSecret is not configured");
            _redirectUri = _config["GitHub:RedirectUri"] ?? throw new InvalidOperationException("GitHub:RedirectUri is not configured");
        }

        public string GetAuthorizationUrl()
        {
            var scope = _config["GitHub:Scope"] ?? "read:user user:email";
            return $"https://github.com/login/oauth/authorize?client_id={_clientId}&redirect_uri={_redirectUri}&scope={scope}";
        }

        public string GetClientId() => _clientId;
        public string GetRedirectUri() => _redirectUri;
        public string GetScope() => _config["GitHub:Scope"] ?? "read:user user:email";

        public async Task<GitHubTokenDto?> ExchangeCodeForTokenAsync(GitHubCodeDto dto)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "client_id", _clientId },
                    { "client_secret", _clientSecret },
                    { "code", dto.code },
                    { "redirect_uri", _redirectUri }
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://github.com/login/oauth/access_token")
                {
                    Content = new FormUrlEncodedContent(parameters)
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"GitHub token exchange failed: {error}");
                    return null;
                }
                var tokens = await response.Content.ReadFromJsonAsync<GitHubTokenDto>();
                Console.WriteLine($"Access token received: {tokens?.access_token}");
                return tokens;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error exchanging code for token: {e}");
                return null;
            }
        }

        public async Task<UserInfoDto?> ExchangeTokenForInfo(GitHubTokenDto dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", dto.access_token);
                _client.DefaultRequestHeaders.UserAgent.ParseAdd("MauiApp1");

                var userJson = await _client.GetStringAsync("https://api.github.com/user");
                Console.WriteLine($"GitHub user info: {userJson}");
                
                var user = JsonSerializer.Deserialize<UserInfoDto>(userJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                Console.WriteLine($"User email: {user?.Email}");
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting user info from token: {e}");
                return null;
            }
        }
    }
}
