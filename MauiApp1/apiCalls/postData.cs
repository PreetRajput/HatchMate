using System.Net.Http;
using System.Net.Http.Json; // for PostAsJsonAsync
using System.Threading.Tasks;

namespace MauiApp1.apiCalls
{
    internal class postData
    {
        public readonly HttpClient _httpClient;
        public postData()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://192.168.1.5:5000/") };
        }
        public async Task<bool> addUserInfo(userDetails user)
        {
            HttpResponseMessage response = await  _httpClient.PostAsJsonAsync("api/Users",user);
            return response.IsSuccessStatusCode;
        }

    }
}
