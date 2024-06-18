using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authen.Commands.Register
{
    public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() 
        {
            RuleFor(r=>r.Password).Equal(x=>x.ConfirmPassword).WithMessage("Password isn't duplication with ConfirmPassword");
        }
    }
}
