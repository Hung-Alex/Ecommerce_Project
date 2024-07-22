using Microsoft.AspNetCore.Authorization;
using static Domain.Enums.PermissionEnum;
using static Infrastructure.Helper.PolicyHelper;

namespace Infrastructure.Services.Auth
{
    public enum PermissionOperator
    {
        And = 1, Or = 2
    }
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission) : base(policy: permission.ToString())
        {
            Policy = $"{PolicyPrefix}{Separator}{(int)PermissionOperator.And}{Separator}{permission}";
        }
        public HasPermissionAttribute(PermissionOperator operatorPermission, Permission[] permissions)
        {
            Policy = $"{PolicyPrefix}{Separator}{(int)operatorPermission}{Separator}{String.Join(Separator, permissions.Select(x=>x.ToString()))}";
        }
    }
}
