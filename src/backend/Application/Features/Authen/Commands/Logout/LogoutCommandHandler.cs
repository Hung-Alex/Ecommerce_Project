using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Domain.Constants;
using Domain.Shared;
using MediatR;

namespace Application.Features.Authen.Commands.Logout
{
    public sealed class LogoutCommandHandler(IJwtProvider jwtProvider, IIdentityService identityService) : IRequestHandler<LogoutCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var isValid = await jwtProvider.ValidateTokenAsync(request.AccessToken);
            if (isValid is false)
            {
                return Result<bool>.ResultFailures();
            }
            var claims = await jwtProvider.GetClaimsFromTokenAsync(request.AccessToken);
            var userId = claims.FirstOrDefault(x => x.Type == ClaimUser.ApplicationUserId);
            var refreshToken = await identityService.GetRefreshTokenAsync(Guid.Parse(userId.Value), UserToken.Provider, UserToken.RefreshToken);
            if (refreshToken is null)
            {
                return Result<bool>.ResultSuccess(true);
            }
            var isDeleteRefreshToken = await identityService.DeleteRefreshTokenAsync(Guid.Parse(userId.Value), UserToken.Provider, UserToken.RefreshToken);
            return Result<bool>.ResultSuccess(true);
        }
    }
}
