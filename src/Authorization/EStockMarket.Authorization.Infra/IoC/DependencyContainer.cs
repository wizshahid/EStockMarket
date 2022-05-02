using EStockMarket.Authorization.Application.Interfaces;
using EStockMarket.Authorization.Application.Services;
using EStockMarket.Authorization.Domain.Interfaces;
using EStockMarket.Authorization.Infra.Data.Mongo;
using EStockMarket.Authorization.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EStockMarket.Authorization.Infra.IoC;
public static class DependencyContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<MongoConnection>();
        return services;
    }
}
