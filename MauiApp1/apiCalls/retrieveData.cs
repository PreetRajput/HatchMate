using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<bool> retrieveUserData(userDetails user)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Users/{user.id}");
            return response.IsSuccessStatusCode;
        }
    }
}
