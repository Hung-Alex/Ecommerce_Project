using Domain.Entities.Category;
using Domain.Interface;
using Infrastructure.Data;
using Infrastructure.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Repository.Category
{
    public class CategoryRepository : BaseRepository<Categories>, ICategoryRepository
    {
        public CategoryRepository(StoreDbContext context) : base(context) { }

        public async Task<bool> IsExistedAsync(Guid CategoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.AnyAsync(c => c.Id == CategoryId);
        }

        public async Task<bool> IsSubCategoryOfParrentAsync(Guid SubCategoryId, Guid ParrentId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.AnyAsync(c => c.Id == SubCategoryId && c.ParrentId == ParrentId);
        }
    }
}
