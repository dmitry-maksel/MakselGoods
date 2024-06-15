
namespace Identity.API.ServiceInstallers
{
    public class LoggingInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(cfg =>
            {
                cfg.ClearProviders();
                cfg.AddConsole();
                cfg.AddDebug();
            });
        }
    }
}
