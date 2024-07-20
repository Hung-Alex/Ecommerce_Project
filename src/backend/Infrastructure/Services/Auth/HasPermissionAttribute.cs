using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Services.Auth
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission) : base(policy:permission)
        {
        }
    }
}
