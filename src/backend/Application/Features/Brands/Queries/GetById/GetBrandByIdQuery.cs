using Application.DTOs.Responses.Brand;
using Domain.Shared;
using MediatR;

namespace Application.Features.Brands.Queries.GetById
{
    public record GetBrandByIdQuery(Guid Id) : IRequest<Result<BrandDTOs>>;
}
