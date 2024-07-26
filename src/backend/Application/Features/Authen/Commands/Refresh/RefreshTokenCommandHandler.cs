using Application.Common.Interface.IdentityService;
using Application.Common.Interface;
using Application.DTOs.Responses.Auth;
using MediatR;
using System.Security.Claims;
using Domain.Constants;
using Application.Helper;
using System.Text.Json;
using Application.DTOs.Internal.Authen;
using Application.DTOs.Internal;
using Microsoft.Extensions.Configuration;
using Domain.Shared;

namespace Application.Features.Authen.Commands.Refresh
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthencationResponse>>
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
        public async Task<Result<AuthencationResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //validate Accesstoken
            var validateToken = await _jwtProvider.ValidateTokenAsync(request.Token);
            if (validateToken is false)
            {
                return Result<AuthencationResponse>.ResultFailures(ErrorConstants.AuthenticationError.AuthAccessTokenInvalid);
            }
            //get claim from token 
            var claims = await _jwtProvider.GetClaimsFromTokenAsync(request.Token);
            //get type is containing userId ,after check it existed in claim.
            var claimUserId = claims.FirstOrDefault(x => x.Type == ClaimUser.ApplicationUserId)?.Value;
            if (claimUserId is null)
            {
                return Result<AuthencationResponse>.ResultFailures(null, ErrorConstants.AuthenticationError.AuthAccessTokenInvalid);
            }
            //convert userid into Guid
            var userId = new Guid(claimUserId);
            //get refresh token from database to check it is valid
            var refreshTokenIsValid = await _jwtProvider.ValidateRefreshTokenAsync(userId, request.RefreshToken);
            if (!refreshTokenIsValid)
            {
                return Result<AuthencationResponse>.ResultFailures(ErrorConstants.AuthenticationError.AuthRefreshTokenDoesNotMatchOrExpired);
            }
            //generate new refresh token and access token
            var newRefreshToken = JWTHelper.GenerateRefreshToken(DateTime.Now.AddDays(_jwtSetting.ExpiredRefreshToken));
            var token = await _jwtProvider.GenerateTokenAsync(userId);
            //convert refresh token into json then save it
            var refreshTokenJson = JsonSerializer.Serialize<RefreshToken>(newRefreshToken);
            var user = await _identityService.GetUserByIdAsync(userId);
            await _identityService.SaveRefreshTokenAsync(userId, UserToken.Provider, UserToken.RefreshToken, refreshTokenJson);
            return Result<AuthencationResponse>.ResultSuccess(new AuthencationResponse(token, newRefreshToken.Token, "Bearer", new AuthencationResponse.UserAuthentication(user.Id, user.Name ?? "", user.ImageUrl)));
        }
    }
}
