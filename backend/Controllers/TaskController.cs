using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using models.Dtos.TaskDtos;
using System.Security.Claims;
using WebApplication1.services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        public readonly TaskService _taskService;
        public readonly UserService _userService;
        public TaskController(TaskService TaskService, UserService UserService)
        {
            _taskService = TaskService;
            _userService = UserService;
        }

        [HttpPost]
        [Authorize]
        public async Task<List<TaskItemDto>> AddTasksToDB([FromBody] TaskListDto dto)
        {
            var userIdVal = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userIdVal, out var userId))
            {
                return await _taskService.AddTask(dto, userId);
            }
            return null;
        }
        [HttpGet]
        [Authorize]
        public async Task<List<TaskItemDto>> GetInCompleteTasksFromDB()
        {
            Console.WriteLine("entered tasks");
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.Parse(userIdValue);
            var res = await _taskService.RetrieveInCompleteTasks(userId);
            return res;
        }
        [HttpGet("{skip}, {take}")]
        [Authorize]
        public async Task<TaskElementDto> GetSomecompletedTask(int skip, int take)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.Parse(userIdValue);
            var res = await _taskService.RetrieveSomeCompletedTasks(userId, skip, take);
            Console.WriteLine(res);
            return res;
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTaskFromDB(Guid id)
        {
            Console.WriteLine("dlete task ");
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdValue == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdValue);

            var deleted = await _taskService.DeleteTask(userId, id);

            if (deleted.DeletedCount==0)
                return NotFound();

            return NoContent();
        }
        [HttpPatch]
        [Authorize]
        public async Task<int> markTaskAsCompleted([FromBody] TasksIdDto dto)
        {
            try
            {
                    Console.WriteLine("yayyy");
                var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = Guid.Parse(userIdValue);
                return await _taskService.UpdateTask(dto, userId);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"errrorrrrr is sssss {ex.Message}");
                throw;
            }

        }
     }
}
