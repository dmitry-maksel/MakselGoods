using Reviews.API.Core.Entities;

namespace Reviews.API.Data
{
    public static class ReviewsDbContextSeed
    {
        public static async Task SeedDataAsync(ReviewsDbContext context)
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product
                    {
                        Id = 1 // Ashwagandha
                    },
                    new Product
                    {
                        Id = 2 // L-theanine
                    }
                };

                context.AddRange(products);
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User { Id = 1, DisplayName = "Admin" });
            }

            if (!context.Reviews.Any())
            {

            }

            await context.SaveChangesAsync();
        }
    }
}
