using Application.Common.Interface;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Auth.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceProvider _serviceProvider;
        public PermissionAuthorizationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task<Task> HandleRequirementAsync(
            AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimUser.ApplicationUserId)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            var userId = Guid.Parse(userIdClaim);
            using var scope = _serviceProvider.CreateScope();
            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
            var permissionsHashSet = await permissionService.GetAllPermissionByUserId(userId);

            if (requirement.Operator == PermissionOperator.And)
            {
                if (requirement.Permissions.All(permission => permissionsHashSet.Contains(permission)))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else // PermissionOperator.Or
            {
                if (requirement.Permissions.Any(permission => permissionsHashSet.Contains(permission)))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }

    }
}
