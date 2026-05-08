using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using models.Dtos.GitHubDtos;
using models.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class loginPageViewModel: ObservableObject
    {
        [ObservableProperty]
        string loginBtnText = "Login";

        private bool CanClick = true;
        public bool canClick 
        { 
            get
            {
                return CanClick;
            }
            set
            {
                SetProperty(ref CanClick, value);
            }
        }

        private readonly AuthApiService _authApiService;
        private readonly ApiService _apiService;

        public loginPageViewModel(AuthApiService authApiService, ApiService apiService)
        {
            _authApiService = authApiService;
            _apiService = apiService;
        }

        [RelayCommand(CanExecute = nameof(canClick))]
        public async Task loginClicked()
        {
            try
            {
                // Fetch GitHub OAuth config from backend
                var config = await _apiService.GetAsync();
                
                if (config == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to load authentication configuration.", "OK");
                    return;
                }

                var authUrl = new Uri(config.AuthUrl);
                var callbackUrl = new Uri(config.RedirectUri);

                var result = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

                canClick = false;
                LoginBtnText = "Processing...";

                if (result.Properties.TryGetValue("code", out var code))
                {
                    Debug.WriteLine($"Authorization code received: {code}");
                    
                    GitHubCodeDto dto = new GitHubCodeDto { code = code };
                    
                    // Exchange code for tokens via backend
                    var user = await _authApiService.PostCode(dto);
                    if (user == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Failed to get access token from GitHub.", "OK");
                        canClick = true;
                        LoginBtnText = "Login";
                        return;
                    }
                   
                    var person = new UserEmailDto { Email = user.Email };
                    Debug.WriteLine($"User email: {user.Email}");

                    UserAuthResponseDto check = await _authApiService.GetTokenAsync(person);

                    if (check == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Backend did not return an auth token. See logs for details.", "OK");
                        canClick = true;
                        LoginBtnText = "Login";
                        return;
                    }

                    if (!string.IsNullOrEmpty(check.Token))
                    {
                        _apiService.SetToken(check.Token);
                    }

                    if (check.IsNewUser)
                    {
                        var page = ((App)Application.Current).Services.GetRequiredService<SignupPage>();
                        await Application.Current.MainPage.Navigation.PushAsync(page);
                    }
                    else
                    {
                        Application.Current.MainPage = new AppShell();
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // User cancelled authentication
                Debug.WriteLine("User cancelled authentication");
                canClick = true;
                LoginBtnText = "Login";
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Authentication failed: {ex.Message}", "OK");
                Debug.WriteLine($"Login error: {ex}");
                canClick = true;
                LoginBtnText = "Login";
            }
        }
    }
    }
