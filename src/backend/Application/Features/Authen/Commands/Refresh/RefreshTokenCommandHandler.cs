using Application.Common.Interface.IdentityService;
using Application.Common.Interface;
using Application.DTOs.Responses.Auth;
using MediatR;
using Application.Common.Exceptions;
using System.Security.Claims;
using Domain.Constants;
using Application.Helper;
using System.Text.Json;
using Application.DTOs.Internal.Authen;

namespace Application.Features.Authen.Commands.Refresh
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthencationResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtProvider _jwtProvider;
        public RefreshTokenCommandHandler(IIdentityService identityService, IJwtProvider jwtProvider)
        {
            _identityService = identityService;
            _jwtProvider = jwtProvider;
        }
        public async Task<AuthencationResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var validateToken = await _jwtProvider.ValidateTokenAsync(request.Token);
            if (validateToken == false) throw new ValidationException();
            var claims = await _jwtProvider.GetClaimsFromTokenAsync(request.Token);
            var claimUserId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (claimUserId is null) throw new ValidationException();
            var userId = new Guid(claimUserId);
            var refreshToken = await _identityService.GetRefreshTokenAsync(userId, UserToken.Provider, UserToken.RefreshToken);
            var userToken = JsonSerializer.Deserialize<RefreshToken>(refreshToken);
            if (refreshToken is null) throw new NotFoundException();
            if (!(request.RefreshToken == userToken.Token && userToken.ExpriedTime >= DateTime.Now)) throw new ValidationException();
            var newRefreshToken = JWTHelper.GenerateRefreshToken(DateTime.Now.AddDays(7));
            var token = await _jwtProvider.GenerateTokenAsync(userId);
            var refreshTokenJson = JsonSerializer.Serialize<RefreshToken>(newRefreshToken);
            await _identityService.SaveRefreshTokenAsync(userId, UserToken.Provider, UserToken.RefreshToken, refreshTokenJson);
            return new AuthencationResponse(token, newRefreshToken.Token, "Bearer", userId);
        }
    }
}
