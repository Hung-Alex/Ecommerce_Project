using Application.Common.Interface.IdentityService;
using Domain.Shared;
using MediatR;

namespace Application.Features.Users.Commands.LockAccount
{
    public sealed class LockAccountCommandHandler(IIdentityService identityService) : IRequestHandler<LockAccountCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(LockAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await identityService.LockAccountAsync(request.UserId, request.IsLock, cancellationToken);
            if (result.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(new Error("LockAccount", "Lock account failed"));
            }
            return Result<bool>.ResultSuccess(true);
        }
    }
}
