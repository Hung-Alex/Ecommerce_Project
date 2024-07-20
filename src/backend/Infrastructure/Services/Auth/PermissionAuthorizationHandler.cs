using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Services.Auth
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
