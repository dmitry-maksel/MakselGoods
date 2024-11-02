using Identity.API.ServiceInstallers;

namespace Identity.API.Extensions
{
    public static class ServiceInstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Program).Assembly.ExportedTypes
                .Where(x => typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>()
                .ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
