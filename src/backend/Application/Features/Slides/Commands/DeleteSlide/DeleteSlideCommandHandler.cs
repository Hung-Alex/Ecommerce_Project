using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Slides;
using Domain.Shared;
using MediatR;

namespace Application.Features.Slides.Commands.DeleteSlide
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteSlideCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media;
        }
        public async Task<Result<bool>> Handle(DeleteSlideCommand request, CancellationToken cancellationToken)
        {
            var repoSlide = _unitOfWork.GetRepository<Slide>();
            var slide = await repoSlide.GetByIdAsync(request.Id);
            if (slide == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            var DeleteImageResult = await _media.DeleteImageAsync(slide.PublicIdImage);
            if (DeleteImageResult.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(DeleteImageResult.Errors);
            }
            repoSlide.Delete(slide);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
