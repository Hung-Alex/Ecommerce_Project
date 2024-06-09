using Domain.Entities.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x=>x.PostTags)
                .WithOne(x=>x.Tag)
                .HasForeignKey(x=>x.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
