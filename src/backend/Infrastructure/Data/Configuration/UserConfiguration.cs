using Domain.Entities.Carts;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.WishList)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Addresses)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Orders)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.City).IsRequired(false);
            builder.Property(x => x.Region).IsRequired(false);
            builder.Property(x => x.Country).IsRequired(false);
            builder.Property(x => x.PostalCode).IsRequired(false);
            builder.Property(x => x.AvatarImage).IsRequired(false);
            builder.Property(x => x.City).IsRequired(false);
            builder.Property(x => x.FirstName).IsRequired(false);
            builder.Property(x => x.LastName).IsRequired(false);
        }
    }
}
