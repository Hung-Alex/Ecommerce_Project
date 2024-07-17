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
            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.ProductSkus)
                .HasForeignKey(x => x.ProductSkusId);
        }
    }
}
