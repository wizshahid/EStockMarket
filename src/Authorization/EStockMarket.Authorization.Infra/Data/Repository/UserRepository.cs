using EStockMarket.Authorization.Domain.Entities;
using EStockMarket.Authorization.Domain.Interfaces;
using EStockMarket.Authorization.Infra.Data.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EStockMarket.Authorization.Infra.Data.Repository;
internal class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;
    public UserRepository(MongoConnection mongoConnection)
    {
        _collection = mongoConnection.GetCollection<User>("Users");
    }

    public async Task<string> AddUser(User user)
    {
        user.Id = ObjectId.GenerateNewId().ToString();
        await _collection.InsertOneAsync(user);
        return user.Id;
    }

    public async Task<User> GetUser(string username)
    {
        return await _collection.Find(x => x.Username.ToLower() == username.ToLower()).FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetAll()
    {
        return await _collection.Find(x => true).ToListAsync();
    }
}
