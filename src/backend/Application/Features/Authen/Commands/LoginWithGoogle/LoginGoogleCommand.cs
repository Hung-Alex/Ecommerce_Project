using Application.DTOs.Responses.Auth;
using Domain.Shared;
using MediatR;

namespace Application.Features.Authen.Commands.LoginWithGoogle
{
    public record LoginGoogleCommand(string IdToken) : IRequest<Result<AuthencationResponse>>;
}
