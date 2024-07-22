using Application.Common.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Auth
{
    public class PermissionService : IPermissionService
    {
        private readonly StoreDbContext _context;
        public PermissionService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<HashSet<string>> GetAllPermissionByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            var userRoles = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync(cancellationToken);

            var permissions = await _context.RolePermissions
                .Where(pr => userRoles.Contains(pr.RoleId))
                .Select(pr => pr.Permission.Name) 
                .ToListAsync(cancellationToken);

            return permissions.ToHashSet();
        }
    }
}
