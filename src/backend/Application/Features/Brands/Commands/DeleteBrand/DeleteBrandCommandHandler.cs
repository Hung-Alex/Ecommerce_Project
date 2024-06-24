using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Brands;
using Domain.Shared;
using MediatR;

namespace Application.Features.Brands.Commands.DeleteBrand
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteBrandCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var repoBrand = _unitOfWork.GetRepository<Brand>();
            var brand = await repoBrand.GetByIdAsync(request.Id);
            if (brand == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            repoBrand.Delete(brand);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
