using AutoMapper;
using models.Entities;
using MongoDB.Driver;

namespace WebApplication1.services
{
    public class LogService
    {
        public IMongoCollection<LogEntity> _log;
        public IMapper _mapper;
        public LogService(IMapper mapper, MongoDBService mongo)
        {
            _log = mongo.GetCollection<LogEntity>("LogDetails");
            _mapper = mapper;
        }

        public async Task GetLog(Guid UserId, Guid TaskId)
        {
            var Filter = Builders<LogEntity>.Filter.And(
                Builders<LogEntity>.Filter.Eq(t=> t.TaskId, TaskId),
                Builders<LogEntity>.Filter.Eq(t=> t.UserId, UserId)
            );

            var result= await _log.Find(Filter).ToListAsync();

        }
    }
}
