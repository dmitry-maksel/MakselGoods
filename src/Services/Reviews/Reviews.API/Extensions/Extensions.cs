using Reviews.API.Data;

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
    }
}
