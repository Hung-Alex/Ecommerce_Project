﻿using Application.Common.Interface;
using Application.DTOs.Internal;
using MediatR;
using FluentValidation;
using Domain.Constants;
using Domain.Shared;
using Domain.Entities.Posts;
using Application.Features.Posts.Specification;

namespace Application.Features.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Result<bool>>
    {
        internal class UpdateCategoryCommandValidator : AbstractValidator<UpdatePostCommand>
        {
            public UpdateCategoryCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.Description).NotEmpty().WithMessage("Not Null");
                RuleFor(b => b.UrlSlug).NotEmpty().WithMessage("Not Null");
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
            if (post == null) return Result<bool>.ResultFailures(ErrorConstants.UserNotFoundWithID(request.Id));
            var isExisted = await repo.FindOneAsync(new UrlSlugIsExistedSpecification(post.Id, request.UrlSlug));
            if (isExisted != null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            Result<ImageUpload> uploadResult = null;
            if (!(request.Image is null))
            {
                uploadResult = await _media.UploadLoadImageAsync(request.Image, UploadFolderConstants.FolderCategory, cancellationToken);
            }
            post.UrlSlug = request.UrlSlug;
            post.ShortDescription = request.ShortDescription;
            post.Description = request.Description;
            post.Title=request.Title;
            post.Meta = request.Meta;
            post.Pulished = request.Pulished;
            if (!(uploadResult is null))
            {
                post.ImageUrl = uploadResult.Data.Url;
            }
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}