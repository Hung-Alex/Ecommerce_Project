using Application.Common.Interface;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities.Images;
using Domain.Entities.Products;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<bool>>
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
        public async Task<Result<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var repoProduct = _unitOfWork.GetRepository<Product>();
            var repoImage = _unitOfWork.GetRepository<Image>();
            var isExisted = await repoProduct.FindOneAsync(new UrlSlugIsExistedSpecification(Guid.Empty, request.UrlSlug));
            if (isExisted != null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UrlSlugIsExisted(request.UrlSlug));
            }
            var newProduct = new Product()
            {
                Name = request.Name
            ,
                Description = request.Description
            ,
                UrlSlug = request.UrlSlug
            ,
                Price = request.Price
            ,
                UnitPrice = request.UnitPrice
            ,
                Discount = request.Discount
            ,
                BrandId = request.BrandId
            };
            repoProduct.Add(newProduct);
            var image = new Image();
            newProduct.ProductSkus = request
                .Variant
                .Select(x => new ProductSkus() { Name = x.VariantName, Description = x.description, Quantity = x.Quantity })
                .ToList();
            foreach (var item in request.Images)
            {
                var uploadResult = await _media.UploadLoadImageAsync(item.Image, UploadFolderConstants.FolderProduct);
                if (uploadResult.IsSuccess)
                {
                    image = new Image()
                    {
                        ImageExtension = item.Image.ContentType
                    ,
                        ImageUrl = uploadResult.Data.Url
                    ,
                        PublicId = uploadResult.Data.PublicId
                    };
                    repoImage.Add(image);
                    newProduct.Images.Add(new ProductImages() { ImageId = image.Id, ProductId = newProduct.Id, OrderItem = item.Order });
                }
                else
                {
                    return Result<bool>.ResultFailures(ErrorConstants.UploadImageOccursErrorWithFileName(item.Image.FileName));
                }
            }
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
