using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Images;
using Domain.Entities.Products;
using Domain.Entities.Slides;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Slides.Commands.CreateSlide
{
    public sealed class CreateSlideCommandHandler : IRequestHandler<CreateSlideCommand, Result<bool>>
    {
        internal class CreateSlideCommandValidator : AbstractValidator<CreateSlideCommand>
        {

        }
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMedia _media;
        public CreateSlideCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media;
            _media = media;
        }
        public async Task<Result<bool>> Handle(CreateSlideCommand request, CancellationToken cancellationToken)
        {
            var repoSlide = _unitOfWork.GetRepository<Slide>();
            var repoImage = _unitOfWork.GetRepository<Image>();
            var slide = new Slide(request.Title, request.Description,request.IsActive, request.Order);
            #region hanle Images
            var image = new Image();
            if (request.Images is not null)
            {
                int Count = 0;
                foreach (var item in request.Images)
                {
                    var uploadResult = await _media.UploadLoadImageAsync(item, UploadFolderConstants.FolderProduct);
                    if (uploadResult.IsSuccess)
                    {
                        image = new Image()
                        {
                            ImageExtension = item.ContentType
                        ,
                            ImageUrl = uploadResult.Data.Url
                        ,
                            PublicId = uploadResult.Data.PublicId
                        };
                        repoImage.Add(image);
                    }
                    else
                    {
                        return Result<bool>.ResultFailures(ErrorConstants.UploadImageOccursErrorWithFileName(item.FileName));
                    }
                }
            }
            #endregion
            repoSlide.Add(slide);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
