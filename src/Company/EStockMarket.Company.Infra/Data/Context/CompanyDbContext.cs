using EStockMarket.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EStockMarket.Company.Infra.Data.Context;
internal class CompanyDbContext : DbContext
{
    public CompanyDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public DbSet<CompanyModel> Companies { get; set; } = null!;
}
