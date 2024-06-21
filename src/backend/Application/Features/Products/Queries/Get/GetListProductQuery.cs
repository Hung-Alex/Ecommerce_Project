using Application.DTOs.Filters.Brand;
using Application.DTOs.Responses.Brand;
using Domain.Shared;
using MediatR;


namespace Application.Features.Products.Queries.Get
{
    public record GetListProductQuery(BrandFilter ProductFilter) : IRequest<Result<IEnumerable<BrandDTOs>>>;

}
