using Application.Common.Interface;
using Application.DTOs.Filters.Search;
using Application.DTOs.Internal.Product;
using Domain.Shared;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly StoreDbContext _context;
        public SearchService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<PagingResult<IEnumerable<ProductInternal>>> SearchProductAsync(SearchFilter query, CancellationToken cancellationToken = default)
        {
            return null;
        }
    }
}
