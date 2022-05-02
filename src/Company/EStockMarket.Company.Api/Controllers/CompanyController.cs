using EStockMarket.Company.Application.Interfaces;
using EStockMarket.Company.Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStockMarket.Company.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService service;

    public CompanyController(ICompanyService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        return Ok(service.GetItem(id));
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(Guid id)
    {
        service.Delete(id);
        return Ok();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post([FromBody] CreateCompanyRequest companyRequest)
    {
        return Created("", service.Add(companyRequest));
    }

    [HttpPut]
    [Authorize]
    public IActionResult Put([FromBody] UpdateCompanyRequest companyRequest)
    {
        return Ok(service.Update(companyRequest));
    }
}
