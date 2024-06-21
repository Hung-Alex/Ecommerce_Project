using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities.Products;
using FluentValidation;
using MediatR;


namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        internal class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommandValidator>
        {

        }
        private readonly IMedia _media;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IMedia media, IUnitOfWork unitOfWork)
        {
            _media = media;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var repoProduct = _unitOfWork.GetRepository<Product>();
            var isExisted = await repoProduct.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted != null)
            {
                throw new ConflictException(ErrorConstants.UrlSlugIsExisted);
            }
            ImageUpload image = new ImageUpload(null, null);
            if (request.FormFile is not null)
            {
                image = await _media.UploadLoadImageAsync(request.FormFile);
            }
            repoProduct.Add(new Product() { Name = request.Name, Description = request.Description });
            await _unitOfWork.Commit();
        }
    }
}
