using EStockMarket.Stock.Domain.Entities;
using System.Linq.Expressions;

namespace EStockMarket.Stock.Domain.Interfaces;
public interface IStockRepository
{
    int Add (StockModel item);

    int DeleteRange(IEnumerable<StockModel> items);    

    IQueryable<StockModel> GetAll(Expression<Func<StockModel, bool>> expression);
}
