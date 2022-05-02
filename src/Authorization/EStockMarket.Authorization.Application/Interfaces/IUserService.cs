using EStockMarket.Authorization.Application.Models.Request;
using EStockMarket.Authorization.Application.Models.Response;

namespace EStockMarket.Authorization.Application.Interfaces;
public interface IUserService
{
    Task<AuthResponse> Login(LoginRequest model);
    Task SeedUser();
}
