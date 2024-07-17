using Application.Common.Interface;
using Application.DTOs.Responses.Product.Client;
using Application.DTOs.Responses.Product.Shared.BrandProduct;
using Application.DTOs.Responses.Product.Shared.CategoryProduct;
using Application.Features.Products.Specification;
using Application.Features.Search.Specification;
using AutoMapper;
using Domain.Entities.Products;
using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Features.Search.Queries
{
    public sealed class SearchQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<SearchQuery, Result<IEnumerable<ProductDTO>>>
    {

        internal class SearchQueyValidator : AbstractValidator<SearchQuery>
        {
            public SearchQueyValidator()
            {
               
            }
        }
        public async Task<Result<IEnumerable<ProductDTO>>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Product>();
            var specification = new FilterProductSpecification(request.query);
            var result = await repo.GetAllAsync(specification, cancellationToken);
            var totalItems = await repo.CountAsync(specification);
            return new PagingResult<IEnumerable<ProductDTO>>(result.Select(x => new ProductDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UrlSlug = x.UrlSlug,
                Discount = x.Discount,
                Price = x.Price,
                Brand = mapper.Map<BrandProductDTO>(x.Brand),
                Category = mapper.Map<CategoryProductDTO>(x.Category),
                Rate = x.Rattings.Count() > 0 ? x.Rattings.Average(r => r.Rate) : 0,
                TotalRate = x.Rattings.Count(),
                Images = x.Images.Select(p => p.ImageUrl).ToList(),
            })
                , request.query.PageNumber
                , request.query.PageSize
                , totalItems); ;
        }
    }
}
