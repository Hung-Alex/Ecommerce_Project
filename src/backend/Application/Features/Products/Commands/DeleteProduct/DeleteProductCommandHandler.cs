using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Products;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var repoProduct = _unitOfWork.GetRepository<Product>();
            var product = await repoProduct.GetByIdAsync(request.Id);
            if (product == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            repoProduct.Delete(product);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
