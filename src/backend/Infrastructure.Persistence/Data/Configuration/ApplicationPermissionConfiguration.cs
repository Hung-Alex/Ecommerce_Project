using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configuration
{
    public class ApplicationPermissionConfiguration : IEntityTypeConfiguration<ApplicationPermission>
    {
        public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Roles)
                .WithOne(x => x.Permission)
                .OnDelete(DeleteBehavior.Cascade);
            var initPermission = Enum.GetValues<PermissionEnum.Permission>();
            builder.HasData(initPermission.Select(x => new ApplicationPermission() {Id=Guid.NewGuid(), Name = x.ToString(),Description=x.ToString() }));
        }
    }
}
