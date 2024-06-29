using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.DTOs.Responses.Brand;
using AutoMapper;
using Domain.Entities.Brands;
using MediatR;
using FluentValidation;
using Domain.Shared;
using Domain.Constants;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public sealed class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result<BrandDTOs>>
    {
        internal class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
        {
            public UpdateBrandCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.Name).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.Description).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.UrlSlug).NotEmpty().WithMessage("Not Null");
            }
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        private readonly IMapper _mapper;
        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMedia media, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _media = media;
            _mapper = mapper;
        }
        public async Task<Result<BrandDTOs>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brandRepo = _unitOfWork.GetRepository<Brand>();
            var brand = await brandRepo.GetByIdAsync(request.Id);
            if (brand == null) return Result<BrandDTOs>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id)); ;
            Result<ImageUpload> uploadResult = null;
            if (!(request.Image is null))
            {
                uploadResult = await _media.UploadLoadImageAsync(request.Image, UploadFolderConstants.FolderBrand, cancellationToken);
            }
            brand.UrlSlug = request.UrlSlug;
            brand.Name = request.Name;
            brand.Description = request.Description;
            if (!(uploadResult is null))
            {
                brand.LogoImageUrl = uploadResult.Data.Url;
            }
            await _unitOfWork.Commit();
            var brandDTO = _mapper.Map<BrandDTOs>(brand);
            return Result<BrandDTOs>.ResultSuccess(brandDTO);
        }
    }
}
