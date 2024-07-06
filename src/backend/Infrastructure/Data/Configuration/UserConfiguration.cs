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
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.UpdatedByBanner)
               .WithOne(x => x.UpdatedByUser)
               .HasForeignKey(x => x.UpdatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CreatedByCategory)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.UpdatedByCategory)
               .WithOne(x => x.UpdatedByUser)
               .HasForeignKey(x => x.UpdatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CreatedByComment)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.UpdatedByComment)
               .WithOne(x => x.UpdatedByUser)
               .HasForeignKey(x => x.UpdatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CreatedByPost)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.UpdatedByPost)
               .WithOne(x => x.UpdatedByUser)
               .HasForeignKey(x => x.UpdatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CreatedByRatting)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.UpdatedByRatting)
               .WithOne(x => x.UpdatedByUser)
               .HasForeignKey(x => x.UpdatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CreatedBySlide)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.UpdatedBySlide)
               .WithOne(x => x.UpdatedByUser)
               .HasForeignKey(x => x.UpdatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CreatedByTag)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.UpdatedByTag)
               .WithOne(x => x.UpdatedByUser)
               .HasForeignKey(x => x.UpdatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CreatedByCoupon)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.UpdatedByCoupon)
               .WithOne(x => x.UpdatedByUser)
               .HasForeignKey(x => x.UpdatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CreatedByProduct)
               .WithOne(x => x.CreatedByUser)
               .HasForeignKey(x => x.CreatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.UpdatedByProduct)
               .WithOne(x => x.UpdatedByUser)
               .HasForeignKey(x => x.UpdatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
