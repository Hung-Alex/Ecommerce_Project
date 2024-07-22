using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationRole:IdentityRole<Guid>
    {
        public ICollection<RolePermission> Permissions { get; set; }
    }
}
