
using EventBus.Abstractions;
using RabbitMQ;
using RabbitMQ.Client;

namespace Identity.API.ServiceInstallers;

public class EventBusInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventPublisher, RabbitMQEventPublisher>();
        services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory
        {
            HostName = "rabbitmq", // localhost
            UserName = "user",
            Password = "password",
            Port = 5672
        });
    }
}
