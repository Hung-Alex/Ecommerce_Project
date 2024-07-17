using Application.DTOs.Responses.Auth;
using Domain.Shared;
using MediatR;

namespace Application.Features.Authen.Commands.LoginWithGoogle
{
    public sealed class LoginGoogleCommandHandler : IRequestHandler<LoginGoogleCommand, Result<AuthencationResponse>>
    {
        public Task<Result<AuthencationResponse>> Handle(LoginGoogleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
