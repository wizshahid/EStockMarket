using EStockMarket.Stock.Domain.Entities;
using EStockMarket.Stock.Domain.Interfaces;
using EStockMarket.Stock.Infra.Data.Context;
using System.Linq.Expressions;

namespace EStockMarket.Stock.Infra.Data.Repository;
internal class StockRepository : IStockRepository
{
    private readonly StockDbContext dbContext;

    public StockRepository(StockDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public int Add(StockModel item)
    {
        dbContext.Add(item);
        return dbContext.SaveChanges();
    }

    public int DeleteRange(IEnumerable<StockModel> items)
    {
        dbContext.RemoveRange(items);
        return dbContext.SaveChanges();
    }

    public IQueryable<StockModel> GetAll(Expression<Func<StockModel, bool>> expression)
    {
        return dbContext.Stocks.Where(expression);
    }
}
