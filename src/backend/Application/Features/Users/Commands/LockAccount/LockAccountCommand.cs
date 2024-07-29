using Domain.Shared;
using MediatR;

namespace Application.Features.Users.Commands.LockAccount
{
    public record LockAccountCommand(bool IsLocked, Guid UserId) : IRequest<Result<bool>>;
}
