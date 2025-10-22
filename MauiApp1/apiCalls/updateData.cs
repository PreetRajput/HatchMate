using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.apiCalls
{
    internal class updateData
    {
       public readonly HttpClient _httpClient;

        public updateData()
        {

            _httpClient = new HttpClient { BaseAddress = new Uri("http://192.168.1.5:5000/") };


        }

        public async Task<bool> updateUserInfo(userDetails user)
        {
            HttpResponseMessage response = await _httpClient.PatchAsJsonAsync($"api/Users/{user.id}", user);
            return response.IsSuccessStatusCode;
        }

    }
}
