using Application.Common.Interface;
using Application.DTOs.Internal;
using MediatR;
using FluentValidation;
using Domain.Constants;
using Domain.Shared;
using Domain.Entities.Posts;
using Application.Features.Posts.Specification;
using Application.Common.Exceptions;
using Application.Features.Posts.Commands.CreatePost;
using Application.Utils;
using Application.Features.Brands.Commands.CreateBrands;

namespace Application.Features.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Result<bool>>
    {
        public class UpdateCategoryCommandValidator : AbstractValidator<UpdatePostCommand>
        {
            public UpdateCategoryCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty().WithMessage(nameof(CreatePostCommand.Title));
                RuleFor(x => x.ShortDescription).NotEmpty().WithMessage(nameof(CreatePostCommand.ShortDescription));
                RuleFor(x => x.Description).NotEmpty().WithMessage(nameof(CreatePostCommand.Description));
                RuleFor(x => x.UrlSlug)
                     .NotEmpty()
                     .WithMessage(nameof(CreateBrandCommand.UrlSlug))
                     .MustAsync(ValidationExtension.ValidateSlug)
                     .WithMessage(ErrorConstants.UrlSlugInvalid.Description);
            }
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        public UpdatePostCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media;
        }
        public async Task<Result<bool>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Post>();
            var post = await repo.GetByIdAsync(request.Id);
            if (post == null) return Result<bool>.ResultFailures(ErrorConstants.ApplicationUserError.UserNotFoundWithID(request.Id));
            var isExisted = await repo.FindOneAsync(new UrlSlugIsExistedSpecification(post.Id, request.UrlSlug));
            if (isExisted != null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            if (request.Image is not null)
            {

                Result<ImageUpload> uploadResult = await _media.UploadLoadImageAsync(request.Image, UploadFolderConstants.FolderPost);
                if (uploadResult.IsSuccess is false)
                {
                    throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
                }
                post.ImageUrl = uploadResult.Data.PublicId;
            }
            post.UrlSlug = request.UrlSlug;
            post.ShortDescription = request.ShortDescription;
            post.Description = request.Description;
            post.Title = request.Title;
            post.Published = request.Published;
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
