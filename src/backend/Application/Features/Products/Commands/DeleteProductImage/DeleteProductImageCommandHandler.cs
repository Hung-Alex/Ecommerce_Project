using Application.Common.Interface;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities.Products;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProductImage
{
    public sealed class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia media;
        public DeleteProductImageCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            this.media = media;
        }

        public async Task<Result<bool>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            var repoProduct = _unitOfWork.GetRepository<Product>();
            var product = await repoProduct.FindOneAsync(new GetProductDetailAndImageSpecification(request.ProductId));
            if (product == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.ProductId));
            }
            var hasImage = product.Images.Where(x => x.Id == request.ProductImageId).FirstOrDefault();
            if (hasImage is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.ProductImageDontHaveImageWithId(request.ProductImageId));
            }
            product.Images.Remove(hasImage);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
