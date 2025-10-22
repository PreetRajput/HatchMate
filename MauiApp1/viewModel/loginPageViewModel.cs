using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.apiCalls;
using MauiApp1.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
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
        public bool canClick { 
            get
            {
                return CanClick;
            }
            set
            {
                SetProperty(ref CanClick, value);
            }
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
                    // Exchange code for tokens
                    var tokens = await ExchangeCodeForTokenAsync(code);
                    var accessToken = tokens["access_token"];

                    // Fetch user info from GitHub
                    using var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("MauiApp1"); // GitHub requires a User-Agent

                    var userJson = await client.GetStringAsync("https://api.github.com/user");
                    var user = JsonSerializer.Deserialize<UserInfo>(userJson);


                    var apiForRetrieve = new retrieveData();
                    var id = user.email;

                    userDetails check = await apiForRetrieve.retrieveUserData(id);
                    Preferences.Set("email", user.email);
                    Preferences.Set("isLoggedIn", true);


                    if (check != null)
                    {
                        detailsPage.currentUser = check;
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        var api = new postData();

                        var person = new userDetails
                        {
                            id = $"{user.email}",
                        };

                        bool success = await api.addUserInfo(person);

                        if (success)
                            await Application.Current.MainPage.DisplayAlert("Success", "User added", "OK");
                        else
                            await Application.Current.MainPage.DisplayAlert("Error", "Failed to add user", "OK");

                        await Application.Current.MainPage.Navigation.PushAsync(new SignupPage(user.email));
                    }




                    // tokens will have ID token, access token, refresh token
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Asasd", "OK");

                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }

        }
      public async Task<Dictionary<string, string>> ExchangeCodeForTokenAsync(string code)
        {
            using var client = new HttpClient();

            var parameters = new Dictionary<string, string>
            {
                { "client_id", "Ov23liCktt04rNqSpZg7" },
                { "client_secret", "3873f3b84a0ab26b5906965278def5ae08a83fe6" },
                { "code", code },
                { "redirect_uri", "com.virtualpet://oauth2redirect" }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://github.com/login/oauth/access_token")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var tokens = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            return tokens;
        }

    }
    }
