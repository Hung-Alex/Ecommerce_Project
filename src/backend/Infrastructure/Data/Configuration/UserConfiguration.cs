using Domain.Entities.Carts;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Cart)
                .WithOne(c => c.User as ApplicationUser)
                .HasForeignKey<Cart>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.WishList)
                .WithOne(x => x.User as ApplicationUser)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Addresses)
                .WithOne(x => x.User as ApplicationUser)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Orders)
                .WithOne(x => x.User as ApplicationUser)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.City).IsRequired(false);
            builder.Property(x => x.Region).IsRequired(false);
            builder.Property(x => x.Country).IsRequired(false);
            builder.Property(x => x.PostalCode).IsRequired(false);
            builder.Property(x => x.ImageUrl).IsRequired(false);
            builder.Property(x => x.City).IsRequired(false);
          


        }
    }
}
