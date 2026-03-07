using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using models.Dtos;
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

        private bool CanClick= true;
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
                var clientId = "Ov23liCktt04rNqSpZg7";
                var redirectUri = "com.virtualpet://oauth2redirect";
                var scope = "read:user user:email";

                var authUrl = new Uri($"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUri}&scope={scope}");

                var callbackUrl = new Uri(redirectUri);

                var result = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

                canClick = false;
                LoginBtnText = "Processing...";


                if (result.Properties.TryGetValue("code", out var code))
                {
                    Debug.WriteLine("code is: ", code);
                    GitHubCodeDto dto = new GitHubCodeDto();
                    dto.code = code;
                    // Exchange code for tokens
                   var user = await _authApiService.PostCode(dto);
                   if (user == null )
                    {
                        await Application.Current.Windows[0].Page.DisplayAlert("Error", "Failed to get access token from GitHub.", "OK");
                        return;
                    }
                   
                    var person = new UserEmailDto { Email = user.Email };
                    Debug.WriteLine("user email is: ", user.Email);

                    UserAuthResponseDto check = await _authApiService.GetTokenAsync(person);

                   if (check == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Backend did not return an auth token. See logs for details.", "OK");
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
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Asasd", "OK");

                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }

        }
     

    }
    }
