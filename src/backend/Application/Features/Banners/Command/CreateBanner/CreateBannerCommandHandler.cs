using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Banners.Specification;
using Domain.Constants;
using Domain.Entities.Banners;
using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Features.Banners.Commands.CreateBanner
{
    public sealed class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, Result<bool>>
    {
        internal class CreateBannerCommandValidator : AbstractValidator<CreateBannerCommand>
        {
            public CreateBannerCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty().WithMessage(nameof(CreateBannerCommand.Title));
                RuleFor(x => x.Description).NotEmpty().WithMessage(nameof(CreateBannerCommand.Description));
                RuleFor(x => x.FormFile).NotEmpty().WithMessage(nameof(CreateBannerCommand.FormFile));
            }
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
            if (request.Location is not null)
            {
                var isExistedLocationBanner = await repoBanner.FindOneAsync(new CheckLocationBannerAlreadyExistedSpecification(request.Location, null));
                if (isExistedLocationBanner is not null)
                {
                    return Result<bool>.ResultFailures(ErrorConstants.LocationBannerAlreadyExisted(request.Location));
                }
            }
            Result<ImageUpload> uploadResult = await _media.UploadLoadImageAsync(request.FormFile, UploadFolderConstants.FolderBanner);
            if (uploadResult.IsSuccess is false)
            {
                throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
            }
            repoBanner.Add(new Banner() { Title = request.Title, Description = request.Description, LogoImageUrl = uploadResult.Data.Url, Location = request.Location });
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
