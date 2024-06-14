using Domain.Entities.Users;

namespace Application.Common.Interface
{
    public interface IJwtProvider
    {
        Task<string> GenerateTokenAsync(IUser user);
        Task<string> GenerateRefreshTokenAsync();
    }
}
