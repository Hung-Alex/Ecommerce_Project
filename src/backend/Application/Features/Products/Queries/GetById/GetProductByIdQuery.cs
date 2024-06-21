using Application.DTOs.Responses.Brand;
using MediatR;

namespace Application.Features.Products.Queries.GetById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<BrandDTOs>;
}
