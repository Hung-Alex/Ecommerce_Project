using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Services.Auth.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionOperator Operator { get; }
        public string[] Permissions { get; }
        public PermissionRequirement(PermissionOperator permissionOperator, string[] permissions)
        {
            if (permissions.Length == 0)
            {
                throw new ArgumentException("At least one permission is required.", nameof(permissions));
            }
            Operator = permissionOperator;
            Permissions = permissions;
        }
    }
}
