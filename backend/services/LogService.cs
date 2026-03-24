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
    }
}
