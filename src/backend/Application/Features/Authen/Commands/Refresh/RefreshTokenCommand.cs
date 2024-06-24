using Application.DTOs.Responses.Auth;
using Domain.Shared;
using MediatR;

namespace Application.Features.Authen.Commands.Refresh
{
    public record RefreshTokenCommand(string Token, string RefreshToken) : IRequest<Result<AuthencationResponse>>;
}
