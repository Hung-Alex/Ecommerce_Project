using Application.Common.Interface;
using Application.DTOs.Responses.Product;
using Application.DTOs.Responses.Product.ProductImage;
using Application.DTOs.Responses.Product.Variants;
using Application.Features.Products.Specification;
using Domain.Constants;
using Domain.Entities.Products;
using Domain.Shared;
using MediatR;


namespace Application.Features.Products.Queries.GetByUrlSlug
{
    public sealed class GetProductByUrlSlugQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductByUrlSlugQuery, Result<ProductDetailGetByUrlSlug>>
    {

        public async Task<Result<ProductDetailGetByUrlSlug>> Handle(GetProductByUrlSlugQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Product>();
            var product = await repo.FindOneAsync(new GetProductDetailsByUrlSlugSpecification(request.UrlSlug));
            if (product == null)
            {
                return Result<ProductDetailGetByUrlSlug>.ResultFailures(ErrorConstants.ProductNotFoundWithSlug(request.UrlSlug));
            }
            var productDto = new ProductDetailGetByUrlSlug()
            {
                Id = product.Id,
                Name = product.Name,
                UrlSlug = product.UrlSlug,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Weight = product.Weight,
                WeightUnit = product.WeightUnit,
                BrandId = product.BrandId,
                BrandUrlSlug = product.Brand.UrlSlug,
                Price = product.Price,
                Images = product.Images.Select(x => new ProductImageDTO() { Id = x.Id, Order = x.OrderItem, Image = x.Image.ImageUrl }),
                Variants = product.ProductSkus.Select(x => new VariantsDTO() { Id = x.Id, VariantName = x.Name, Description = x.Description, Price = x.Price, Quantity = x.Quantity }),
                Discount = product.Discount,
                Rate = product.Rattings.Count() > 0 ? product.Rattings.Average(x => x.Rate) : 0,
                TotalRate = product.Rattings.Count()
            };
            return Result<ProductDetailGetByUrlSlug>.ResultSuccess(productDto);
        }
    }
}
