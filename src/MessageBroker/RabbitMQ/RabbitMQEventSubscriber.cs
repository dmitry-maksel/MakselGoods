using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace RabbitMQ;
public class RabbitMQEventSubscriber : IEventSubscriber
{
    private IConnection _connection;
    private IModel _channel;
    private readonly ILogger<RabbitMQEventSubscriber> _logger;

    public RabbitMQEventSubscriber(IConnectionFactory factory, ILogger<RabbitMQEventSubscriber> logger)
    {
        var pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions
            {
                ShouldHandle = new PredicateBuilder().Handle<BrokerUnreachableException>().Handle<SocketException>(),
                Delay = TimeSpan.FromSeconds(3),
                BackoffType = DelayBackoffType.Constant,
                MaxRetryAttempts = 5,
                OnRetry = (args) =>
                {

                    if (args.Outcome.Result is BrokerUnreachableException ex)
                    {
                        logger.LogError(ex, "RabbitMQ not available. Trying to connect...");
                    }

                    if (args.Outcome.Result is SocketException socketEx)
                    {
                        logger.LogError(socketEx, "RabbitMQ not available. Trying to connect...");
                    }

                    return ValueTask.CompletedTask;
                }
            })
            .Build();

        pipeline.Execute(() =>
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        });


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

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                var @event = JsonSerializer.Deserialize<T>(message);

                if (@event != null)
                {
                    await onMessage(@event);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "----- Error processing message:{message}", message);
            }
            finally
            {
                _channel.BasicAck(ea.DeliveryTag, false);
            }

        };

        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
