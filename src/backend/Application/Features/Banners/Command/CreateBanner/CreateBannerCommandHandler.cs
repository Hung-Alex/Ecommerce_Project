using Application.Common.Interface;
using Application.DTOs.Internal;
using Domain.Constants;
using Domain.Entities.Banner;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Banners.Commands.CreateBanner
{
    public sealed class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, Result<bool>>
    {
        internal class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommandValidator>
        {

        }
        private readonly IMedia _media;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBannerCommandHandler(IMedia media, IUnitOfWork unitOfWork)
        {
            _media = media;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
        {
            var repoBanner = _unitOfWork.GetRepository<Banner>();
            Result<ImageUpload> image=null;
            if (request.FormFile is not null)
            {
                image = await _media.UploadLoadImageAsync(request.FormFile, UploadFolderConstants.FolderBanner);
            }
            repoBanner.Add(new Banner() { Title = request.Title, Description = request.Description, LogoImageUrl = image.Data.Url ,Left=request.left,Right=request.right});
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
