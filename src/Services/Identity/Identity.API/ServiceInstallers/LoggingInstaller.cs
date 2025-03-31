
using Microsoft.Extensions.Logging.AzureAppServices;

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
                cfg.AddAzureWebAppDiagnostics();
                cfg.AddApplicationInsights(
                    telemetryCfg =>
                    {
                        telemetryCfg.ConnectionString = configuration.GetConnectionString("AppInsights");
                    },
                    opts => { });
            });

            services.Configure<AzureFileLoggerOptions>(cfg =>
            {
                cfg.FileName = "logs-";
                cfg.FileSizeLimit = 50 * 1024;
                cfg.RetainedFileCountLimit = 5;
            });

            services.Configure<AzureBlobLoggerOptions>(cfg =>
            {
                cfg.BlobName = "logs.txt";
            });
        }
    }
}
