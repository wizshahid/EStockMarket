using EStockMarket.Stock.Application.AutoMapper;
using EStockMarket.Stock.Application.Interfaces;
using EStockMarket.Stock.Application.Services;
using EStockMarket.Stock.Domain.Interfaces;
using EStockMarket.Stock.Infra.Data.Context;
using EStockMarket.Stock.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EStockMarket.Stock.Infra.IoC;
public static class DependencyContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Infra layer dependencies
        services.AddDbContext<StockDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("StockDbConnection"));
        });
        services.AddScoped<IStockRepository, StockRepository>();

        //Application layer dependencies
        services.AddScoped<IStockService, StockService>();

        services.AddAutoMapper(typeof(IAutoMapperMarker));

        return services;
    }
}
