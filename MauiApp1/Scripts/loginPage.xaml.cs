using Microsoft.Maui.Authentication;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using MauiApp1.Scripts;
using MauiApp1.apiCalls;
namespace MauiApp1;

public partial class loginPage : ContentPage
{
	public loginPage()
	{
		InitializeComponent();
	}
    public async void loginClicked(object sender, EventArgs e)
    {
        try
        {
            var clientId = "Ov23liCktt04rNqSpZg7";
            var redirectUri = "com.virtualpet://oauth2redirect";
            var scope = "read:user user:email";

            var authUrl = new Uri($"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUri}&scope={scope}");

            var callbackUrl = new Uri(redirectUri);

            var result = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

            // result.Properties["code"] will contain the authorization code


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

                var api = new postData();

                var person = new userDetails
                {
                    id = $"{user.email}",
                };

                bool success = await api.addUserInfo(person);

                if (success)
                    await DisplayAlert("Success", "User added", "OK");
                else
                    await DisplayAlert("Error", "Failed to add user", "OK");


                // Now you have user info (login, email, name, etc.)
                await Navigation.PushAsync(new SignupPage(user.email));
                // tokens will have ID token, access token, refresh token
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Asasd", "OK");

            await DisplayAlert("Error", ex.ToString(), "OK");
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