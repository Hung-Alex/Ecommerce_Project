using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Brands.Specification;
using Application.Utils;
using Domain.Constants;
using Domain.Entities.Brands;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Brands.Commands.CreateBrands
{
    public sealed class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<bool>>
    {
        public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
        {
            public CreateBrandCommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage(nameof(CreateBrandCommand.Name));
                RuleFor(x => x.Description).NotEmpty().WithMessage(nameof(CreateBrandCommand.Description));
                RuleFor(x => x.FormFile).NotEmpty().WithMessage(nameof(CreateBrandCommand.FormFile));
                RuleFor(x => x.UrlSlug)
                    .NotEmpty()
                    .WithMessage(nameof(CreateBrandCommand.UrlSlug))
                    .MustAsync(ValidationExtension.ValidateSlug)
                    .WithMessage(ErrorConstants.UrlSlugInvalid.Description); ;
            }
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
            if (isExisted is not null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            Result<ImageUpload> uploadResult = await _media.UploadLoadImageAsync(request.FormFile, UploadFolderConstants.FolderBrand); ;
            if (uploadResult.IsSuccess is false)
            {
                throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
            }
            repo.Add(new Brand() { Name = request.Name, Description = request.Description, UrlSlug = request.UrlSlug, Image = uploadResult.Data.PublicId });
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
