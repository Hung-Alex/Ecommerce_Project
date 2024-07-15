using Application.Common.Interface;
using Application.DTOs.Responses.Product;
using Application.DTOs.Responses.Sections;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Repository
{
    public sealed class SectionService : ISectionService
    {
        private readonly StoreDbContext _context;
        public SectionService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SectionDTO>> GetSectionsAsync(int takeNumberCategories, int limitNumberItems, CancellationToken cancellationToken = default)
        {
            //var sections = from cate in _context.Categories.Include(x => x.SubCategories)
            //               where cate.ParrentId==null
            //               select new SectionDTO
            //               {
            //                   Category = new CategorySection
            //                   {
            //                       Id = cate.Id,
            //                       Name = cate.Name,
            //                       UrlSlug = cate.UrlSlug,
            //                       Image = cate.Image,
            //                       SubCategories = cate.SubCategories.Select(x => new CategorySection
            //                       {
            //                           Id = x.Id,
            //                           Name = x.Name,
            //                           UrlSlug = x.UrlSlug,
            //                           Image = x.Image
            //                       }).ToList(),
            //                   },
            //                   products = (from p in _context.Products
            //                              join productCate in _context.ProductSubCategories on p.Id equals productCate.ProductId
            //                              where productCate.CategoryId == cate.Id
            //                              group p by new { p.Id, p.Name, p.UnitPrice, p.UrlSlug, p.Discount } into produc
            //                              select new ProductDTO
            //                              {
            //                                  Id = produc.Key.Id
            //                                  ,
            //                                  Name = produc.Key.Name,
            //                                  UnitPrice = produc.Key.UnitPrice,
            //                                  UrlSlug= produc.Key.UrlSlug,
            //                                  Discount = produc.Key.Discount,
            //                                  Images = _context.ProductImages
            //                                  .Include(x => x.Image)
            //                                  .Where(x => x.ProductId == produc.Key.Id)
            //                                  .Select(x => x.Image.ImageUrl).ToList(),
            //                              }).Take(limitNumberItems).ToList()
            //               };

            //var result = await sections.Take(takeNumberCategories).ToListAsync(cancellationToken);
            //return result;
            return null;
        }
    }
}
