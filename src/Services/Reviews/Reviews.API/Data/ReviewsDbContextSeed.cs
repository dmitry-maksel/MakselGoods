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
                var reviews = new List<Review>()
                {
                    new()
                    {
                        ProductId = 1,
                        UserId = 1,
                        Rating = 4,
                        Text = "Super duper, it works.",
                        IsApproved = true,
                        CreatedAt = DateTimeOffset.Now,
                        ModifiedAt = DateTimeOffset.Now
                    },
                    new()
                    {
                        ProductId = 1,
                        UserId = 1,
                        Rating = 1,
                        Text = "It doesn't work for me, wasting money",
                        IsApproved = true,
                        CreatedAt = DateTimeOffset.Now,
                        ModifiedAt = DateTimeOffset.Now
                    },
                    new()
                    {
                        ProductId = 2,
                        UserId = 1,
                        Rating = 1,
                        Text = "Very useless supplement, it did not impove my sleep",
                        IsApproved = true,
                        CreatedAt = DateTimeOffset.Now,
                        ModifiedAt = DateTimeOffset.Now
                    },
                };

                context.Reviews.AddRange(reviews);
            }

            await context.SaveChangesAsync();
        }
    }
}
