using Domain.Entities.Slides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class SlideImageConfiguration : IEntityTypeConfiguration<SlidesImage>
    {
        public void Configure(EntityTypeBuilder<SlidesImage> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
