using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ProductSkusConfiguration : IEntityTypeConfiguration<ProductSkus>
    {
        public void Configure(EntityTypeBuilder<ProductSkus> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(r => r.IsDeleted)
                    .HasFilter("IsDeleted = 0");
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.HasQueryFilter(c => !c.IsDeleted);//negate the value of IsDeleted, true into false and false into true
            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.ProductSkus)
                .HasForeignKey(x => x.ProductSkusId);
        }
    }
}
