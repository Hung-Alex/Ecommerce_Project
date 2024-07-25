using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Banners;
using Domain.Entities.Brands;
using Domain.Shared;
using MediatR;

namespace Application.Features.Brands.Commands.DeleteBrand
{
    public sealed class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media;
        }
        public async Task<Result<bool>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Brand>();
            var brand = await repo.GetByIdAsync(request.Id);
            if (brand == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            var DeleteImageResult = await _media.DeleteImageAsync(brand.PublicIdImage);
            if (DeleteImageResult.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(DeleteImageResult.Errors);
            }
            repo.Delete(brand);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
