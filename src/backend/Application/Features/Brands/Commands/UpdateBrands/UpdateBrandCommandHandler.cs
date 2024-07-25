using Application.Common.Interface;
using Application.DTOs.Internal;
using AutoMapper;
using MediatR;
using FluentValidation;
using Domain.Constants;
using Domain.Shared;
using Application.DTOs.Responses.Brands;
using Domain.Entities.Brands;
using Application.Common.Exceptions;
using Application.Features.Brands.Specification;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result<BrandDTO>>
    {
        internal class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
        {
            public UpdateBrandCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage(nameof(UpdateBrandCommand.Id));
                RuleFor(b => b.Name).NotEmpty().WithMessage(nameof(UpdateBrandCommand.Name));
                RuleFor(b => b.Description).NotEmpty().WithMessage(nameof(UpdateBrandCommand.Name));
                RuleFor(b => b.UrlSlug).NotEmpty().WithMessage(nameof(UpdateBrandCommand.UrlSlug));
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
            if (brand == null) return Result<BrandDTO>.ResultFailures(ErrorConstants.ApplicationUserError.UserNotFoundWithID(request.Id));
            var isExisted = await repo.FindOneAsync(new UrlSlugIsExistedSpecification(request.Id, request.UrlSlug));
            if (isExisted is not null)
            {
                return Result<BrandDTO>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            if (request.Image is not null)
            {
                Result<ImageUpload> uploadResult = await _media.UploadLoadImageAsync(request.Image, UploadFolderConstants.FolderBrand, cancellationToken);
                if (uploadResult.IsSuccess is false)
                {
                    throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
                }
                brand.Image = uploadResult.Data.PublicId;
            }
            brand.UrlSlug = request.UrlSlug;
            brand.Name = request.Name;
            brand.Description = request.Description;
            await _unitOfWork.Commit();
            var brandDTO = _mapper.Map<BrandDTO>(brand);
            return Result<BrandDTO>.ResultSuccess(brandDTO);
        }
    }
}
