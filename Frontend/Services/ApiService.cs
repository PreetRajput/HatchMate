using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using models.Dtos;

namespace MauiApp1.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private string? _token;
        private const string AuthKey = "auth_token";

        public ApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://192.168.1.39:5000/") };
        }

        public async Task<bool> InitializeFromStorageAsync()
        {
            var token = await SecureStorage.GetAsync(AuthKey);
            if (!string.IsNullOrEmpty(token))
            {
                SetToken(token);
                return true;
            }
            return false;
        }

        public void SetToken(string token)
        {
            _token = token?.Trim().Trim('"');
            if (!string.IsNullOrEmpty(_token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                _ = SecureStorage.SetAsync(AuthKey, _token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        // GetPet
        public async Task<PetNameDto?> GetPetAsync()
        {
            var response = await _httpClient.GetAsync("api/pet");
            var body = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"[ApiService] GetPetAsync -> {(int)response.StatusCode} {body}");
            if (!response.IsSuccessStatusCode) return null;
            return JsonSerializer.Deserialize<PetNameDto>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // Post user
        public async Task<bool> PostUserAsync(UserAppRelatedInfoDto user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users", user);
            return response.IsSuccessStatusCode;
        }

        // Post task
        public async Task<List<TaskItemDto>?> PostTaskAsync(TaskListDto task)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/task", task);
                var body = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode) return null;
                return JsonSerializer.Deserialize<List<TaskItemDto>>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("postTaskASYNC", ex.ToString());
                return null;
            }


        }

        // Retrieve user tasks
        public async Task<List<TaskItemDto>?> RetrieveUserTasksAsync()
        {
            try
            {
                var res=  await _httpClient.GetFromJsonAsync<List<TaskItemDto>>("api/task");
                Debug.WriteLine("preeeet", res);
                return res;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("apicall exception", ex.ToString());
                return null;
            }
        }
        public async Task<bool> UpdateTaskToCompletedAsync(TasksIdDto dto, bool TrueIfCompleted)
        {
            try
            {

                var response = await _httpClient.PatchAsJsonAsync($"api/task/{TrueIfCompleted}", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error might be:", ex.ToString());
                return false;
            }

        }

        public async Task<bool> PostPetAsync(PetInfoDto pet)
        {
            try
            {
                Debug.WriteLine("postPetAsync");
                var response = await _httpClient.PostAsJsonAsync("api/pet", pet);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error yeh hai", ex.Message);
                return false;
            }

        }
        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var res = await _httpClient.DeleteAsync($"api/task/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
