using Application.DTOs.Filters.Search;
using Application.DTOs.Internal.Product;
using Domain.Shared;

namespace Application.Common.Interface
{
    public interface ISearchService
    {
        Task<PagingResult<IEnumerable<ProductInternal>>> SearchProductAsync(SearchFilter query, CancellationToken cancellationToken = default);
    }
}
