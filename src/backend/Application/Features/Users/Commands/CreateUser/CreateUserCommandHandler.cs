using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Application.Utils;
using Domain.Constants;
using Domain.Entities.Carts;
using Domain.Entities.Users;
using Domain.Shared;
using FluentValidation;
using MediatR;
using System.Transactions;

namespace Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<bool>>
    {
        private readonly IIdentityService _identityService;
        private readonly IRoleServivce _roleService;
        private readonly IUnitOfWork _unitOfWork;
        public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
        {
            public CreateUserCommandValidator()
            {
                RuleFor(x => x.Email).NotEmpty()
                    .WithMessage(nameof(CreateUserCommand.Email))
                    .MustAsync(ValidationExtension.ValidateEmail)
                    .WithMessage(nameof(ErrorConstants.UserError.EmailIsInvaild.Description));
                RuleFor(x => x.Password).NotEmpty().WithMessage(nameof(CreateUserCommand.Password));
                RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(nameof(CreateUserCommand.ConfirmPassword));
                RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage(ErrorConstants.UserError.PasswordNotMatch.Description);
                RuleFor(x => x.UserName).NotEmpty().WithMessage(nameof(CreateUserCommand.UserName))
                    .MustAsync(ValidationExtension.IsValidUsername)
                    .WithMessage(nameof(ErrorConstants.UserError.InvalidUserName.Description));
                RuleFor(x => x.Roles).NotEmpty().WithMessage(nameof(CreateUserCommand.Roles));
                RuleFor(x => x.PhoneNumber)
                    .Must(x => string.IsNullOrEmpty(x) || ValidationExtension.ValidatePhone(x))
                    .WithMessage(ErrorConstants.UserError.PhoneNumberIsInvaild.Description);
            }
        }
        public CreateUserCommandHandler(IIdentityService identityService, IRoleServivce roleService, IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _roleService = roleService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var checkValidation = await ValidationRole(request.Roles);
            if (checkValidation.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(checkValidation.Errors);
            }
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var userId = CreateUser(request);
                    var applicationUserId = await CreateApplicationUser(request, userId);
                    if (applicationUserId.IsSuccess is false)
                    {
                        transactionScope.Dispose();
                        return Result<bool>.ResultFailures(applicationUserId.Errors);
                    }
                    var isAssignRole = await AssignRoleForApplicationUser(request.Roles, applicationUserId.Data);
                    if (isAssignRole.IsSuccess is false)
                    {
                        transactionScope.Dispose();
                        return Result<bool>.ResultFailures(isAssignRole.Errors);
                    }
                    await _unitOfWork.Commit();
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
            var roleInDb = await _roleService.GetAllRoleAsync();
            var intersect = roleInDb.Select(x => x.Id).Intersect(roles).ToList();
            if (intersect.Count() != roles.Count())
            {
                return Result<bool>.ResultFailures(ErrorConstants.RoleError.HaveAnyRoleDontBelongApplication);
            }
            return Result<bool>.ResultSuccess(true);
        }
        private async Task<Result<Guid>> CreateApplicationUser(CreateUserCommand ApplicationUser, Guid userId)
        {
            var isCreatedUser = await _identityService.CreateUserAsync(ApplicationUser.Email, ApplicationUser.Password, ApplicationUser.UserName, userId, ApplicationUser.IsActive);
            if (isCreatedUser.IsSuccess is false)
            {
                return Result<Guid>.ResultFailures(isCreatedUser.Errors);
            }
            return isCreatedUser;
        }
        private async Task<Result<bool>> AssignRoleForApplicationUser(IEnumerable<Guid> roles, Guid applicationUserId)
        {
            var result = await _roleService.AssignmentRoleForUserAsync(applicationUserId, roles);
            return result;
        }
        private Guid CreateUser(CreateUserCommand User)
        {
            var repoUser = _unitOfWork.GetRepository<User>();
            var repoCart = _unitOfWork.GetRepository<Cart>();
            var user = new User()
            {
                Region = User.Region,
                FirstName = User.FirstName,
                LastName = User.LastName,
                City = User.City,
                Country = User.Country
            };
            repoUser.Add(user);
            repoCart.Add(new Cart() { UserId = user.Id });
            return user.Id;
        }
    }
}
