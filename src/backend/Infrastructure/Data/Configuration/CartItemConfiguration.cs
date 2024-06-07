using Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(x => x.Id);
            //mapping productSkus, Product
            builder.HasOne(x=>x.Product)
                .WithMany(x=>x.CartItems)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ProductSkus)
               .WithMany(x => x.CartItems)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
