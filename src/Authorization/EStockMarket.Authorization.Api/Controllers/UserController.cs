using EStockMarket.Authorization.Application.Interfaces;
using EStockMarket.Authorization.Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace EStockMarket.Authorization.Api.Controllers;
[Route("api")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest model)
    {
        return Ok(await userService.Login(model));
    }
}
