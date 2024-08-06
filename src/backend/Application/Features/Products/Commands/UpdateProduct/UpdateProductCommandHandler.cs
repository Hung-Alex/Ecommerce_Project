using Application.Common.Interface;
using MediatR;
using FluentValidation;
using Domain.Shared;
using Domain.Constants;
using Domain.Entities.Products;
using Application.Features.Products.Specification;
using Application.Utils;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, Result<bool>>
    {
        public class UpdateBrandCommandValidator : AbstractValidator<UpdateProductCommand>
        {
            public UpdateBrandCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage(nameof(UpdateProductCommand.Id));
                RuleFor(b => b.Name).NotEmpty().WithMessage(nameof(UpdateProductCommand.Name));
                RuleFor(b => b.Description).NotEmpty().WithMessage(nameof(UpdateProductCommand.Description));
                RuleFor(b => b.UrlSlug).NotEmpty()
                    .WithMessage(nameof(UpdateProductCommand.UrlSlug)).
                    MustAsync(ValidationExtension.ValidateSlug)
                    .WithMessage(ErrorConstants.UrlSlugInvalid.Description);
            }
        }
        public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Product>();
            var product = await repo.GetByIdAsync(request.Id);
            if (product == null) return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            var isExisted = await repo.FindOneAsync(new UrlSlugIsExistedSpecification(request.Id, request.UrlSlug));
            if (isExisted is not null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            product.UrlSlug = request.UrlSlug;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.OldPrice = request.OldPrice;
            product.BrandId = request.BrandId;
            product.CategoryId = request.CategoryId;
            product.IsStock = request.IsStock;
            if (request.Discount is not null)
            {
                product.Discount = request.Discount;

            }
            await unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
