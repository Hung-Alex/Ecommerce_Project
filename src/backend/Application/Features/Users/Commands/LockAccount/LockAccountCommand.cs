using Domain.Shared;
using MediatR;

namespace Application.Features.Users.Commands.LockAccount
{
    public record LockAccountCommand(bool IsLock, Guid UserId) : IRequest<Result<bool>>;
}
