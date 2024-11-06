
using EventBus.Abstractions;
using RabbitMQ;
using RabbitMQ.Client;
using Reviews.API.IntegrationEvents.EventHandlers;

namespace Reviews.API.ServicesInstallers;

public class EventBusServiceInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory
        {
            HostName = "rabbitmq", // localhost
            UserName = "user",
            Password = "password",
            Port = 5672
        });

        services.AddSingleton<IEventSubscriber, RabbitMQEventSubscriber>();
        services.AddTransient<DisplayNameChangedEventHandler>();
    }
}
