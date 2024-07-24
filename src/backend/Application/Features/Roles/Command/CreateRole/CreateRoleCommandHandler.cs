using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Domain.Constants;
using Domain.Shared;
using FluentValidation;
using MediatR;
using System.Transactions;

namespace Application.Features.Roles.Command.CreateRole
{
    public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<bool>>
    {
        public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
        {
            public CreateRoleCommandValidator()
            {
                RuleFor(x => x.Permissions).NotNull().WithMessage(nameof(CreateRoleCommand.Permissions));
                RuleFor(x => x.RoleName).NotNull().WithMessage(nameof(CreateRoleCommand.RoleName));
            }
        }
        private readonly IPermissionService _permissionService;
        private readonly IRoleServivce _roleServivce;
        public CreateRoleCommandHandler(IPermissionService permissionService, IRoleServivce roleServivce)
        {
            _permissionService = permissionService;
            _roleServivce = roleServivce;
        }

        public async Task<Result<bool>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
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
                return Result<bool>.ResultFailures(ErrorConstants.PermissionError.DuplicatePermission);
            }
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var role = await _roleServivce.CreateRoleAsync(request.RoleName);
                    if (role.IsSuccess is false)
                    {
                        return Result<bool>.ResultFailures(role.Errors);

                    }
                    await _permissionService.AssignPermissionForRoleAsync(role.Data, request.Permissions);

                    transactionScope.Complete();
                    return Result<bool>.ResultSuccess(true);

                }
                catch (Exception e)
                {
                    transactionScope.Dispose();
                    throw new Exception("An error occurred in Process");
                }
            };
        }
    }
}
