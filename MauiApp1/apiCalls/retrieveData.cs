using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1.apiCalls
{
    internal class retrieveData
    {
        public readonly HttpClient _httpClient;
        public retrieveData()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://192.168.1.17:5000/") };
        }
        public async Task<userDetails?> retrieveUserData(string id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Users/{id}");

            if (!response.IsSuccessStatusCode)
                return null; // or handle error

            // Read response as JSON and deserialize
            string json = await response.Content.ReadAsStringAsync();
            userDetails? user = JsonSerializer.Deserialize<userDetails>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true   
            });

            return user;
        }

    }
}
