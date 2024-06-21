using Application.Common.Exceptions;
using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Products;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var repoProduct = _unitOfWork.GetRepository<Product>();
            var product = await repoProduct.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException($"{ErrorConstants.NotFound}{request.Id}");
            }
            repoProduct.Delete(product);
            await _unitOfWork.Commit();
        }
    }
}
