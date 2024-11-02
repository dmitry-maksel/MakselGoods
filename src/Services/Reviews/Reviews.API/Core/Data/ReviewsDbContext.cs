using Microsoft.EntityFrameworkCore;

namespace Reviews.API.Core.Data
{
    public class ReviewsDbContext: DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public ReviewsDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReviewsDbContext).Assembly);
        }
    }
}
