using FluentValidation;

namespace Application.Features.Authen.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r => r.Password).Equal(x => x.ConfirmPassword).WithMessage("Password isn't duplication with ConfirmPassword");
        }
    }
}
