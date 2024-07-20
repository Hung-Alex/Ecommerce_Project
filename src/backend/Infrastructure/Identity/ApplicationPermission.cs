using Domain.Common;

namespace Infrastructure.Identity
{
    public class ApplicationPermission : BaseEntity
    {
        public ApplicationPermission() : base() { }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<RolePermission> Roles { get; set; }
    }
}
