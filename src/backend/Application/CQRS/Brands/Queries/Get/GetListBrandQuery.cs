using Application.DTOs.Filters.Brand;
using Application.DTOs.Responses.Brand;
using Domain.Shared;
using MediatR;


namespace Application.CQRS.Brands.Queries.Get
{
    public record GetListBrandQuery(BrandFilter ProductFilter) : IRequest<Result<IEnumerable<BrandDTOs>>>;

}
