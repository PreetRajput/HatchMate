using models.Dtos;
using System.Net.Http.Headers;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.services
{
    public class GitHubService
    {
        public readonly IConfiguration _config;
        public readonly HttpClient client;
        public GitHubService(IConfiguration config)
        {
            _config = config;
            client = new HttpClient();
        }
        public async Task<GitHubTokenDto?> ExchangeCodeForTokenAsync(GitHubCodeDto dto)
        {
            try
            {


                var parameters = new Dictionary<string, string>
                {
                    { "client_id", "Ov23liCktt04rNqSpZg7" },
                    { "client_secret", "3873f3b84a0ab26b5906965278def5ae08a83fe6" },
                    { "code", dto.code  },
                    { "redirect_uri", "com.virtualpet://oauth2redirect" }
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://github.com/login/oauth/access_token")
                {
                    Content = new FormUrlEncodedContent(parameters)
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }

                var raw = await response.Content.ReadAsStringAsync();
                Console.WriteLine(raw);
                var tokens = await response.Content.ReadFromJsonAsync<GitHubTokenDto>();
                Console.WriteLine($"token is {tokens.access_token}");
                 return tokens;
            }
            catch(Exception e)
            {

                    Console.Write(e.ToString());
                    Console.Write("code CRASH ");
                    Console.Write($"Error: {e.Message}");
                    return null;
            }
        }
        public async Task<UserInfoDto?> ExchangeTokenForInfo(GitHubTokenDto dto)
        {
            try
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", dto.access_token);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("MauiApp1");

                var userJson = await client.GetStringAsync("https://api.github.com/user");
                Console.WriteLine(userJson);
                var user = JsonSerializer.Deserialize<UserInfoDto>(userJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                Console.WriteLine(user.Email);
                return user;
            }
            catch (Exception e)
            {
                Console.Write("token to info crashmna");
                Console.WriteLine(e.ToString());
                return null;
            }

        }
    }
}
