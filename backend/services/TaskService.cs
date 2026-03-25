using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using models.Dtos.LogDtos;
using models.Dtos.TaskDtos;
using models.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace WebApplication1.services
{
    public class TaskService
    {
        private readonly IMongoCollection<TaskEntity> _tasks;
        private readonly IMongoCollection<LogEntity> _logs;
        private readonly IMongoCollection<UserEntity> _users;
        private readonly IMapper _mapper;
        
        public TaskService(MongoDBService mongo, IMapper mapper)
        {
            _tasks = mongo.GetCollection<TaskEntity>("TaskDetails");
            _mapper = mapper;
            _logs = mongo.GetCollection<LogEntity>("LogDetails");
            _users = mongo.GetCollection<UserEntity>("UserDetails");
        }
        public async Task<List<TaskItemDto>> AddTask(TaskListDto dto, Guid userId)
        {
            try
            {
                List<TaskItemDto> Tasks = new();
                foreach (var task in dto.Tasks)
                {
                    TaskEntity newTask = new TaskEntity();
                    newTask.UserId = userId;
                    Guid newTaskId = Guid.NewGuid();
                    newTask.Id = newTaskId;
                    newTask.Task = task;
                    await _tasks.InsertOneAsync(newTask);
                    TaskItemDto taskItem = _mapper.Map<TaskItemDto>(newTask);
                    Tasks.Add(taskItem);
                    Console.WriteLine("ummm okayyyy");
                }
                return Tasks;

            }
            catch (Exception ex)
            {
                Console.WriteLine("addTask error");
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task UpdateTask(TasksIdDto dto, Guid userId, bool TrueIfCompleted)
        {
            try
            {
                UpdateDefinition<TaskEntity> update;
                    Console.WriteLine("im NOT in the loop yayyy");
                foreach(var item in dto.TaskIds)
                {
                    Console.WriteLine("im in the loop yayyy");

                    var filter = Builders<TaskEntity>.Filter.And(
                            Builders<TaskEntity>.Filter.Eq(t => t.UserId, userId),
                            Builders<TaskEntity>.Filter.Eq(t => t.Id, item),
                             Builders<TaskEntity>.Filter.Eq(t => t.IsCompleted, false)
                        );
                
                    update = Builders<TaskEntity>.Update.Set(u => u.IsCompleted, true);
                    var result = await _tasks.UpdateOneAsync(filter, update);
                    if (result.MatchedCount == 0)
                        continue;
                    Console.WriteLine("entered the uodatedTasks field");
                        try
                        {
                            
                                LogEntity logEntity = new LogEntity();
                                logEntity.UserId = userId;
                                logEntity.TaskId = item;
                                Guid Id = Guid.NewGuid();
                                logEntity.Id = Id;
                                logEntity.ExpGranted = 10;
                                await _logs.InsertOneAsync(logEntity);

                            UpdateDefinition<UserEntity> updateUser;
                            var filterForUser = Builders<UserEntity>.Filter.And(
                                Builders<UserEntity>.Filter.Eq(t => t.Id, userId),
                                Builders<UserEntity>.Filter.Lt(t => t.TodayXp, 200)
                                );
                                
                            
                            updateUser = Builders<UserEntity>.Update
                                                .Inc(t => t.TotalTaskXp, 10)
                                                .Inc(t=> t.TodayXp, 10);
                            await  _users.UpdateOneAsync(filterForUser, updateUser);
                        }
                        catch (Exception ex)
                        {
                                Console.WriteLine($"Error: {ex}");
                        }


                    }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public async Task<DeleteResult> DeleteTask(Guid userId, Guid id)
        {
            var filter = Builders<TaskEntity>.Filter.And(
                Builders<TaskEntity>.Filter.Eq(t => t.UserId, userId),
                Builders<TaskEntity>.Filter.Eq(u => u.Id, id)
                );
           return await _tasks.DeleteOneAsync(filter);
        }

        public async Task<List<TaskItemDto>> RetrieveTasks(Guid userId)
        {
            try
            {
                Console.WriteLine("preeet");

                var filter = Builders<TaskEntity>.Filter.Eq(t => t.UserId, userId);
                var response = await _tasks.Find(filter).ToListAsync();
                List<TaskItemDto> taskItem = new List<TaskItemDto>();
                foreach (var entity in response)
                {
                    Console.WriteLine("huh");
                   taskItem.Add(_mapper.Map<TaskItemDto>(entity));
                }
                return taskItem; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not find {userId}");
                Console.WriteLine(ex.ToString());
                return new List<TaskItemDto>(); 
            }
        }
    }
}
