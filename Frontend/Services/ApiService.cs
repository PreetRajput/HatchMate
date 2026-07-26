using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using HatchMate.Api;

namespace MauiApp1.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthClient _authClient;
        private readonly IPetClient _petClient;
        private readonly IUsersClient _usersClient;
        private readonly ITaskClient _taskClient;
        private string? _token;
        private const string AuthKey = "auth_token";

        public ApiService(IAuthClient authClient, IPetClient petClient, IUsersClient usersClient, ITaskClient taskClient)
        {
            _authClient = authClient;
            _petClient = petClient;
            _usersClient = usersClient;
            _taskClient = taskClient;
            _httpClient = new HttpClient { BaseAddress = new Uri("http://192.168.1.4:5000/") };
        }

        //to retrieve the token taken from secure storage 
        public async Task<bool> InitializeFromStorageAsync()
        {
            var token = await SecureStorage.GetAsync(AuthKey);
            return  SetToken(token);
        }
        //to check the validity of token taken from secure storage 
        public bool SetToken(string? token)
        {
            _token = token?.Trim().Trim('"');
            if (!string.IsNullOrEmpty(_token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                _ = SecureStorage.SetAsync(AuthKey, _token);
                return true;
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
                return false;
            }
        }

        // GetPet
        public async Task<PetInfoDto?> GetPetAsync()
        {

            var response = await _httpClient.GetAsync("api/pet");
            var body = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"[ApiService] GetPetAsync -> {(int)response.StatusCode} {body}");
            if (!response.IsSuccessStatusCode) return null;
            return JsonSerializer.Deserialize<PetInfoDto>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task<List<EmoteInfoDto>?> GetAnimationAsync(PetDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/seed", dto);
            var body = await response.Content.ReadAsStringAsync();
            if(!response.IsSuccessStatusCode) return null;
            return JsonSerializer.Deserialize<List<EmoteInfoDto>>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<EmoteInfoDto>();
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
                return res;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("apicall exception", ex.ToString());
                return null;
            }
        }
        public async Task<int?> UpdateTaskToCompletedAsync(TasksIdDto dto)
        {
            try
            {

                var response = await _httpClient.PatchAsJsonAsync($"api/task", dto);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<int?>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error might be:", ex.ToString());
                throw;
            }

        }
        public async Task<GitHubConfigDto?> GetAsync()
        {
            try
            {
               return await _authClient.GetGitHubConfigAsync();
            }
            catch (Exception ex)
            {
                return null;
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
