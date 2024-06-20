using Application.Common.Exceptions;
using Application.Common.Interface.IdentityService;
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
        public async Task<bool> AssignmentRoleForUserAsync(Guid userId, string role, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new NotFoundException($"not found user with id is {userId}");
            }
            var isExixted = await _userManager.IsInRoleAsync(user, role);
            if (isExixted)
            {
                throw new ConflictException("User've been same role");
            }
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }
    }
}
