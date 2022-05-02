namespace EStockMarket.Stock.Application.Models.Request;
public class CreateStockRequest
{
    public Guid CompanyId { get; set; }

    public decimal Price { get; set; }
}
