using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using models.Dtos.UserDtos;
using models.Dtos.GitHubDtos;

namespace MauiApp1.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://192.168.1.4:5000/") };

        }

        public async Task<UserAuthResponseDto?> GetTokenAsync(UserEmailDto user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/github-login", user);
                var content = await response.Content.ReadFromJsonAsync<UserAuthResponseDto>();
                if (!response.IsSuccessStatusCode)
                {
                  Debug.WriteLine($"[AuthApiService] GetTokenAsync getToken one failed: {(int)response.StatusCode} {content}");
                    return null;
                }

                return content;
            }
            catch (Exception ex)
            {
               Debug.WriteLine($"getToken Exception: {ex}");
                return null;
            }
        }
    public async Task<UserInfoDto?> PostCode(GitHubCodeDto code)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/githubCollect", code);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[AuthApiService] GetTokenAsync postcode one failed: {(int)response.StatusCode}");
                    return null;
                }
                var content = await response.Content.ReadFromJsonAsync<UserInfoDto>();
                return content;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"postCode api Exception: {ex}");
                return null;
            }
        }
    }
}
