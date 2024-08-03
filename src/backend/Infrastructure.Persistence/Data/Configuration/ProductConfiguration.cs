using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(r => r.IsDeleted)
                    .HasFilter("IsDeleted = 0");
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.HasQueryFilter(c => !c.IsDeleted);//negate the value of IsDeleted, true into false and false into true
            builder.HasMany(x => x.Images)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.Rattings)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.WishLists)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            builder.HasMany(x=>x.Images)
                .WithOne(x => x.Product)
                .HasForeignKey(x=>x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
