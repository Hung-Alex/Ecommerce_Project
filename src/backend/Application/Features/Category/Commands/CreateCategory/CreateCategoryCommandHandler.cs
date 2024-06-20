using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Category.Specification;
using Domain.Constants;
using Domain.Entities.Category;
using FluentValidation;
using MediatR;


namespace Application.Features.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
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
        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var repoCategory = _unitOfWork.GetRepository<Categories>();
            var isExisted = await repoCategory.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted != null)
            {
                throw new ConflictException(ErrorConstants.UrlSlugIsExisted);
            }
            ImageUpload image = new ImageUpload(null, null);
            if (request.FormFile is not null)
            {
                image = await _media.UploadLoadImageAsync(request.FormFile);
            }
            repoCategory.Add(new Categories() { Name = request.Name, Description = request.Description, UrlSlug = request.UrlSlug, Image = image.Url });
            await _unitOfWork.Commit();
        }
    }
}
