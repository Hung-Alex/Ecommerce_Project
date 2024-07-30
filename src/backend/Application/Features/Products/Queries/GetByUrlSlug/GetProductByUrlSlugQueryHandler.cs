using Application.Common.Interface;
using Application.DTOs.Responses.Product.Client;
using Application.DTOs.Responses.Product.Shared.BrandProduct;
using Application.DTOs.Responses.Product.Shared.CategoryProduct;
using Application.Features.Products.Specification;
using AutoMapper;
using Domain.Constants;
using Domain.Entities.Products;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Queries.GetByUrlSlug
{
    public sealed class GetProductByUrlSlugQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProductByUrlSlugQuery, Result<ProductGetBySlugDTO>>
    {
        public async Task<Result<ProductGetBySlugDTO>> Handle(GetProductByUrlSlugQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Product>();
            var product = await repo.FindOneAsync(new GetProductDetailsByUrlSlugSpecification(request.UrlSlug));
            if (product == null)
            {
                return Result<ProductGetBySlugDTO>.ResultFailures(ErrorConstants.ProductError.ProductNotFoundWithSlug(request.UrlSlug));
            }
            var productDto = new ProductGetBySlugDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                UrlSlug = product.UrlSlug,
                Discount = product.Discount,
                Price = product.Price,
                OldPrice = product.OldPrice,
                IsStock = product.IsStock,
                Brand = mapper.Map<BrandProductDTO>(product.Brand),
                Category = mapper.Map<CategoryProductDTO>(product.Category),
                Rate = product.Rattings.Count() > 0 ? product.Rattings.Average(r => r.Rate) : 0,
                TotalRate = product.Rattings.Count(),
                Images = product.Images.Select(p => p.ImageUrl).ToList(),
            };
            return Result<ProductGetBySlugDTO>.ResultSuccess(productDto);
        }
    }
}
