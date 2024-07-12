using Application.DTOs.Filters.Brands;
using Application.DTOs.Responses.Brands;
using Domain.Shared;
using MediatR;

namespace Application.Features.Brands.Queries.Get
{
    public record GetListBrandsQuery(BrandFilter BrandFilter) : IRequest<Result<IEnumerable<BrandDTO>>>;
}
