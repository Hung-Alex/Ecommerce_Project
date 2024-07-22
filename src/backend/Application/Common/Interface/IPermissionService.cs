using Domain.Shared;

namespace Application.Common.Interface
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetAllPermissionByUserId(Guid userId, CancellationToken cancellationToken = default);
    }
}
