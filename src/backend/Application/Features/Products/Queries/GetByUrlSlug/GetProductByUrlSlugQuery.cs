using Application.DTOs.Responses.Product.Client;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Queries.GetByUrlSlug
{
    public record GetProductByUrlSlugQuery(string UrlSlug) : IRequest<Result<ProductGetBySlugDTO>>;
}
