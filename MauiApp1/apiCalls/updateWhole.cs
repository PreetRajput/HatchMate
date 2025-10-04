using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.apiCalls
{
    internal class updateWhole
    {
        public readonly HttpClient _httpClient;
        public updateWhole()
        {
            _httpClient = new HttpClient{BaseAddress= new Uri("http://192.168.1.17:5000/")};
            
        }
        public async Task<bool> updateDocument(userDetails user)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/api/Users/{user.id}", user);
            return response.IsSuccessStatusCode;
        }
    }
}
