using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Application.DTOs.Internal;
using Application.DTOs.Internal.Authen;
using Application.DTOs.Responses.Auth;
using Application.Helper;
using Domain.Constants;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using static Application.DTOs.Responses.Auth.AuthencationResponse;
using System.Text.Json;

namespace Application.Features.Authen.Commands.LoginWithGoogle
{
    public sealed class LoginGoogleCommandHandler : IRequestHandler<LoginGoogleCommand, Result<AuthencationResponse>>
    {
        private readonly IGoogleAuthenService _authenService;
        private readonly IIdentityService _identityService;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;
        private readonly JwtSetting _jwtSetting;
        public LoginGoogleCommandHandler(IIdentityService identityService, IJwtProvider jwtProvider, IConfiguration configuration, IGoogleAuthenService googleAuthen)
        {
            _authenService = googleAuthen;
            _identityService = identityService;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
            _jwtSetting = _configuration.GetSection("JwtSetting").Get<JwtSetting>() ?? throw new ArgumentNullException();
        }

        public async Task<Result<AuthencationResponse>> Handle(LoginGoogleCommand request, CancellationToken cancellationToken)
        {
            var userId = await _authenService.SignInByGoogleAsync(request.IdToken);
            if (userId.IsSuccess is false)
            {
                return Result<AuthencationResponse>.ResultFailures(ErrorConstants.LoginError.LoginIsNotSuccessWithGoogle);
            }
            //get infomation to generate token
            var user = await _identityService.GetUserByIdAsync(userId.Data);
            if (user is null)
            {
                return Result<AuthencationResponse>.ResultFailures(ErrorConstants.LoginError.LoginIsNotSuccessWithGoogle);
            }
            var token = await _jwtProvider.GenerateTokenAsync(user.Id);
            //generate refresh token
            var refreshToken = JWTHelper.GenerateRefreshToken(DateTime.Now.AddDays(_jwtSetting.ExpiredRefreshToken));
            //convert the refresh token to json containing the expiration time, Token. After saving it
            var convertRefreshIntoJson = JsonSerializer.Serialize<RefreshToken>(refreshToken);
            var isSuccess = await _identityService.SaveRefreshTokenAsync(user.Id, UserToken.Provider, UserToken.RefreshToken, convertRefreshIntoJson);
            if (isSuccess is false)
            {
                return Result<AuthencationResponse>.ResultFailures(ErrorConstants.LoginError.LoginIsNotSuccessWithGoogle);
            }
            return Result<AuthencationResponse>.ResultSuccess(new AuthencationResponse(token, refreshToken.Token, "Bearer", new UserAuthentication(user.Id, user.Name ?? "", user.ImageUrl)));
        }
    }
}
