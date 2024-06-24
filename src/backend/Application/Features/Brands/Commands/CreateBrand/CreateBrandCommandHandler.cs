using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Brands.Specification;
using Domain.Constants;
using Domain.Entities.Brands;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Brands.Commands.CreateBrand
{
    public sealed class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<bool>>
    {
        internal class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommandValidator>
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
            var repoBranch = _unitOfWork.GetRepository<Brand>();
            var isExisted = await repoBranch.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted != null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            ImageUpload image = new ImageUpload(null, null);
            if (request.FormFile is not null)
            {
                image = await _media.UploadLoadImageAsync(request.FormFile);
            }
            repoBranch.Add(new Brand() { Name = request.Name, Description = request.Description, UrlSlug = request.UrlSlug, LogoImageUrl = image.Url });
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
