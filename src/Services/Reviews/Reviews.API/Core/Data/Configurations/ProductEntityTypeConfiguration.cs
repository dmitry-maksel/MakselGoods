using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reviews.API.Core.Data.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasMany(x => x.Reviews)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .IsRequired();
        }
    }
}
