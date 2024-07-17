using Application.DTOs.Responses.Product.Admin;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Queries.GetById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDetailAdminDTO>>;
}
