using EStockMarket.Authorization.Application.Interfaces;
using EStockMarket.Authorization.Application.Models.Request;
using EStockMarket.Authorization.Application.Models.Response;
using EStockMarket.Authorization.Domain.Entities;
using EStockMarket.Authorization.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities.Exceptions;
using Crypt = BCrypt.Net.BCrypt;

namespace EStockMarket.Authorization.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository repository;
    private readonly string key;

    public UserService(IUserRepository repository, IConfiguration configuration)
    {
        this.repository = repository;
        key = configuration["JWT:Secret"];
    }

    public async Task<AuthResponse> Login(LoginRequest model)
    {
        var user = await repository.GetUser(model.Username);

        if (user is null)
            throw new AppException("Invalid Username");

        if (user.Password != Crypt.HashPassword(model.Password, user.Salt))
            throw new AppException("Password is incorrect");

        return GenerateToken(user);
    }

    private AuthResponse GenerateToken(User user)
    {
        var tokenHandlder = new JwtSecurityTokenHandler();

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.Id),
            }),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256)
        };

        var securityToken = tokenHandlder.CreateToken(descriptor);
        var token = tokenHandlder.WriteToken(securityToken);
        return new AuthResponse
        {
            Token = token,
            Id = user.Id
        };
    }

    public async Task SeedUser()
    {
        if ((await repository.GetAll()).Any())
            return;

        var salt = Crypt.GenerateSalt();
        User user = new()
        {
            Username = "Admin",
            Salt = salt,
            Password = Crypt.HashPassword("Admin", salt)
        };

        await repository.AddUser(user);
    }
}
