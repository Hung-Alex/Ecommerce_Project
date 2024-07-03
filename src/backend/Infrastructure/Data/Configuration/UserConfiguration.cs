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

            //config updatedBy and createdBy
            builder.HasMany(x => x.CreatedByBanner)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.UpdatedByBanner)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.CreatedByBrand)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.UpdatedByBrand)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.CreatedByCategory)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.UpdatedByCategory)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.CreatedByComment)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.UpdatedByComment)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.CreatedByPost)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.UpdatedByPost)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.CreatedByRatting)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.UpdatedByRatting)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.CreatedBySlide)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.UpdatedBySlide)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.CreatedByTag)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.UpdatedByTag)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);

        }
    }
}
