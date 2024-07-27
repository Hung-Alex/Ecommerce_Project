using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Brands.Commands.CreateBrands;
using Application.Features.Posts.Specification;
using Application.Utils;
using Domain.Constants;
using Domain.Entities.Posts;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Posts.Commands.CreatePost
{
    public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<bool>>
    {
        public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
        {
            public CreatePostCommandValidator()
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
            Result<ImageUpload> uploadResult = await _media.UploadLoadImageAsync(request.Image, UploadFolderConstants.FolderCategory);
            if (uploadResult.IsSuccess is false)
            {
                throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
            }
            repo.Add(new Post()
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Description = request.Description,
                UrlSlug = request.UrlSlug,
                ImageUrl = uploadResult.Data.PublicId,
                Pulished = request.Pulished,
                ViewCount = 0
            });
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
