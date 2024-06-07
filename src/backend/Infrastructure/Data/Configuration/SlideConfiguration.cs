using Domain.Entities.Slides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class SlideConfiguration : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.SlidesImages)
                .WithOne(x => x.Slide)
                .HasForeignKey(x => x.SlideId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
