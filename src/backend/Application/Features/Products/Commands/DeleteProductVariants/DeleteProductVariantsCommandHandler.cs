using Application.Common.Interface;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities.Products;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProductVariants
{
    public sealed class DeleteProductVariantsCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductVariantsCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteProductVariantsCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Product>();
            var product = await repo.FindOneAsync(new GetProductWithVariantsSpecification(request.ProductId));
            if (product == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.ProductId));
            }
            if (product.ProductSkus.Any())
            {
                var variant = product.ProductSkus.FirstOrDefault(x => x.Id == request.VariantId);
                if (variant is null)
                {
                    return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.VariantId));
                }
                product.ProductSkus.Remove(variant);
            }
            else
            {
                return Result<bool>.ResultFailures(ErrorConstants.VariantsDontHaveVariant);
            }
            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
