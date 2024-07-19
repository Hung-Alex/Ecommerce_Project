using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationRole:IdentityRole<Guid>
    {
        public IEnumerable<RolePermission> Permissions { get; set; }
    }
}
