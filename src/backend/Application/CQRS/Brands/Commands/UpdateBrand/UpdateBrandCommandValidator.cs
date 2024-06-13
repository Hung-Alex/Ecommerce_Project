using FluentValidation;

namespace Application.CQRS.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Not Null");
            RuleFor(b => b.Name).NotEmpty().WithMessage("Not Null");
            RuleFor(b => b.Description).NotEmpty().WithMessage("Not Null");
            RuleFor(b => b.UrlSlug).NotEmpty().WithMessage("Not Null");
        }
    }
}
