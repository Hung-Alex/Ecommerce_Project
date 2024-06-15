using Application.DTOs.Internal.User;

namespace Application.Common.Interface.IdentityService
{
    public interface IIdentityService
    {
        Task<UserDTO> GetUserAsync(string email, CancellationToken cancellationToken = default);

        Task<Guid> CreateUserAsync(string email, string password, string userName, CancellationToken cancellationToken = default);

        public Task<UserDTO> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken = default);
    }
}
