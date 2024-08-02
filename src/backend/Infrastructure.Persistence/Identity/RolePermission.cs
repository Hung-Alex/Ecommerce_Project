namespace Infrastructure.Persistence.Identity
{
    public class RolePermission
    {
        public RolePermission() { }
        public ApplicationRole Role { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public ApplicationPermission Permission { get; set; }
    }
}
