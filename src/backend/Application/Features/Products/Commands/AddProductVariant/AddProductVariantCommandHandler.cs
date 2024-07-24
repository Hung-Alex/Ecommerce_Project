using Application.Common.Interface;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities.Products;
using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Features.Products.Commands.AddProductVariant
{
    public sealed class AddProductVariantCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddProductVariantCommand, Result<bool>>
    {
        public class AddProductVariantCommandValidator : AbstractValidator<AddProductVariantCommand>
        {
            public AddProductVariantCommandValidator()
            {
                RuleFor(b => b.Name).NotEmpty().WithMessage(nameof(UpdateProductCommand.Name));
                RuleFor(b => b.Description).NotEmpty().WithMessage(nameof(UpdateProductCommand.Description));
            }
        }
        public async Task<Result<bool>> Handle(AddProductVariantCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Product>();
            var product = await repo.FindOneAsync(new GetProductWithVariantsSpecification(request.ProductId));
            if (product == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.ProductId));
            }
            product.ProductSkus.Add(new ProductSkus() { ProductId = product.Id, Name = request.Name, Description = request.Description });
            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
