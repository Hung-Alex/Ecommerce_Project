using Domain.Shared;
using MediatR;

namespace Application.Features.Authen.Commands.Logout
{
    public record LogoutCommand(string AccessToken):IRequest<Result<bool>>;
}
