using Application.Utils;
using Domain.Constants;
using FluentValidation;

namespace Application.Features.Authen.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r => r.Password).Equal(x => x.ConfirmPassword).WithMessage(ErrorConstants.UserError.PasswordNotMatch.Description);
            RuleFor(r => r.userName).NotEmpty().WithMessage(nameof(RegisterCommand.userName)).MustAsync(ValidationExtension.IsValidUsername).WithMessage(ErrorConstants.UserError.InvalidUserName.Description);
            RuleFor(r => r.Email).NotEmpty().WithMessage(nameof(RegisterCommand.Email)).MustAsync(ValidationExtension.ValidateEmail).WithMessage(ErrorConstants.UserError.EmailIsInvaild.Description);
        }
    }
}
