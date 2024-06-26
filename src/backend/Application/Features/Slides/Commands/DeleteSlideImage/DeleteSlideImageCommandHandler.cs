using Application.Common.Interface;
using Application.Features.Slides.Specification;
using Domain.Constants;
using Domain.Entities.Slides;
using Domain.Shared;
using MediatR;

namespace Application.Features.Slides.Commands.DeleteSlideImage
{
    public sealed class DeleteSlideImageCommandHandler : IRequestHandler<DeleteSlideImageCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteSlideImageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteSlideImageCommand request, CancellationToken cancellationToken)
        {
            var repoSlide = _unitOfWork.GetRepository<Slide>();
            var slide = await repoSlide.FindOneAsync(new GetSlideByIdSepecification(request.SlideId));
            if (slide == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.SlideId));
            }
            var hasImage = slide.SlidesImages.Where(x => x.Id == request.SlideImageId).FirstOrDefault();
            if (hasImage is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.ProductImageDontHaveImageWithId(request.SlideImageId));
            }
            slide.SlidesImages.Remove(hasImage);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
