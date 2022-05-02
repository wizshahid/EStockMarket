using EStockMarket.Stock.Application.Models.Request;
using EStockMarket.Stock.Application.Models.Response;

namespace EStockMarket.Stock.Application.Interfaces;
public interface IStockService
{
    StockResponse Add(CreateStockRequest item);

    List<StockResponse> GetAll(Guid companyId, DateTime startDate, DateTime endDate);

    StockKPI GetKPIs(Guid companyId, DateTime startDate, DateTime endDate);

    void DeleteCompanyStocks(Guid companyId);
}
