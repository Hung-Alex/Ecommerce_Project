using Application.Common.Interface;
using Application.DTOs.Internal;
using AutoMapper;
using MediatR;
using FluentValidation;
using Application.DTOs.Responses.Category;
using Domain.Entities.Category;
using Domain.Constants;
using Domain.Shared;
using Application.Features.Brands.Commands.CreateBrands;
using Application.Common.Exceptions;
using Application.Features.Category.Specification;

namespace Application.Features.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryDTO>>
    {
        internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
        {
            public UpdateCategoryCommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage(nameof(CreateBrandCommand.Name));
                RuleFor(x => x.Description).NotEmpty().WithMessage(nameof(CreateBrandCommand.Description));
                RuleFor(x => x.UrlSlug).NotEmpty().WithMessage(nameof(CreateBrandCommand.UrlSlug));
            }
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        private readonly IMapper _mapper;
        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMedia media, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _media = media;
            _mapper = mapper;
        }
        public async Task<Result<CategoryDTO>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var repoCategory = _unitOfWork.GetRepository<Categories>();
            var category = await repoCategory.GetByIdAsync(request.Id);
            if (category == null) return Result<CategoryDTO>.ResultFailures(ErrorConstants.ApplicationUserError.UserNotFoundWithID(request.Id));
            var isExisted = await repoCategory.FindOneAsync(new UrlSlugIsExistedSpecification(category.Id, request.UrlSlug));
            if (isExisted != null)
            {
                return Result<CategoryDTO>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            if (request.ParrentId is not null)
            {
                var isExistedParrent = await repoCategory.GetByIdAsync(request.ParrentId);
                if (isExistedParrent is null)
                {
                    return Result<CategoryDTO>.ResultFailures(ErrorConstants.NotFoundWithId((Guid)request.ParrentId));
                }
                category.ParrentId = request.ParrentId;

            }
            if (request.Image is not null)
            {
                Result<ImageUpload> uploadResult = await _media.UploadLoadImageAsync(request.Image, UploadFolderConstants.FolderCategory, cancellationToken);
                if (uploadResult.IsSuccess is false)
                {
                    throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
                }
                category.Image = uploadResult.Data.PublicId;
            }
            category.UrlSlug = request.UrlSlug;
            category.Name = request.Name;
            category.Description = request.Description;
            await _unitOfWork.Commit();
            var CategoryDTO = _mapper.Map<CategoryDTO>(category);
            return Result<CategoryDTO>.ResultSuccess(CategoryDTO);
        }
    }
}
