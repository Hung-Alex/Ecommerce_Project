using Application.Common.Interface;
using Application.DTOs.Responses.Permissions;
using Infrastructure.Data;
using Infrastructure.Identity;
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

        public async Task AssignPermissionForRoleAsync(Guid roleId, IEnumerable<Guid> permissions, CancellationToken cancellationToken = default)
        {

            var role = await _context.Roles.FindAsync(roleId);
            if (role is null)
            {
                throw new ArgumentNullException("role is null");
            }
            if (permissions is null)
            {
                throw new ArgumentNullException("permission is null");
            }
            var permissionsrole = permissions.Select(p => new RolePermission { RoleId = roleId, PermissionId = p });
            await _context.RolePermissions.AddRangeAsync(permissionsrole, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAllPermissionInRole(Guid roleId, CancellationToken cancellationToken = default)
        {
            var rolePermissions = await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .ToListAsync(cancellationToken);
            _context.RolePermissions.RemoveRange(rolePermissions);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<PermissionDTO>> GetAllPermissionAsync(CancellationToken cancellationToken = default)
        {
            var permissions = await _context.Permissions
                .Select(p => new PermissionDTO { Name = p.Name, Id = p.Id })
                .ToListAsync(cancellationToken);
            return permissions;
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
