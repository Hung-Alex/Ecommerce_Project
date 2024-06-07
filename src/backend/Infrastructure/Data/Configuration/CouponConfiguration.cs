using Domain.Entities.Coupons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Coupons)
                .HasForeignKey(x => x.CouponId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CouponProducts)
                .WithOne(x => x.Coupon)
                .HasForeignKey(x => x.CouponId);
        }
    }
}
