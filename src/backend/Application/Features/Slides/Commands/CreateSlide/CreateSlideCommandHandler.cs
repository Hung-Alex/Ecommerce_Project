using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.Slides;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Slides.Commands.CreateSlide
{
    public sealed class CreateSlideCommandHandler : IRequestHandler<CreateSlideCommand, Result<bool>>
    {
        public class CreateSlideCommandValidator : AbstractValidator<CreateSlideCommand>
        {
            public CreateSlideCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty().WithMessage(nameof(CreateSlideCommand.Title));
                RuleFor(x => x.Description).NotEmpty().WithMessage(nameof(CreateSlideCommand.Description));
            }
        }
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMedia _media;
        public CreateSlideCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media;
        }
        public async Task<Result<bool>> Handle(CreateSlideCommand request, CancellationToken cancellationToken)
        {
            var repoSlide = _unitOfWork.GetRepository<Slide>();
            var repoImage = _unitOfWork.GetRepository<Image>();
            var slide = new Slide(request.Title, request.Description, request.IsActive);
            #region hanle Images         
            Result<ImageUpload> uploadResult = await _media.UploadLoadImageAsync(request.Image, UploadFolderConstants.FolderSlide);
            if (uploadResult.IsSuccess is false)
            {
                throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
            }
            slide.Image = uploadResult.Data.PublicId;
            #endregion
            repoSlide.Add(slide);
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
