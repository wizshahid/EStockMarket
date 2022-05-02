using EStockMarket.Authorization.Application.Interfaces;
using EStockMarket.Authorization.Infra.Data.Mongo;
using EStockMarket.Authorization.Infra.IoC;
using Utilities.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDatabaseSetting>(builder.Configuration.GetSection(nameof(MongoDatabaseSetting)));
builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAppExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
await SeedData();

app.Run();

async Task SeedData()
{
    var scope = app.Services.CreateScope();
    var userService = scope!.ServiceProvider.GetRequiredService<IUserService>();
    await userService.SeedUser();
}
