using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.ProductSubCategories)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.Images)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.ProductSkus)
               .WithOne(x => x.Product)
               .HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.ProductCoupons)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.Rattings)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
