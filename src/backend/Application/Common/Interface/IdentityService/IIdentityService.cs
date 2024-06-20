using Application.DTOs.Internal.User;

namespace Application.Common.Interface.IdentityService
{
    public interface IIdentityService
    {
        Task<UserDTO> GetUserAsync(string userName, CancellationToken cancellationToken = default);
        Task<Guid> CreateUserAsync(string email, string password, string userName, CancellationToken cancellationToken = default);
        Task<UserDTO> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> SignInAsync(string userName, string password, CancellationToken cancellationToken = default);
        Task<bool> SaveRefreshTokenAsync(Guid userId, string provider, string tokenName, string value);
        Task<bool> DeleteRefreshTokenAsync(Guid userId, string provider, string tokenName);
        Task<string> GetRefreshTokenAsync(Guid userId, string provider, string tokenName);
    }
}
