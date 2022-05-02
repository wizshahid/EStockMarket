using EStockMarket.Company.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EStockMarket.Company.Application.MessageQueue;
public class CompanyEventBackgroundService : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;

    private readonly IServiceProvider _serviceProvider;

    public CompanyEventBackgroundService(IServiceProvider serviceProvider)
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

        _channel.QueueDeclare("UpdateStockPrice", false, false, false, null);
        _channel.BasicQos(0, 1, false);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += ConsumerRecieved;

        _channel.BasicConsume("UpdateStockPrice", false, consumer);
        return Task.CompletedTask;
    }

    private void ConsumerRecieved(object sender, BasicDeliverEventArgs eventArgs)
    {
        var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

        EventModel obj = JsonConvert.DeserializeObject<EventModel>(content)!;

        using IServiceScope scope = _serviceProvider.CreateScope();
        ICompanyService manager =
            scope.ServiceProvider.GetRequiredService<ICompanyService>();

        manager.UpdateLatestStockPrice(obj.CompanyId, obj.Price);
        _channel.BasicAck(eventArgs.DeliveryTag, false);
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }

    private class EventModel
    {
        public Guid CompanyId { get; set; }

        public decimal Price { get; set; }
    }
}

