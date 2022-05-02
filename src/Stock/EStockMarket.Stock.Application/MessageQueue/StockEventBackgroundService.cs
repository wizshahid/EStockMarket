using EStockMarket.Stock.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EStockMarket.Stock.Application.MessageQueue;
public class StockEventBackgroundService : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;

    private readonly IServiceProvider _serviceProvider;

    public StockEventBackgroundService(IServiceProvider serviceProvider)
    {
        InitRabbitMQ();
        _serviceProvider = serviceProvider;
    }

    private void InitRabbitMQ()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };

        // create connection  
        _connection = factory.CreateConnection();

        // create channel  
        _channel = _connection.CreateModel();

        _channel.QueueDeclare("DeleteRelatedStock", false, false, false, null);
        _channel.BasicQos(0, 1, false);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += ConsumerRecieved;

        _channel.BasicConsume("DeleteRelatedStock", false, consumer);
        return Task.CompletedTask;
    }

    private void ConsumerRecieved(object sender, BasicDeliverEventArgs eventArgs)
    {
        var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

        using IServiceScope scope = _serviceProvider.CreateScope();
        IStockService manager =
            scope.ServiceProvider.GetRequiredService<IStockService>();

        manager.DeleteCompanyStocks(Guid.Parse(content));
        _channel.BasicAck(eventArgs.DeliveryTag, false);
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}

