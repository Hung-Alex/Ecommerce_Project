using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Posts.Specification;
using Domain.Constants;
using Domain.Entities.Posts;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Posts.Commands.CreatePost
{
    public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<bool>>
    {
        internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandValidator>
        {

        }
        private readonly IMedia _media;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostCommandHandler(IMedia media, IUnitOfWork unitOfWork)
        {
            _media = media;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Post>();
            var isExisted = await repo.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted != null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            Result<ImageUpload> image = null;
            if (request.Image is not null)
            {
                image = await _media.UploadLoadImageAsync(request.Image, UploadFolderConstants.FolderPost);
            }
            repo.Add(new Post()
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Description = request.Description,
                UrlSlug = request.UrlSlug,
                Meta = request.Meta,
                Pulished = request.Pulished,
                ImageUrl = image.Data.Url
            });
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
