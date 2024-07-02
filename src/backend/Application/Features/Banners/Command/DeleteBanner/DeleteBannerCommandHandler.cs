using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Banner;
using Domain.Shared;
using MediatR;

namespace Application.Features.Banners.Commands.DeleteBanner
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteBannerCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
        {
            var repoBanner = _unitOfWork.GetRepository<Banner>();
            var banner = await repoBanner.GetByIdAsync(request.Id);
            if (banner == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            repoBanner.Delete(banner);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
