namespace Products.API.Core.Data
{
    public static class ProductsDbContextSeed
    {

        public static async Task SeedDataAsync(ProductsDbContext context)
        {
            if (!context.Products.Any())
            {
                await context.Products.AddRangeAsync(GetProducts());
            }

            await context.SaveChangesAsync();
        }

        private static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Ashwagandha",
                    Description = "Description",
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    Tags = new List<Tag>
                    {
                        new Tag
                        {
                            Name = "Adaptogen"
                        }
                    }
                },
                new Product
                {
                    Name = "L-theanine",
                    Description = "Description",
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    Tags = new List<Tag>
                    {
                        new Tag
                        {
                            Name = "Amino Acid"
                        },
                        new Tag
                        {
                            Name = "Calm"
                        }
                    }
                }
            };
        }
    }
}
