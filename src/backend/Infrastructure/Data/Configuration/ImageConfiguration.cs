using Domain.Entities.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.SlidesImages)
                .WithOne(x => x.Image)
                .HasForeignKey(x => x.ImageId);
            builder.HasMany(x => x.ProductImages)
                .WithOne(x => x.Image)
                .HasForeignKey(x => x.ImageId);
        }
    }
}
