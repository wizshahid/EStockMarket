using AutoMapper;
using AutoMapper.QueryableExtensions;
using EStockMarket.Company.Application.Interfaces;
using EStockMarket.Company.Application.Models.Request;
using EStockMarket.Company.Application.Models.Response;
using EStockMarket.Company.Domain.Entities;
using EStockMarket.Company.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using RabbitMQ.Client;
using System.Text;
using Utilities.Exceptions;
using Utilities.Extensions;

namespace EStockMarket.Company.Application.Services;
public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository repository;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor contextAccessor;

    public CompanyService(ICompanyRepository repository, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.contextAccessor = contextAccessor;
    }

    public CompanyResponse Add(CreateCompanyRequest companyRequest)
    {
        if (repository.Any(x => x.Name == companyRequest.Name))
            throw new AppException("Company Name already exists");

        if (repository.Any(x => x.Code == companyRequest.Code))
            throw new AppException("Company Code already exists");

        var company = mapper.Map<CompanyModel>(companyRequest);
        company.CreatedBy = contextAccessor?.HttpContext?.GetUserId()!;

        repository.Add(company);

        return mapper.Map<CompanyResponse>(company);
    }

    public void Delete(Guid id)
    {
        repository.Delete(id);
        PublishMessage(id);
    }

    private static void PublishMessage(Guid id)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        channel.QueueDeclare("DeleteRelatedStock", false, false, false, null);
        var message = id.ToString();
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish("", "DeleteRelatedStock", null, body);
    }

    public List<CompanyResponse> GetAll()
    {
        return repository.GetAll().ProjectTo<CompanyResponse>(mapper.ConfigurationProvider).ToList();
    }

    public CompanyResponse GetItem(Guid id)
    {
        return mapper.Map<CompanyResponse>(repository.GetById(id));
    }

    public CompanyResponse Update(UpdateCompanyRequest companyRequest)
    {
        if (repository.Any(x => x.Name == companyRequest.Name && x.Id != companyRequest.Id))
            throw new AppException("Company Name already exists");

        if (repository.Any(x => x.Code == companyRequest.Code && x.Id != companyRequest.Id))
            throw new AppException("Company Code already exists");

        var company = mapper.Map<CompanyModel>(companyRequest);

        repository.Update(company);

        return mapper.Map<CompanyResponse>(company);
    }

    public void UpdateLatestStockPrice(Guid id, decimal price)
    {
        var company = repository.GetById(id);
        if (company is null)
            return;

        company.LatestStockPrice = price;
        repository.Update(company);
    }
}
