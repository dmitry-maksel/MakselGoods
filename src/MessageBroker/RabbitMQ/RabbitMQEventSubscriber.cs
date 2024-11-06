using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace RabbitMQ;
public class RabbitMQEventSubscriber : IEventSubscriber
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<RabbitMQEventSubscriber> _logger;

    public RabbitMQEventSubscriber(IConnectionFactory factory, ILogger<RabbitMQEventSubscriber> logger)
    {
        int retryAttempts = 5;
        while (retryAttempts > 0)
        {
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                break;
            }
            catch (BrokerUnreachableException ex)
            {
                logger.LogError(ex, "RabbitMQ not available. Trying to connect...");
                retryAttempts--;
                Thread.Sleep(2000);
            }
        }

        if (_connection == null || _channel == null)
        {
            throw new Exception("Failed to connect to RabbitMQ.");
        }

        _logger = logger;
    }

    public void Subscribe<T>(Func<T, Task> onMessage) where T : class
    {
        var queueName = typeof(T).Name;
        _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var @event = JsonSerializer.Deserialize<T>(message);

            if (@event != null)
            {
                await onMessage(@event);
            }
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
