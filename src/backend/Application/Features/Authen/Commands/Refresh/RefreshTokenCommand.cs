using Application.DTOs.Responses.Auth;
using MediatR;

namespace Application.Features.Authen.Commands.Refresh
{
    public record RefreshTokenCommand(string Token,string RefreshToken):IRequest<AuthencationResponse>;
}
