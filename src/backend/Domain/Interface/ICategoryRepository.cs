using Domain.Entities.Category;

namespace Domain.Interface
{
    public interface ICategoryRepository : IRepository<Categories>
    {
        Task<bool> IsExistedAsync(Guid CategoryId, CancellationToken cancellationToken = default);
        Task<bool> IsSubCategoryOfParrentAsync(Guid SubCategoryId, Guid ParrentId, CancellationToken cancellationToken = default);
    }
}
