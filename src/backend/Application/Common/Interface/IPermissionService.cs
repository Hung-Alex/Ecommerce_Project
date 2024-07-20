using Domain.Shared;

namespace Application.Common.Interface
{
    public interface IPermissionService
    {
        Task<bool> IsInRole(Guid permissionId, Guid roleId, CancellationToken cancellationToken = default);
        Task<Result<bool>> AddPermissionForRole(Guid permissionId, Guid roleId, CancellationToken cancellationToken = default);
        Task<Result<bool>> RemovePermissionForRole(Guid permissionId, Guid roleId, CancellationToken cancellationToken = default);
        Task<HashSet<string>> GetAllPermissionByUserId(Guid userId, CancellationToken cancellationToken = default);
    }
}
