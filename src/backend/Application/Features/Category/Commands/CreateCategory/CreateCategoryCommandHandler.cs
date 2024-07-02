using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Category.Specification;
using Domain.Constants;
using Domain.Entities.Category;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Category.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<bool>>
    {
        internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandValidator>
        {

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
            Result<ImageUpload> image = null;
            if (request.FormFile is not null)
            {
                image = await _media.UploadLoadImageAsync(request.FormFile, UploadFolderConstants.FolderCategory);
            }
            repoCategory.Add(new Categories() { Name = request.Name, Description = request.Description, UrlSlug = request.UrlSlug, Image = image.Data.Url, ParrentId = request.ParrentId });
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
