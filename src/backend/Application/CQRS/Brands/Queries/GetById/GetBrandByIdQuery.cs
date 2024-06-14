using Application.DTOs.Responses.Brand;
using MediatR;

namespace Application.CQRS.Brands.Queries.GetById
{
    public record GetBrandByIdQuery(Guid Id) : IRequest<BrandDTOs>;
}
