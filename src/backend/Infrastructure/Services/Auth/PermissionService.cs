using Application.Common.Interface;
using Domain.Shared;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
namespace Infrastructure.Services.Auth
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public PermissionService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) 
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public Task<Result<bool>> AddPermissionForRole(Guid permissionId, Guid roleId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<HashSet<string>> GetAllPermissionByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            
            throw new NotImplementedException();
        }

        public Task<bool> IsInRole(Guid permissionId, Guid roleId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> RemovePermissionForRole(Guid permissionId, Guid roleId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
