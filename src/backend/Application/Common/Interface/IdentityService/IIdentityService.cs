using Application.DTOs.Internal.User;
using Application.DTOs.Responses.ApplicationUsers;
using Domain.Shared;

namespace Application.Common.Interface.IdentityService
{
    public interface IIdentityService
    {
        Task<UserDTO> GetUserAsync(string userName, CancellationToken cancellationToken = default);
        Task<Result<bool>> ChangePasswordAsync(Guid userId,string password, CancellationToken cancellationToken = default);
        Task<Result<Guid>> UpdateUserByUserIdAsync(Guid userId,string phoneNumber,bool isLock, CancellationToken cancellationToken = default);
        Task<Result<bool>> LockAccountAsync(Guid userId, bool isLock, CancellationToken cancellationToken = default);
        Task<Guid> CreateUserAsync(string email, string password, string userName, Guid UserDomainId, CancellationToken cancellationToken = default);
        Task<Result<Guid>> CreateUserAsync(string email, string password, string userName, Guid UserDomainId, bool lockAccount, CancellationToken cancellationToken = default);
        Task<UserDTO> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApplicationUserDTO> GetApplicationUserByUserIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> SignInAsync(string userName, string password, CancellationToken cancellationToken = default);
        Task<bool> SaveRefreshTokenAsync(Guid userId, string provider, string tokenName, string value);
        Task<bool> DeleteRefreshTokenAsync(Guid userId, string provider, string tokenName);
        Task<string> GetRefreshTokenAsync(Guid userId, string provider, string tokenName);
    }
}
