using AutoMapper;
using models.Dtos.TaskDtos;
using models.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication1.services
{
    public class TaskService
    {
        private readonly IMongoCollection<TaskEntity> _tasks;
        private readonly IMongoCollection<LogEntity> _logs;
        private readonly IMongoCollection<UserEntity> _users;
        private readonly IMongoCollection<LevelSeedEntity> _level;
        private readonly IMapper _mapper;
        
        public TaskService(MongoDBService mongo, IMapper mapper)
        {
            _tasks = mongo.GetCollection<TaskEntity>("TaskDetails");
            _logs = mongo.GetCollection<LogEntity>("LogDetails");
            _users = mongo.GetCollection<UserEntity>("UserDetails");
            _level = mongo.GetCollection<LevelSeedEntity>("LevelDetails");
            _mapper = mapper;
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
        public async Task<int> UpdateTask(TasksIdDto dto, Guid userId)
        {
            try
            {
                UpdateDefinition<TaskEntity> update;
                foreach (var item in dto.TaskIds)
                {
                    var filter = Builders<TaskEntity>.Filter.And(
                            Builders<TaskEntity>.Filter.Eq(t => t.UserId, userId),
                            Builders<TaskEntity>.Filter.Eq(t => t.Id, item)
                        );

                    update = Builders<TaskEntity>.Update.Set(u => u.IsCompleted, dto.IsCompleted);
                    var result = await _tasks.UpdateOneAsync(filter, update);

                    var filterToCheckIfLogExist = Builders<LogEntity>.Filter.Eq(t => t.TaskId, item);
                    await UpdateLog(filterToCheckIfLogExist, userId, item);
                }
                    return await UpdatePetLevelAsync(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }
        public async Task UpdateLog(FilterDefinition<LogEntity> filterToCheckIfLogExist, Guid userId, Guid item)
        {
            try
            {
                var TrueIfExist = await _logs.Find(filterToCheckIfLogExist).AnyAsync();
            if (!TrueIfExist)
                {
                    LogEntity logEntity = new LogEntity();
                    logEntity.UserId = userId;
                    logEntity.TaskId = item;
                    Guid Id = Guid.NewGuid();
                    logEntity.Id = Id;
                    logEntity.ExpGrantedDate = DateTime.UtcNow.Date;
                    logEntity.ExpGranted = 10;
                    await _logs.InsertOneAsync(logEntity);
                    await UpdateXp(userId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ummm Error: {ex}");
            }
        }
        public async Task UpdateXp(Guid userId)
        {
            var filterForCheck = Builders<UserEntity>.Filter.Eq(t => t.Id, userId);
            var currentDate = DateTime.UtcNow.Date;
            var updates = new PipelineUpdateDefinition<UserEntity>(new[]
                  {
                                new BsonDocument("$set", new BsonDocument
                                {
                                    { "TodayXp",

                                        new BsonDocument("$cond", new BsonArray
                                        {
                                            new BsonDocument("$ne", new  BsonArray {"$LastExpGrantedDate", currentDate}),
                                            10,
                                            new BsonDocument("$min", new BsonArray {200,
                                                new BsonDocument("$add", new BsonArray
                                                {
                                                    "$TodayXp" , 10
                                                })
                                            })
                                        })
                                     },
                                      {

                                    "LastExpGrantedDate",  currentDate
                                    },
                                    {
                                        "TotalTaskXp", new BsonDocument("$add", new BsonArray
                                        {
                                            "$TotalTaskXp", new BsonDocument("$max",
                                            new BsonArray
                                            { 0,
                                            new BsonDocument("$min",
                                                    new BsonArray{10,
                                                        new BsonDocument("$subtract",
                                                            new BsonArray{200 ,
                                                                "$TodayXp"})
                                                                  })
                                            })
                                        })
                                    },

                                })
                            }

              );
            await _users.UpdateOneAsync(filterForCheck, updates);
        }
        public async Task<int> UpdatePetLevelAsync(Guid userId)
        {
            var filterForXp = Builders<UserEntity>.Filter.Eq(t=> t.Id, userId);
            var UserTotalXp = await _users.Find(filterForXp).Project(x => x.TotalTaskXp).FirstOrDefaultAsync();
            return await _level.Find(x => x.RequiredXp <= UserTotalXp).SortByDescending(x => x.RequiredXp).Project(x=> x.Level).FirstOrDefaultAsync();
        }
        public async Task<DeleteResult> DeleteTask(Guid userId, Guid id)
        {
            var filter = Builders<TaskEntity>.Filter.And(
                Builders<TaskEntity>.Filter.Eq(t => t.UserId, userId),
                Builders<TaskEntity>.Filter.Eq(u => u.Id, id)
                );
           return await _tasks.DeleteOneAsync(filter);
        }

        public async Task<List<TaskItemDto>> RetrieveInCompleteTasks(Guid userId)
        {
            try
            {
                var filter = Builders<TaskEntity>.Filter.And(
                    Builders<TaskEntity>.Filter.Eq(t=> t.UserId, userId),
                    Builders<TaskEntity>.Filter.Eq(t => t.IsCompleted, false)
                );
                var response = await _tasks.Find(filter).ToListAsync();
                List<TaskItemDto> taskItem = new List<TaskItemDto>();
                foreach (var entity in response)
                {
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
        public async Task<TaskElementDto> RetrieveSomeCompletedTasks(Guid userId, int skip, int take)
        {
            try
            {
                var filter = Builders<TaskEntity>.Filter.And(
                            Builders<TaskEntity>.Filter.Eq(t=> t.UserId, userId),
                            Builders<TaskEntity>.Filter.Eq(t=> t.IsCompleted, true)
                            );
                var totalCount = await _tasks.CountDocumentsAsync(filter);
                var LoadSomeTasks = await _tasks.Find(filter)
                                           .SortByDescending(x => x.CreatedAt)
                                           .Skip(skip)
                                           .Limit(take)
                                           .ToListAsync();
                List<TaskItemDto> taskItem = new List<TaskItemDto>();
                foreach (var item in LoadSomeTasks)
                {
                    taskItem.Add(_mapper.Map<TaskItemDto>(item));
                }
                TaskElementDto response = new TaskElementDto();
                response.HasMoreTask= ((skip+take) > totalCount)?false: true;
                response.Tasks = taskItem;
                return response;
            }
            catch (Exception ex )
            {
                Console.WriteLine($"Could not find {userId}");
                Console.WriteLine(ex.ToString());
                return new TaskElementDto();
            }
        }
    }
}
