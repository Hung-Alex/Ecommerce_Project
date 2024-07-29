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
using System.Text.Json;
using static Application.DTOs.Responses.Auth.AuthencationResponse;

namespace Application.Features.Authen.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthencationResponse>>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;
        private readonly JwtSetting _jwtSetting;
        public LoginCommandHandler(IIdentityService identityService, IJwtProvider jwtProvider, IConfiguration configuration)
        {
            _identityService = identityService;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
            _jwtSetting = _configuration.GetSection("JwtSetting").Get<JwtSetting>() ?? throw new ArgumentNullException();
        }
        public async Task<Result<AuthencationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //check login
            var result = await _identityService.SignInAsync(request.UserName, request.Password);
            if (!result)
            {
                return Result<AuthencationResponse>.ResultFailures(ErrorConstants.AuthenticationError.AuthUsernamePasswordInvalid);
            }
            //get infomation to generate token
            var user = await _identityService.GetUserAsync(request.UserName);
            if (user is null)
            {
                return Result<AuthencationResponse>.ResultFailures(ErrorConstants.ApplicationUserError.UserNotFoundWithName(request.UserName));
            }
            var token = await _jwtProvider.GenerateTokenAsync(user.Id);
            //generate refresh token
            var refreshToken = JWTHelper.GenerateRefreshToken(DateTime.Now.AddDays(_jwtSetting.ExpiredRefreshToken));
            //convert the refresh token to json containing the expiration time, Token. After saving it
            var convertRefreshIntoJson = JsonSerializer.Serialize<RefreshToken>(refreshToken);
            var isSuccess= await _identityService.SaveRefreshTokenAsync(user.Id, UserToken.Provider, UserToken.RefreshToken, convertRefreshIntoJson);
            if (isSuccess is false)
            {
                return Result<AuthencationResponse>.ResultFailures(ErrorConstants.LoginError.LoginIsNotSuccess(request.UserName));
            }
            return Result<AuthencationResponse>.ResultSuccess(new AuthencationResponse(token, refreshToken.Token, "Bearer",user));
        }
    }
}
