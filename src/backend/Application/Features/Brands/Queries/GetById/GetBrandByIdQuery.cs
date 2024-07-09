using Application.DTOs.Responses.Brands;
using Domain.Shared;
using MediatR;

namespace Application.Features.Brands.Queries.GetById
{
    public record GetBrandByIdQuery(Guid Id) : IRequest<Result<BrandDTO>>;
}
