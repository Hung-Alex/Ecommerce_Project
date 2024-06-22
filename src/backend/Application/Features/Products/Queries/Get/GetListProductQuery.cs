using Application.DTOs.Filters.Product;
using Application.DTOs.Responses.Product;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Queries.Get
{
    public record GetListProductQuery(ProductFilter ProductFilter) : IRequest<Result<IEnumerable<ProductDTO>>>;
}
