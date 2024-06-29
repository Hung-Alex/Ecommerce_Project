using Application.Common.Interface;
using Application.Features.Slides.Specification;
using Domain.Constants;
using Domain.Entities.Images;
using Domain.Entities.Slides;
using Domain.Shared;
using MediatR;

namespace Application.Features.Slides.Commands.AddProductSlide
{
    public sealed class AddSlideImageCommandHandler : IRequestHandler<AddSlideImageCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        public AddSlideImageCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media;
        }
        public async Task<Result<bool>> Handle(AddSlideImageCommand request, CancellationToken cancellationToken)
        {
            var repoSlide = _unitOfWork.GetRepository<Slide>();
            var slide = await repoSlide.FindOneAsync(new GetSlideByIdSepecification(request.SlideId));
            if (slide == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.SlideId));

            }
            var uploadImage = await _media.UploadLoadImageAsync(request.File, UploadFolderConstants.FolderSlide);
            if (uploadImage.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(uploadImage.Errors);

            }
            var repoImage = _unitOfWork.GetRepository<Image>();
            var image = new Image()
            {
                ImageUrl = uploadImage.Data.Url
                ,
                PublicId = uploadImage.Data.PublicId
                ,
                ImageExtension = request.File.ContentType
            };
            repoImage.Add(image);
            var slideImage = new SlidesImage(slide.Id, image.Id) { OrderItem=slide.SlidesImages.Count()};
            slide.SlidesImages.Add(slideImage);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
