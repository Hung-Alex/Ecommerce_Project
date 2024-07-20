using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configuration
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasMany(x => x.Permissions)
                .WithOne(x => x.Role)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
