using Domain.Entities.Brands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Data.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Products)
                .WithOne(x => x.Brand)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
