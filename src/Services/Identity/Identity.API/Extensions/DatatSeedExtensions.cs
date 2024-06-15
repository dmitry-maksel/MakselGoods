using Identity.API.Core.Data;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Extensions
{
    public static class DatatSeedExtensions
    {
        public static async Task SeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    app.Logger.LogInformation("SeedData started");

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await ApplicationDbContextSeed.SeedDataAsync(userManager);

                    app.Logger.LogInformation("SeedData completed");
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "SeedData failed");
                }
            }
        }
    }
}
