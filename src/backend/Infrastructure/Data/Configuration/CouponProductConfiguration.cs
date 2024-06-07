using Domain.Entities.Coupons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class CouponProductConfiguration : IEntityTypeConfiguration<CouponProduct>
    {
        public void Configure(EntityTypeBuilder<CouponProduct> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
