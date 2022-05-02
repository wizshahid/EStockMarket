using EStockMarket.Stock.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EStockMarket.Stock.Infra.Data.Context;
internal class StockDbContext : DbContext
{
    public StockDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<StockModel> Stocks { get; set; } = null!;
}
