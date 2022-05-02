using EStockMarket.Stock.Application.Models.Request;

namespace EStockMarket.Stock.Application.Models.Response;
public class StockResponse : CreateStockRequest
{
    public Guid Id { get; set; }

    public DateTime CreatedOn { get; set; }
}
