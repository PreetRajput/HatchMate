using CommunityToolkit.Maui.Views;
using MauiApp1.BaseClass;
using MauiApp1.Interfaces;
using models.Dtos.GitHubDtos;
using models.Dtos.UserDtos;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;
        private IPopupService _popUp;
        public AuthApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://192.168.1.4:5000/") };
            _popUp = AppService.GetService<IPopupService>();
        }

        public async Task<UserAuthResponseDto?> GetTokenAsync(UserEmailDto user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/github-login", user);
                var content = await response.Content.ReadFromJsonAsync<UserAuthResponseDto>();
                if (!response.IsSuccessStatusCode)
                {
                    await _popUp.OpenUserAddedPopUp("Unhandled Exception", $"[AuthApiService] GetTokenAsync getToken one failed: {(int)response.StatusCode} {content}");
                    return null;
                }

                return content;
            }
            catch (Exception ex)
            {
                await _popUp.OpenUserAddedPopUp("Unhandled Exception", ex.ToString());
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
                await _popUp.OpenUserAddedPopUp("Unhandled Exception", ex.ToString());
                return null;
            }
        }
    }
}
