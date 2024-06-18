using Application.DTOs.Responses.Auth;
using MediatR;

namespace Application.Features.Authen.Commands.Login
{
    public record LoginCommand(string UserName, string Password) : IRequest<AuthencationResponse>;
}
