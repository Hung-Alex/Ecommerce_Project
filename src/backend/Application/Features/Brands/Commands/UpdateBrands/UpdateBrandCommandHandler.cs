using Application.Common.Interface;
using Application.DTOs.Internal;
using AutoMapper;
using MediatR;
using FluentValidation;
using Domain.Constants;
using Domain.Shared;
using Application.DTOs.Responses.Brands;
using Domain.Entities.Brands;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result<BrandDTO>>
    {
        internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateBrandCommand>
        {
            public UpdateCategoryCommandValidator()
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
        public async Task<Result<BrandDTO>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Brand>();
            var brand = await repo.GetByIdAsync(request.Id);
            if (brand == null) return Result<BrandDTO>.ResultFailures(ErrorConstants.UserNotFoundWithID(request.Id));
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
                brand.Image = uploadResult.Data.Url;
            }
            await _unitOfWork.Commit();
            var brandDTO = _mapper.Map<BrandDTO>(brand);
            return Result<BrandDTO>.ResultSuccess(brandDTO);
        }
    }
}
