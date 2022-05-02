namespace EStockMarket.Authorization.Infra.Data.Mongo;

public class MongoDatabaseSetting
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;
}