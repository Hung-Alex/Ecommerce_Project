using Application.Common.Interface;
using MediatR;
using FluentValidation;
using Domain.Shared;
using Domain.Constants;
using Domain.Entities.Slides;
using Application.Common.Exceptions;
using Application.DTOs.Internal;

namespace Application.Features.Slides.Commands.UpdateSlide
{
    public sealed class UpdateSlideCommandHandler : IRequestHandler<UpdateSlideCommand, Result<bool>>
    {
        public class UpdateBrandCommandValidator : AbstractValidator<UpdateSlideCommand>
        {
            public UpdateBrandCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage(nameof(UpdateSlideCommand.Id));
                RuleFor(b => b.Title).NotEmpty().WithMessage(nameof(UpdateSlideCommand.Title));
                RuleFor(b => b.Description).NotEmpty().WithMessage(nameof(UpdateSlideCommand.Description));
            }
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        public UpdateSlideCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media;
        }
        public async Task<Result<bool>> Handle(UpdateSlideCommand request, CancellationToken cancellationToken)
        {
            var repoSlide = _unitOfWork.GetRepository<Slide>();
            var slide = await repoSlide.GetByIdAsync(request.Id);
            if (slide == null) return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            Result<ImageUpload> uploadResult = null;
            if (request.Image is not null)
            {
                uploadResult = await _media.UploadLoadImageAsync(request.Image, UploadFolderConstants.FolderBanner, cancellationToken);
                if (uploadResult.IsSuccess is false)
                {
                    throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
                }
                slide.Image = uploadResult?.Data.PublicId;
            }
            slide.Title = request.Title;
            slide.Description = request.Description;
            slide.IsActive = request.IsActive;
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
