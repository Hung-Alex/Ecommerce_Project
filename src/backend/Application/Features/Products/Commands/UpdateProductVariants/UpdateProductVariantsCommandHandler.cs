using Application.Common.Interface;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities.Products;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProductVariants
{
    public sealed class UpdateProductVariantsCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductVariantsCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateProductVariantsCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Product>();
            var product = await repo.FindOneAsync(new GetProductWithVariantsSpecification(request.ProductId));
            if (product == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.ProductId));
            }
            if (product.ProductSkus.Any())
            {
                var variant = product.ProductSkus.FirstOrDefault(x => x.Id == request.VariantsId);
                if (variant is null)
                {
                    return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.VariantsId));
                }
                variant.Name = request.Name;
                variant.Description = request.Description;
            }
            else
            {
                return Result<bool>.ResultFailures(ErrorConstants.ProductError.VariantsDontHaveVariant);
            }
            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
