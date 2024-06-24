
using System.Security.Claims;

namespace Application.Common.Interface
{
    public interface IJwtProvider
    {
        Task<string> GenerateTokenAsync(Guid userId);
        Task<bool> ValidateTokenAsync(string token);
        Task<IEnumerable<Claim>> GetClaimsFromTokenAsync(string token);
        Task<bool> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
    }
}
