using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using System.Security.Authentication;

namespace WebApplication1.services
{
    public class MongoDBService
    {

        private readonly IMongoDatabase _database;
        public MongoDBService(IConfiguration config)
        {
            var connectionUri = config.GetConnectionString("MongoDb");
            var databaseName = config["MongoDbName"];
            Console.WriteLine(databaseName);

            if (string.IsNullOrWhiteSpace(connectionUri))
            {
                throw new InvalidOperationException("MongoDb connection string is not configured. Check your connection string in appsettings or environment variables.");
            }

            var settings = MongoClientSettings.FromConnectionString(connectionUri);

            // Use the Stable Server API (if required by your deployment)
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Force TLS 1.2 (helps when remote host requires TLS and prevents protocol negotiation issues)
            settings.SslSettings = new SslSettings
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };

            // Make server selection timeout explicit so failures surface faster during development
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(15);
            settings.ConnectTimeout = TimeSpan.FromSeconds(10);

            var client = new MongoClient(settings);

            try
            {
                _database = client.GetDatabase(databaseName);
                Console.WriteLine(_database);

                // Try a ping to validate connectivity and surface errors early
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>("{ ping: 1 }");
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("MongoDB connection failed. See exception details below:");
                Console.WriteLine(ex.ToString());
                // Rethrow so the host can fail fast, or comment the next line if you want the app to continue without DB.
                throw;
            }
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

    }
}
