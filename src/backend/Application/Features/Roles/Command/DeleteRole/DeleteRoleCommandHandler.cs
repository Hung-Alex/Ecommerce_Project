using Application.Common.Interface.IdentityService;
using Domain.Constants;
using Domain.Shared;
using MediatR;

namespace Application.Features.Roles.Command.DeleteRole
{
    public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result<bool>>
    {
        private readonly IRoleServivce _roleServivce;
        public DeleteRoleCommandHandler(IRoleServivce roleServivce)
        {
            _roleServivce = roleServivce;
        }
        public async Task<Result<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleServivce.GetRoleAsync(request.RoleId);
            if (role == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.RoleId));
            }
            var result = await _roleServivce.DeleteAsync(request.RoleId);
            if (result.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(result.Errors);
            }
            return Result<bool>.ResultSuccess(true);
        }
    }
}
