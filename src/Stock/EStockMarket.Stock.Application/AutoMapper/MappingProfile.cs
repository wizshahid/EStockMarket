using AutoMapper;
using EStockMarket.Stock.Application.Models.Request;
using EStockMarket.Stock.Application.Models.Response;
using EStockMarket.Stock.Domain.Entities;

namespace EStockMarket.Stock.Application.AutoMapper;
internal class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<CreateStockRequest, StockModel>();
        CreateMap<StockModel, StockResponse>();
    }
}
