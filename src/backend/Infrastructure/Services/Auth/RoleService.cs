using Application.Common.Exceptions;
using Application.Common.Interface.IdentityService;
using Application.DTOs.Internal.Role;
using Domain.Constants;
using Domain.Shared;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Auth
{
    public class RoleService : IRoleServivce
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<Result<bool>> CreateRoleAsync(string name, CancellationToken cancellationToken = default)
        {
            var role = new ApplicationRole() { Name = name };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded is false)
            {
                return Result<bool>.ResultFailures(result.Errors.Select(e => new Error(e.Code, e.Description)));
            }
            return Result<bool>.ResultSuccess(true);
        }

        public async Task<Result<bool>> DeleteRoleFromUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.ApplicationUserError.UserNotFoundWithID(userId));
            }
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(roleId));
            }
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded is false)
            {
                return Result<bool>.ResultFailures(result.Errors.Select(x => new Error(x.Code, x.Description)));
            }
            return Result<bool>.ResultSuccess(true);
        }

        public async Task<RoleInternal> GetRoleAsync(Guid roleId, CancellationToken cancellationToken = default)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null) return null;
            return new RoleInternal { Id = role.Id, RoleName = role.Name };
        }

        public async Task<bool> IsInRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                return false;
            }
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null) return false;
            var roleHaveBelongUser = await _userManager.IsInRoleAsync(user, role.Name);
            return roleHaveBelongUser;
        }

        public async Task<Result<bool>> UpdateRoleAsync(Guid roleId, string name, CancellationToken cancellationToken = default)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(roleId));
            }
            role.Name = name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded is false)
            {
                return Result<bool>.ResultFailures(result.Errors.Select(x => new Error(x.Code, x.Description)));
            }
            return Result<bool>.ResultSuccess(true);
        }

        public async Task<Result<bool>> AssignmentRoleForUserAsync(Guid userId, string role, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(userId));
            }
            var isExixted = await _userManager.IsInRoleAsync(user, role);
            if (isExixted)
            {
                return Result<bool>.ResultFailures(ErrorConstants.RoleError.UserHaveBeenSameRole(role));
            }
            var result = await _userManager.AddToRoleAsync(user, role);
            if (result.Succeeded is false)
            {
                return Result<bool>.ResultFailures(result.Errors.Select(x => new Error(x.Code, x.Description)));
            }
            return Result<bool>.ResultSuccess(true);
        }
    }
}
