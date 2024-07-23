using Application.Common.Interface;
using Application.DTOs.Internal;
using AutoMapper;
using MediatR;
using FluentValidation;
using Domain.Shared;
using Domain.Constants;
using Domain.Entities.Banners;
using Application.DTOs.Responses.Banners;
using Application.Features.Banners.Commands.CreateBanner;
using Application.Common.Exceptions;
namespace Application.Features.Banners.Commands.UpdateBanner
{
    public sealed class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand, Result<BannerDTO>>
    {
        internal class UpdateBannerCommandValidator : AbstractValidator<UpdateBannerCommand>
        {
            public UpdateBannerCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty().WithMessage(nameof(CreateBannerCommand.Title));
                RuleFor(x => x.Description).NotEmpty().WithMessage(nameof(CreateBannerCommand.Description));
            }
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        private readonly IMapper _mapper;
        public UpdateBannerCommandHandler(IUnitOfWork unitOfWork, IMedia media, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _media = media;
            _mapper = mapper;
        }
        public async Task<Result<BannerDTO>> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
        {
            var repoBanner = _unitOfWork.GetRepository<Banner>();
            var banner = await repoBanner.GetByIdAsync(request.Id);
            if (banner == null) return Result<BannerDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            Result<ImageUpload> uploadResult = null;
            if (request.FormFile is not null)
            {
                uploadResult = await _media.UploadLoadImageAsync(request.FormFile, UploadFolderConstants.FolderBanner, cancellationToken);
                if (uploadResult.IsSuccess is false)
                {
                    throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
                }
                banner.LogoImageUrl = uploadResult?.Data.Url;

            }
            banner.Title = request.Title;
            banner.Description = request.Description;
            banner.IsVisible = request.Visible;
            await _unitOfWork.Commit();
            var BannerDTO = _mapper.Map<BannerDTO>(banner);
            return Result<BannerDTO>.ResultSuccess(BannerDTO);
        }
    }
}
