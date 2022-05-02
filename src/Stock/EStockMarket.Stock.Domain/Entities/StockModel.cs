namespace EStockMarket.Stock.Domain.Entities;
public class StockModel
{
    public Guid Id { get; set; }

    public Guid CompanyId { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;
}
