using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EStockMarket.Authorization.Infra.Data.Mongo;
    internal class MongoConnection
    {
        private readonly IMongoDatabase _mongoDatabase;
        public MongoConnection(IOptions<MongoDatabaseSetting> dbSetting)
        {

            var mongoClient = new MongoClient(
               dbSetting.Value.ConnectionString);

            _mongoDatabase = mongoClient.GetDatabase(
                            dbSetting.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _mongoDatabase.GetCollection<T>(collectionName);
        }
    }
