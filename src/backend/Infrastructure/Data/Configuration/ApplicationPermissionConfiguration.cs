using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ApplicationPermissionConfiguration : IEntityTypeConfiguration<ApplicationPermission>
    {
        public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Roles)
                .WithOne(x => x.Permission)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
