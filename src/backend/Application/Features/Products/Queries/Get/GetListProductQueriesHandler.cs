using Application.Common.Interface;
using Domain.Shared;
using MediatR;
using Application.Features.Products.Specification;
using Domain.Entities.Products;
using Application.DTOs.Responses.Product.Client;
using AutoMapper;
using Application.DTOs.Responses.Product.Shared.BrandProduct;
using Application.DTOs.Responses.Product.Shared.CategoryProduct;


namespace Application.Features.Products.Queries.Get
{
    public class GetListProductQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetListProductQuery, Result<IEnumerable<ProductDTO>>>
    {
        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            var productRepo = unitOfWork.GetRepository<Product>();
            var getProductSpecification = new GetProductsSpecification(request.ProductFilter);
            var products = await productRepo.GetAllAsync(getProductSpecification);
            var totalItems = await productRepo.CountAsync(getProductSpecification);
            return new PagingResult<IEnumerable<ProductDTO>>(products.Select(x => new ProductDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UrlSlug = x.UrlSlug,
                Discount = x.Discount,
                Price = x.Price,
                OldPrice = x.OldPrice,
                IsStock=x.IsStock,
                Brand = mapper.Map<BrandProductDTO>(x.Brand),
                Category = mapper.Map<CategoryProductDTO>(x.Category),
                Rate = x.Rattings.Count() > 0 ? x.Rattings.Average(r => r.Rate) : 0,
                TotalRate = x.Rattings.Count(),
                Images = x.Images.Select(p => p.ImageUrl).ToList(),
            })
                , request.ProductFilter.PageNumber
                , request.ProductFilter.PageSize
                , totalItems); ;
        }
    }
}
