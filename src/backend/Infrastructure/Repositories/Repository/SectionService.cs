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
            var sections = from c in _context.Categories
                           join p in _context.Products on c.Id equals p.Id
                           join productImage in _context.ProductImages.Include(x => x.Image) on p.Id equals productImage.Id
                           join image in _context.Images on productImage.ImageId equals image.Id
                           where c.ParrentId == null
                           group new { c, productImage, p, image } by new { c.Id, c.Name, c.UrlSlug, c.Image } into g
                           select new SectionDTO
                           {
                               Category = new CategorySection()
                               {
                                   Id = g.Key.Id
                                   ,
                                   Name = g.Key.Name
                                   ,
                                   UrlSlug = g.Key.UrlSlug
                                   ,
                                   Image = g.Key.Image
                                   ,
                                   SubCategories = _context.Categories.Where(x => x.ParrentId == g.Key.Id).Select(sc => new CategorySection
                                   {
                                       Id = sc.Id,
                                       Name = sc.Name,
                                       UrlSlug = sc.UrlSlug,
                                       Image = sc.Image
                                   }).ToList(),
                               },
                               products = g.Select(x => new ProductDTO
                               {
                                   Id = x.p.Id,
                                   Name = x.p.Name,
                                   UrlSlug = x.p.UrlSlug,
                                   Images = x.image.ProductImages.Select(x => x.Image.ImageUrl).ToList(),
                               }).ToList()
                           };


            var result = await sections.ToListAsync(cancellationToken);




            return result;
        }
    }
}
