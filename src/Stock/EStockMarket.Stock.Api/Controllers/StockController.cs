using EStockMarket.Stock.Application.Interfaces;
using EStockMarket.Stock.Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStockMarket.Stock.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockService service;

    public StockController(IStockService service)
    {
        this.service = service;
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post(CreateStockRequest stock)
    {
        return Created("", service.Add(stock));
    }

    [HttpGet("{companyId}/{startDate}/{endDate}")]
    public IActionResult Get(Guid companyId, DateTime startDate, DateTime endDate)
    {
        return Ok(service.GetAll(companyId, startDate, endDate));
    }

    [HttpGet("kpis/{companyId}/{startDate}/{endDate}")]
    public IActionResult GetKPIs(Guid companyId, DateTime startDate, DateTime endDate)
    {
        return Ok(service.GetKPIs(companyId, startDate, endDate));
    }
}
