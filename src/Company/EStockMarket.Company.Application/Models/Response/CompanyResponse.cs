using EStockMarket.Company.Application.Models.Request;

namespace EStockMarket.Company.Application.Models.Response;
public class CompanyResponse : UpdateCompanyRequest
{
    public decimal LatestStockPrice { get; set; }
}
