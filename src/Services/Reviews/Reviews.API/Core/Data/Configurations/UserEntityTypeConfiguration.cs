using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reviews.API.Core.Data.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasMany(x => x.Reviews)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}
