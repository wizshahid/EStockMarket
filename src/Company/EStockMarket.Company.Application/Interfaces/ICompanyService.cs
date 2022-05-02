using EStockMarket.Company.Application.Models.Request;
using EStockMarket.Company.Application.Models.Response;

namespace EStockMarket.Company.Application.Interfaces;
public interface ICompanyService
{
    CompanyResponse Add(CreateCompanyRequest companyRequest);

    CompanyResponse Update(UpdateCompanyRequest companyRequest);
    
    CompanyResponse GetItem(Guid id);

    void Delete(Guid id);

    List<CompanyResponse> GetAll();

    void UpdateLatestStockPrice(Guid id, decimal price);
}
