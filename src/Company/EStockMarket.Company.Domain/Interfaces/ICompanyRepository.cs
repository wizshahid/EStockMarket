using EStockMarket.Company.Domain.Entities;
using System.Linq.Expressions;

namespace EStockMarket.Company.Domain.Interfaces;
public interface ICompanyRepository
{
    int Add(CompanyModel company);

    IQueryable<CompanyModel> GetAll();

    CompanyModel? GetById(Guid id);

    int Delete(Guid id);    

    int Update(CompanyModel company);

    bool Any(Expression<Func<CompanyModel, bool>> func);
}
