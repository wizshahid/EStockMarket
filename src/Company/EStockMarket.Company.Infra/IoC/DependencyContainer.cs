using EStockMarket.Company.Application.AutoMapper;
using EStockMarket.Company.Application.Interfaces;
using EStockMarket.Company.Application.Services;
using EStockMarket.Company.Domain.Interfaces;
using EStockMarket.Company.Infra.Data.Context;
using EStockMarket.Company.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EStockMarket.Company.Infra.IoC;
public static class DependencyContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Infra layer dependencies
        services.AddDbContext<CompanyDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CompanyDbConnection"));
        });
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        //Application layer dependencies
        services.AddScoped<ICompanyService, CompanyService>();

        services.AddAutoMapper(typeof(IAutoMapperMarker));

        return services;
    }
}
