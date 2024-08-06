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
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteSlideCommand request, CancellationToken cancellationToken)
        {
            var repoSlide = _unitOfWork.GetRepository<Slide>();
            var slide = await repoSlide.GetByIdAsync(request.Id);
            if (slide == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            repoSlide.Delete(slide);
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
