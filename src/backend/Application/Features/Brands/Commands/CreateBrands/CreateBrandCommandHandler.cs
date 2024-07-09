using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Brands.Specification;
using Domain.Constants;
using Domain.Entities.Brands;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Brands.Commands.CreateBrands
{
    public sealed class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<bool>>
    {
        internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandValidator>
        {

        }
        private readonly IMedia _media;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBrandCommandHandler(IMedia media, IUnitOfWork unitOfWork)
        {
            _media = media;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Brand>();
            var isExisted = await repo.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted != null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            Result<ImageUpload> image = null;
            if (request.FormFile is not null)
            {
                image = await _media.UploadLoadImageAsync(request.FormFile, UploadFolderConstants.FolderBrand);
            }
            repo.Add(new Brand() { Name = request.Name, Description = request.Description, UrlSlug = request.UrlSlug, Image = image.Data.Url });
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
