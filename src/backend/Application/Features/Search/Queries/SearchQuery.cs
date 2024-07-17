using Application.DTOs.Filters.Search;
using Application.DTOs.Responses.Product.Client;
using Domain.Shared;
using MediatR;

namespace Application.Features.Search.Queries
{
    public record SearchQuery(SearchFilter query) : IRequest<Result<IEnumerable<ProductDTO>>>;
}
