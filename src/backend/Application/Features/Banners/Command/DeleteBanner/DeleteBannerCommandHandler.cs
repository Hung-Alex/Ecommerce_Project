using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Banners;
using Domain.Shared;
using MediatR;

namespace Application.Features.Banners.Commands.DeleteBanner
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteBannerCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media;
        }
        public async Task<Result<bool>> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
        {
            var repoBanner = _unitOfWork.GetRepository<Banner>();
            var banner = await repoBanner.GetByIdAsync(request.Id);
            if (banner == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            var DeleteImageResult = await _media.DeleteImageAsync(banner.PublicIdImage);
            if (DeleteImageResult.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(DeleteImageResult.Errors);
            }
            repoBanner.Delete(banner);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
