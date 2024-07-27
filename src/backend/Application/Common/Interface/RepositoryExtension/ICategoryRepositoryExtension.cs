using Application.DTOs.Responses.Sections;
using Domain.Entities.Category;
using Domain.Interface;

namespace Application.Common.Interface.RepositoryExtension
{
    public interface ICategoryRepositoryExtension : IRepository<Categories>
    {
        Task<IEnumerable<SectionDTO>> GetSectionsAsync(int limitCategory = 5, int limitProduct = 5, CancellationToken cancellationToken = default);
    }
}
