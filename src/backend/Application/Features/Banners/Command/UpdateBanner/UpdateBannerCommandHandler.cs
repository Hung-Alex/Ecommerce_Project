using Application.Common.Interface;
using Application.DTOs.Internal;
using AutoMapper;
using MediatR;
using FluentValidation;
using Domain.Shared;
using Domain.Constants;
using Domain.Entities.Banner;
using Application.DTOs.Responses.Banners;

namespace Application.Features.Banners.Commands.UpdateBanner
{
    public sealed class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand, Result<BannerDTO>>
    {
        internal class UpdateBrandCommandValidator : AbstractValidator<UpdateBannerCommand>
        {
            public UpdateBrandCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.Description).NotEmpty().WithMessage("Not Null");
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
            var bannerRepo = _unitOfWork.GetRepository<Banner>();
            var banner = await bannerRepo.GetByIdAsync(request.Id);
            if (banner == null) return Result<BannerDTO>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id)); ;
            Result<ImageUpload> uploadResult = null;
            if (!(request.FormFile is null))
            {
                uploadResult = await _media.UploadLoadImageAsync(request.FormFile, cancellationToken);
            }
            banner.Title = request.Title;
            banner.Description = request.Description;
            banner.Left = request.left;
            banner.Right = request.right;
            if (!(uploadResult is null))
            {
                banner.LogoImageUrl = uploadResult.Data.Url;
            }
            await _unitOfWork.Commit();
            var BannerDTO = _mapper.Map<BannerDTO>(banner);
            return Result<BannerDTO>.ResultSuccess(BannerDTO);
        }
    }
}
