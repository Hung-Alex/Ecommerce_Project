using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Application.Utils;
using Domain.Constants;
using Domain.Entities.Users;
using Domain.Interface;
using Domain.Shared;
using FluentValidation;
using MediatR;
using System.Transactions;

namespace Application.Features.Users.Commands.UpdateUser
{
    public sealed class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IIdentityService identityService, IRoleServivce roleServivce) : IRequestHandler<UpdateUserCommand, Result<bool>>
    {
        public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
        {
            public UpdateUserCommandValidator()
            {
                RuleFor(x => x.UserId).NotEmpty().WithMessage(nameof(UpdateUserCommand.UserId));
                RuleFor(x => x.PhoneNumber)
                   .Must(x => string.IsNullOrEmpty(x) || ValidationExtension.ValidatePhone(x))
                   .WithMessage(ErrorConstants.UserError.PhoneNumberIsInvaild.Description);
            }
        }
        public async Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Roles is not null)
            {
                var checkValidation = await ValidationRole(request.Roles);
                if (checkValidation.IsSuccess is false)
                {
                    return Result<bool>.ResultFailures(checkValidation.Errors);
                }
            }
            var repo = unitOfWork.GetRepository<User>();
            var user = await repo.GetByIdAsync(request.UserId);
            var ApplicationUser = await identityService.GetApplicationUserByUserIdAsync(request.UserId);
            if (user == null && ApplicationUser == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UserError.UserNotFoundWithID(request.UserId));
            }
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    UpdateUser(user, request, repo);
                    var applicationUserId = await identityService.UpdateUserByUserIdAsync(request.UserId, request.PhoneNumber, request.IsLocked);
                    if (applicationUserId.IsSuccess is false)
                    {
                        transactionScope.Dispose();
                        return Result<bool>.ResultFailures(applicationUserId.Errors);
                    }
                    if (request.Roles is not null)
                    {
                        var isDeleteSuccess = await roleServivce.DeleteAllRolesByApplicationUserIdAsync(applicationUserId.Data);
                        if (isDeleteSuccess.IsSuccess is false)
                        {
                            transactionScope.Dispose();
                            return Result<bool>.ResultFailures(isDeleteSuccess.Errors);
                        }
                        var isAssignRole = await AssignRoleForApplicationUser(request.Roles, applicationUserId.Data);
                        if (isAssignRole.IsSuccess is false)
                        {
                            transactionScope.Dispose();
                            return Result<bool>.ResultFailures(isAssignRole.Errors);
                        }
                    }
                    await unitOfWork.Commit();
                    transactionScope.Complete();
                    return Result<bool>.ResultSuccess(true);
                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();
                    throw ex;
                }
            }

        }
        private async Task<Result<bool>> ValidationRole(IEnumerable<Guid> roles)
        {
            var checkDuplicateRole = new HashSet<Guid>(roles);
            if (roles.Count() != checkDuplicateRole.Count())
            {
                return Result<bool>.ResultFailures(ErrorConstants.RoleError.DuplicateRole);
            }
            var roleInDb = await roleServivce.GetAllRoleAsync();
            var intersect = roleInDb.Select(x => x.Id).Intersect(roles).ToList();
            if (intersect.Count() != roles.Count())
            {
                return Result<bool>.ResultFailures(ErrorConstants.RoleError.HaveAnyRoleDontBelongApplication);
            }
            return Result<bool>.ResultSuccess(true);
        }
        private async Task<Result<bool>> AssignRoleForApplicationUser(IEnumerable<Guid> roles, Guid applicationUserId)
        {
            var result = await roleServivce.AssignmentRoleForUserAsync(applicationUserId, roles);
            return result;
        }
        private void UpdateUser(User user, UpdateUserCommand request, IRepository<User> repository)
        {
            user.Region = request.Region;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.City = request.City;
            user.Country = request.Country;
            repository.Update(user);
        }
    }
}
