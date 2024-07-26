using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Domain.Constants;
using Domain.Shared;
using MediatR;
using System.Transactions;

namespace Application.Features.Roles.Command.UpdateRole
{
    public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<bool>>
    {
        private readonly IPermissionService _permissionService;
        private readonly IRoleServivce _roleServivce;

        public UpdateRoleCommandHandler(IPermissionService permissionService, IRoleServivce roleServivce)
        {
            _permissionService = permissionService;
            _roleServivce = roleServivce;
        }
        public async Task<Result<bool>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var checkDuplicatePermission = new HashSet<Guid>(request.Permissions);
            if (request.Permissions.Count() != checkDuplicatePermission.Count())
            {
                return Result<bool>.ResultFailures(ErrorConstants.PermissionError.DuplicatePermission);
            }
            var PermisstionInDb = await _permissionService.GetAllPermissionAsync();
            var intersect = PermisstionInDb.Select(x => x.Id).Intersect(request.Permissions).ToList();
            if (intersect.Count() != request.Permissions.Count())
            {
                return Result<bool>.ResultFailures(ErrorConstants.PermissionError.HaveAnyPermissionDontBelongApplication);
            }
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var updateRole = await _roleServivce.UpdateRoleAsync(request.RoleId, request.RoleName);
                    if (updateRole.IsSuccess is false)
                    {
                        return Result<bool>.ResultFailures(updateRole.Errors);
                    }
                    if (request.Permissions is not null)
                    {
                        await _permissionService.DeleteAllPermissionInRole(request.RoleId);
                        await _permissionService.AssignPermissionForRoleAsync(request.RoleId, request.Permissions);
                    }
                    transactionScope.Complete();
                    return Result<bool>.ResultSuccess(true);
                }
                catch (Exception)
                {
                    transactionScope.Dispose();
                    throw new Exception("An error occurred in Process");
                }
            }
        }
    }
}
