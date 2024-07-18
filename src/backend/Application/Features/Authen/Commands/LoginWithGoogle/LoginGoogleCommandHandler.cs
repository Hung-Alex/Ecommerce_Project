using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Application.DTOs.Internal;
using Application.DTOs.Responses.Auth;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;

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
                return Result<AuthencationResponse>.ResultFailures();
            }
            return Result<AuthencationResponse>.ResultSuccess(null);
        }
    }
}
