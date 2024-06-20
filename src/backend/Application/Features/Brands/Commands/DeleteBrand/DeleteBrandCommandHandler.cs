using Application.Common.Exceptions;
using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Category;
using MediatR;

namespace Application.Features.Brands.Commands.DeleteBrand
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteBrandCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var repoBrand = _unitOfWork.GetRepository<Categories>();
            var brand = await repoBrand.GetByIdAsync(request.Id);
            if (brand == null)
            {
                throw new NotFoundException($"{ErrorConstants.NotFound}{request.Id}");
            }
            repoBrand.Delete(brand);
            await _unitOfWork.Commit();
        }
    }
}
