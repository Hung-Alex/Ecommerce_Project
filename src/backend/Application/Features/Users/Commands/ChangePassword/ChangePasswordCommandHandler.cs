using Application.Common.Interface.IdentityService;
using Domain.Constants;
using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Features.Users.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandHandler(IIdentityService identityService) : IRequestHandler<ChangePasswordCommand, Result<bool>>
    {
        public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
        {
            public ChangePasswordCommandValidator()
            {
                RuleFor(x => x.UserId).NotEmpty();
                RuleFor(x => x.Password).NotEmpty()
                    .MinimumLength(AccountConstants.MinimumLengthPassword)
                    .WithMessage(ErrorConstants.UserError.MaximumLengthOfInvalidPassword(AccountConstants.MinimumLengthPassword).Description)
                    .MaximumLength(AccountConstants.MaximumLengthPassword)
                    .WithMessage(ErrorConstants.UserError.MaximumLengthOfInvalidPassword(AccountConstants.MaximumLengthPassword).Description);
                RuleFor(x => x.ConfirmPassword)
                    .Equal(x => x.Password)
                    .WithMessage(nameof(ErrorConstants.UserError.PasswordNotMatch.Description));
            }
        }
        public async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await identityService.ChangePasswordAsync(request.UserId, request.Password, cancellationToken);
            if (result.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(result.Errors);
            }
            return Result<bool>.ResultSuccess(true);
        }
    }
}
