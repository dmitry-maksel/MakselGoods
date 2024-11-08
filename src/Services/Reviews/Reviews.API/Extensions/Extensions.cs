using Reviews.API.Data;
using Reviews.API.ServicesInstallers;

namespace Reviews.API.Extensions
{
    public static class Extensions
    {
        public static async Task SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                app.Logger.LogInformation("SeedData started");

                var dbContext = scope.ServiceProvider.GetRequiredService<ReviewsDbContext>();
                await ReviewsDbContextSeed.SeedDataAsync(dbContext);

                app.Logger.LogInformation("SeedData completed");
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "SeedData failed");
            }
        }

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
