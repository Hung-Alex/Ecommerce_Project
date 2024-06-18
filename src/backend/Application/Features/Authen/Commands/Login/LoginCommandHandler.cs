using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Application.DTOs.Responses.Auth;
using Application.Helper;
using MediatR;

namespace Application.Features.Authen.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthencationResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtProvider _jwtProvider;
        public LoginCommandHandler(IIdentityService identityService, IJwtProvider jwtProvider)
        {
            _identityService = identityService;
            _jwtProvider = jwtProvider;
        }
        public async Task<AuthencationResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SignInAsync(request.UserName, request.Password);
            if (!result) throw new ValidationException("username or password is invalid");
            var user = await _identityService.GetUserAsync(request.UserName);
            var token = await _jwtProvider.GenerateTokenAsync(user.Id);
            var refreshToken = JWTHelper.GenerateRefreshToken(DateTime.Now.AddDays(7));
            return new AuthencationResponse( token, refreshToken.Token, "bear", DateTime.Now, DateTime.Now, Guid.NewGuid());
        }
    }
}
