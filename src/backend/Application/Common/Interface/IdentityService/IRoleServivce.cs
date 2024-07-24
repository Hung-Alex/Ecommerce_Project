using Application.DTOs.Internal.Role;
using Domain.Shared;

namespace Application.Common.Interface.IdentityService
{
    public interface IRoleServivce
    {
        Task<Result<bool>> AssignmentRoleForUserAsync(Guid userId, string role, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteRoleFromUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
        Task<Result<Guid>> CreateRoleAsync(string name, CancellationToken cancellationToken = default);
        Task<Result<bool>> UpdateRoleAsync(Guid roleId,string name, CancellationToken cancellationToken = default);
        Task<bool> IsInRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
        Task<RoleInternal> GetRoleAsync(Guid roleId, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteAsync(Guid roleId, CancellationToken cancellationToken = default);

    }
}
