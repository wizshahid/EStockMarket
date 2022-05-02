using AutoMapper;
using AutoMapper.QueryableExtensions;
using EStockMarket.Stock.Application.Interfaces;
using EStockMarket.Stock.Application.Models.Request;
using EStockMarket.Stock.Application.Models.Response;
using EStockMarket.Stock.Domain.Entities;
using EStockMarket.Stock.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Utilities.Extensions;

namespace EStockMarket.Stock.Application.Services;
public class StockService : IStockService
{
    private readonly IStockRepository repository;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor contextAccessor;

    public StockService(IStockRepository repository, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.contextAccessor = contextAccessor;
    }

    public StockResponse Add(CreateStockRequest item)
    {
        var stock = mapper.Map<StockModel>(item);
        stock.CreatedOn = DateTime.Now;
        stock.CreatedBy = contextAccessor?.HttpContext?.GetUserId()!;
        repository.Add(stock);
        PublishMessage(item);
        return mapper.Map<StockResponse>(stock);
    }

    private static void PublishMessage(CreateStockRequest item)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        channel.QueueDeclare("UpdateStockPrice", false, false, false, null);
        var message = JsonConvert.SerializeObject(new
        {
            item.CompanyId,
            item.Price
        });
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish("", "UpdateStockPrice", null, body);
    }

    public void DeleteCompanyStocks(Guid companyId)
    {
        var stockList = repository.GetAll(x => x.CompanyId == companyId);
        repository.DeleteRange(stockList);
    }

    public List<StockResponse> GetAll(Guid companyId, DateTime startDate, DateTime endDate)
    {
        var query = repository.GetAll(x => x.CompanyId == companyId);

        if (startDate != DateTime.MinValue)
            query = query.Where(x => x.CreatedOn >= startDate);

        if (endDate != DateTime.MinValue)
            query = query.Where(x => x.CreatedOn <= endDate);

        return query.ProjectTo<StockResponse>(mapper.ConfigurationProvider).ToList();
    }

    public StockKPI GetKPIs(Guid companyId, DateTime startDate, DateTime endDate)
    {
        var list = GetAll(companyId, startDate, endDate);
        StockKPI kpi = new()
        {
            AvgPrice = list.Average(x => x.Price),
            MaxPrice = list.Max(x => x.Price),
            MinPrice = list.Min(x => x.Price),
        };

        return kpi;
    }
}
