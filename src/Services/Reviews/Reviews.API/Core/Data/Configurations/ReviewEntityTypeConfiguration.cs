using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reviews.API.Core.Data.Configurations
{
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text)
                .HasMaxLength(1000);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.ProductId)
                .IsRequired();
        }
    }
}
