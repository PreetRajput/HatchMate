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
        private readonly IAuthClient _authClient;
        private readonly ISeedClient _seedClient;
        private readonly IPetClient _petClient;
        private readonly IUsersClient _usersClient;
        private readonly ITaskClient _taskClient;
        private string? _token;
        private const string AuthKey = "auth_token";

        public ApiService(IAuthClient authClient, IPetClient petClient, IUsersClient usersClient, ITaskClient taskClient, ISeedClient seedClient)
        {
            _authClient = authClient;
            _seedClient = seedClient;
            _petClient = petClient;
            _usersClient = usersClient;
            _taskClient = taskClient;
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
            return !string.IsNullOrEmpty(_token);
        }

        // GetPet
        public Task<PetInfoDto?> GetPetAsync()
        {
            return _petClient.GetPetInfoAsync();
        }
        public Task<ICollection<EmoteInfoDto>?> GetAnimationAsync(PetDto dto)
        {
            return _seedClient.PostAsync(dto);
        }

        // Post user
        public Task PostUserAsync(UserAppRelatedInfoDto user)
        {
            return _usersClient.CreateAsync(user);
        }

        // Post task
        public Task<ICollection<TaskItemDto>?> PostTaskAsync(TaskListDto task)
        {
            return _taskClient.AddTasksToDBAsync(task);
        }

        // Retrieve user tasks
        public Task<ICollection<TaskItemDto>?> RetrieveUserTasksAsync()
        {
            return _taskClient.GetTasksFromDBAsync();
        }
        public Task<int> UpdateTaskToCompletedAsync(TasksIdDto dto)
        {
            return _taskClient.MarkTaskAsCompletedAsync(dto);
        }
        public Task<GitHubConfigDto?> GetAsync()
        {
            return _authClient.GetGitHubConfigAsync();
        }
        public Task PostPetAsync(PetInfoDto pet)
        {
            return _petClient.PostPetInfoAsync(pet);
        }
        public Task DeleteTaskAsync(Guid id)
        {
            return _taskClient.DeleteTaskFromDBAsync(id);
        }
    

    }
}
