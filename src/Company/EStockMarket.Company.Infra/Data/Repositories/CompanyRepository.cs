using EStockMarket.Company.Domain.Entities;
using EStockMarket.Company.Domain.Interfaces;
using EStockMarket.Company.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EStockMarket.Company.Infra.Data.Repositories;
internal class CompanyRepository : ICompanyRepository
{
    private readonly CompanyDbContext dbContext;

    public CompanyRepository(CompanyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public int Add(CompanyModel company)
    {
        dbContext.Add(company);
        return dbContext.SaveChanges();
    }

    public bool Any(Expression<Func<CompanyModel, bool>> func)
    {
        return dbContext.Companies.Any(func);
    }

    public int Delete(Guid id)
    {
        dbContext.Remove(new CompanyModel
        {
            Id = id
        });
        return dbContext.SaveChanges();
    }

    public IQueryable<CompanyModel> GetAll()
    {
        return dbContext.Companies;
    }

    public CompanyModel? GetById(Guid id)
    {
        return dbContext.Companies.Find(id);
    }

    public int Update(CompanyModel company)
    {
        dbContext.Entry(company).State = EntityState.Modified;
        return dbContext.SaveChanges();
    }
}
