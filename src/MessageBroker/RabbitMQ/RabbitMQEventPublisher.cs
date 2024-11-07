using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQ;
public class RabbitMQEventPublisher : IEventPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<RabbitMQEventPublisher> _logger;

    public RabbitMQEventPublisher(IConnectionFactory factory, ILogger<RabbitMQEventPublisher> logger)
    {
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _logger = logger;
    }

    public Task PublishAsync<T>(T @event) where T : class
    {
        var queueName = typeof(T).Name;
        //_channel.QueueDeclare(queue: queueName, durable: false, autoDelete: false, exclusive: false, arguments: null);

        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);

        return Task.CompletedTask;
    }
}
