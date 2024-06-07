using Domain.Entities.SubCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.productSubCategories)
                .WithOne(x => x.SubCategory)
                .HasForeignKey(x => x.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
