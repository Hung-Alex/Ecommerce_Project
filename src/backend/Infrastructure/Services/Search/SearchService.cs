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
            #region query
            var queryProduct = (from p in _context.Products.Include(x => x.ProductSubCategories)
                                select new ProductInternal
                                {
                                    Id = p.Id,
                                    Name = p.Name,
                                    Price = p.Price,
                                    UnitPrice = p.UnitPrice,
                                    BrandId = p.BrandId,
                                    CollectionId = p.ProductSubCategories.Select(x => x.Id).ToList(),
                                    Images = _context.Images
                                            .Join(_context.ProductImages, image => image.Id, productImg => productImg.ImageId, (image, productImg) => new { image, productImg })
                                            .Where(joined => joined.productImg.ProductId == p.Id)
                                            .Select(joined => joined.image.ImageUrl)
                                            .ToList()

                                });
            #endregion
            #region Condition filler
            if (query.CategoryId is not null)
            {
                queryProduct = queryProduct.Where(x => x.CollectionId.Any(c => c == query.CategoryId));
            }
            if (query.ProductName is not null)
            {
                queryProduct = queryProduct.Where(x => x.Name.Contains(query.ProductName));
            }
            if (query.MinPrice is not null)
            {
                queryProduct = queryProduct.Where(x => x.Price >= query.MinPrice);
            }
            if (query.MaxPrice is not null)
            {
                queryProduct = queryProduct.Where(x => x.Price <= query.MaxPrice);
            }
            if (query.SortBy == "ASC")
            {
                queryProduct = queryProduct.OrderBy(x => x.Price);
            }
            else
            {
                queryProduct = queryProduct.OrderByDescending(x => x.Price);
            }
            #endregion
            var totalItem = await queryProduct.LongCountAsync(cancellationToken);
            queryProduct = queryProduct.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);
            var result = await queryProduct.ToListAsync(cancellationToken);
            return new PagingResult<IEnumerable<ProductInternal>>(result, query.PageNumber, query.PageSize, totalItem);
        }
    }
}
