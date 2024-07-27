using Application.Common.Interface;
using Application.Common.Interface.RepositoryExtension;
using Application.DTOs.Responses.Sections;

namespace Application.Common.Implementation
{
    public class SectionService : ISectionService
    {
        private readonly ICategoryRepositoryExtension _categoryRepository;
        public SectionService(ICategoryRepositoryExtension categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<SectionDTO>> GetSectionsAsync(int limitCategory = 5, int limitProduct = 5, CancellationToken cancellationToken = default)
        {
            var sections = await _categoryRepository.GetSectionsAsync(limitCategory, limitProduct, cancellationToken);
            return sections;
        }
    }
}
