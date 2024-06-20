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
using Application.DTOs.Internal;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Authen.Commands.Refresh
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthencationResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;
        private readonly JwtSetting _jwtSetting;
        public RefreshTokenCommandHandler(IIdentityService identityService, IJwtProvider jwtProvider, IConfiguration configuration)
        {
            _identityService = identityService;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
            _jwtSetting = _configuration.GetSection("JwtSetting").Get<JwtSetting>() ?? throw new ArgumentNullException();
        }
        public async Task<AuthencationResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //validate Accesstoken
            var validateToken = await _jwtProvider.ValidateTokenAsync(request.Token);
            if (validateToken == false)
            {
                throw new ValidationException(ErrorConstants.AuthAccessTokenInvalid);
            }
            //get claim from token 
            var claims = await _jwtProvider.GetClaimsFromTokenAsync(request.Token);
            //get type is containing userId ,after check it existed in claim.
            var claimUserId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (claimUserId is null)
            {
                throw new ValidationException(ErrorConstants.AuthAccessTokenInvalid);
            }
            //convert userid into Guid
            var userId = new Guid(claimUserId);
            //get refresh token from database to check it is valid
            var refreshToken = await _identityService.GetRefreshTokenAsync(userId, UserToken.Provider, UserToken.RefreshToken);
            var userToken = JsonSerializer.Deserialize<RefreshToken>(refreshToken);
            if (refreshToken is null || !(request.RefreshToken == userToken.Token && userToken.ExpriedTime >= DateTime.Now))
            {
                throw new ValidationException(ErrorConstants.AuthRefreshTokenDoesNotMatchOrExpired);
            }
            //generate new refresh token and access token
            var newRefreshToken = JWTHelper.GenerateRefreshToken(DateTime.Now.AddDays(_jwtSetting.ExpiredRefreshToken));
            var token = await _jwtProvider.GenerateTokenAsync(userId);
            //convert refresh token into json then save it
            var refreshTokenJson = JsonSerializer.Serialize<RefreshToken>(newRefreshToken);
            await _identityService.SaveRefreshTokenAsync(userId, UserToken.Provider, UserToken.RefreshToken, refreshTokenJson);
            return new AuthencationResponse(token, newRefreshToken.Token, "Bearer", userId);
        }
    }
}
