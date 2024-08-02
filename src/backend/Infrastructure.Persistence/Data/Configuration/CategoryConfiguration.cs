using Domain.Entities.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(r => r.IsDeleted)
                    .HasFilter("IsDeleted = 0");
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.HasQueryFilter(c => !c.IsDeleted);//negate the value of IsDeleted, true into false and false into true
            builder.HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParrentId);
        }
    }
}
