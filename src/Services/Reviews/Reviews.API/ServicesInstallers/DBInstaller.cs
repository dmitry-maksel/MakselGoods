
using Microsoft.EntityFrameworkCore;
using Reviews.API.Core.Interfaces;
using Reviews.API.Data;
using Reviews.API.Infrastructure.Implementaions;
using System.Reflection;

namespace Reviews.API.ServicesInstallers
{
    public class DBInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReviewsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 15,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });

            services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}
