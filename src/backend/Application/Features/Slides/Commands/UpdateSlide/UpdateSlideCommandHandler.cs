using Application.Common.Interface;
using MediatR;
using FluentValidation;
using Domain.Shared;
using Domain.Constants;
using Domain.Entities.Slides;

namespace Application.Features.Slides.Commands.UpdateSlide
{
    public sealed class UpdateSlideCommandHandler : IRequestHandler<UpdateSlideCommand, Result<bool>>
    {
        internal class UpdateBrandCommandValidator : AbstractValidator<UpdateSlideCommand>
        {
            public UpdateBrandCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.Description).NotEmpty().WithMessage("Not Null");
            }
        }
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSlideCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(UpdateSlideCommand request, CancellationToken cancellationToken)
        {
            var repoSlide = _unitOfWork.GetRepository<Slide>();
            var slide = await repoSlide.GetByIdAsync(request.Id);
            if (slide == null) return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            slide.Title = request.Title;
            slide.Description = request.Description;
            slide.Order = request.Order;
            slide.Status = request.Status;
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
