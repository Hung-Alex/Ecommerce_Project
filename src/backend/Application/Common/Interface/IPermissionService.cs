using Application.DTOs.Responses.Permissions;
using Domain.Shared;

namespace Application.Common.Interface
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetAllPermissionByUserId(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<PermissionDTO>> GetAllPermissionAsync(CancellationToken cancellationToken = default);
        Task AssignPermissionForRoleAsync(Guid roleId, IEnumerable<Guid> permissions, CancellationToken cancellationToken = default);
    }
}
