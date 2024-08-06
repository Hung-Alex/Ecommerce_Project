using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Brands;
using Domain.Shared;
using MediatR;

namespace Application.Features.Brands.Commands.DeleteBrand
{
    public sealed class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Brand>();
            var brand = await repo.GetByIdAsync(request.Id);
            if (brand == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            repo.Delete(brand);
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
