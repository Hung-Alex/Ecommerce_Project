using Application.Common.Interface;
using Application.DTOs.Internal;
using Domain.Constants;
using Domain.Entities.Images;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Images.Commands.CreateImage
{
    public sealed class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, Result<bool>>
    {
        internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandValidator>
        {

        }
        private readonly IMedia _media;
        private readonly IUnitOfWork _unitOfWork;

        public CreateImageCommandHandler(IMedia media, IUnitOfWork unitOfWork)
        {
            _media = media;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            var repoCategory = _unitOfWork.GetRepository<Image>();
            Result<ImageUpload> image = await _media.UploadLoadImageAsync(request.FormFile, UploadFolderConstants.FolderImage);
            if (image.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(image.Errors);
            }
            repoCategory.Add(new Image() { ImageExtension = request.FormFile.ContentType, ImageUrl = image.Data.Url, PublicId = image.Data.PublicId });
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
