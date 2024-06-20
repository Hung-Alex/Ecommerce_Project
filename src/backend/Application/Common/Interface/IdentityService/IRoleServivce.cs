
namespace Application.Common.Interface.IdentityService
{
    public interface IRoleServivce
    {
        Task<bool> AssignmentRoleForUserAsync(Guid userId, string role, CancellationToken cancellationToken = default);
    }
}
