using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Brands.Commands.CreateBrands;
using Application.Features.Category.Specification;
using Application.Utils;
using Domain.Constants;
using Domain.Entities.Category;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Category.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<bool>>
    {
        public class CreateCategoryCommandValidator : AbstractValidator<CreateBrandCommand>
        {
            public CreateCategoryCommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage(nameof(CreateBrandCommand.Name));
                RuleFor(x => x.Description).NotEmpty().WithMessage(nameof(CreateBrandCommand.Description));
                RuleFor(x => x.FormFile).NotEmpty().WithMessage(nameof(CreateBrandCommand.FormFile));
                RuleFor(x => x.UrlSlug)
                    .NotEmpty()
                    .WithMessage(nameof(CreateBrandCommand.UrlSlug))
                    .MustAsync(ValidationExtension.ValidateSlug)
                    .WithMessage(ErrorConstants.UrlSlugInvalid.Description);
            }
        }
        private readonly IMedia _media;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IMedia media, IUnitOfWork unitOfWork)
        {
            _media = media;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var repoCategory = _unitOfWork.GetRepository<Categories>();
            var isExisted = await repoCategory.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted != null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            if (request.ParrentId is not null)
            {
                var isExistedParrent = await repoCategory.GetByIdAsync(request.ParrentId);
                if (isExistedParrent is null)
                {
                    return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId((Guid)request.ParrentId));
                }
            }
            Result<ImageUpload> uploadResult = await _media.UploadLoadImageAsync(request.FormFile, UploadFolderConstants.FolderCategory);
            if (uploadResult.IsSuccess is false)
            {
                throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
            }
            repoCategory.Add(new Categories() { Name = request.Name, Description = request.Description, UrlSlug = request.UrlSlug, Image = uploadResult.Data.PublicId, ParrentId = request.ParrentId });
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
