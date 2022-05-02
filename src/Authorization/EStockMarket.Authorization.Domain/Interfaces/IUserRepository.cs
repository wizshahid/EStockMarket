using EStockMarket.Authorization.Domain.Entities;

namespace EStockMarket.Authorization.Domain.Interfaces;
public interface IUserRepository
{
    Task<User> GetUser(string username);

    Task<string> AddUser(User user);

    Task<List<User>> GetAll();
}