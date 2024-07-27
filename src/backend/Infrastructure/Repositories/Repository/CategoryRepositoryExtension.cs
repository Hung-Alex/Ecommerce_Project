using Application.Common.Interface.RepositoryExtension;
using Application.DTOs.Responses.Product.Client;
using Application.DTOs.Responses.Product.Shared.BrandProduct;
using Application.DTOs.Responses.Product.Shared.CategoryProduct;
using Application.DTOs.Responses.Sections;
using Domain.Entities.Category;
using Infrastructure.Data;
using Infrastructure.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Repository
{
    public class CategoryRepositoryExtension : BaseRepository<Categories>, ICategoryRepositoryExtension
    {
        public CategoryRepositoryExtension(StoreDbContext context) : base(context) { }

        public async Task<IEnumerable<SectionDTO>> GetSectionsAsync(int limitCategory = 5, int limitProduct = 5, CancellationToken cancellationToken = default)
        {
            var query = from cate in _context.Categories
                        where cate.ParrentId == null
                        select new SectionDTO
                        {
                            Category = new CatetgorySection { Id = cate.Id, Name = cate.Name, UrlSlug = cate.UrlSlug, TotalItems = cate.Products.Count() },
                            products = _context.Products
                               .Include(x => x.Brand)
                               .Include(x => x.Rattings)
                               .Include(x => x.Images)
                               .Where(p => p.CategoryId == cate.Id).Select(x => new ProductDTO
                               {
                                   Id = x.Id,
                                   Name = x.Name,
                                   Description = x.Description,
                                   UrlSlug = x.UrlSlug,
                                   Discount = x.Discount,
                                   Price = x.Price,
                                   Category = new CategoryProductDTO { Id = cate.Id, Name = cate.Name, UrlSlug = cate.UrlSlug },
                                   Brand = new BrandProductDTO { Id = x.Brand.Id, Name = x.Brand.Name, UrlSlug = x.Brand.UrlSlug },
                                   Rate = x.Rattings.Count() > 0 ? x.Rattings.Average(r => r.Rate) : 0,
                                   TotalRate = x.Rattings.Count(),
                                   Images = x.Images.Select(p => p.ImageUrl).ToList(),
                               }).Take(limitProduct).ToList()


                        };
            var result = await query.Take(limitCategory).ToListAsync(cancellationToken);
            return result;
        }
    }
}
