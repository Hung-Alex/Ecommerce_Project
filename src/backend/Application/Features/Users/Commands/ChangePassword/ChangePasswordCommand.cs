using Domain.Shared;
using MediatR;

namespace Application.Features.Users.Commands.ChangePassword
{
    public record ChangePasswordCommand(Guid UserId, string Password, string ConfirmPassword) : IRequest<Result<bool>>;
}
