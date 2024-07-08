using Application.DTOs.Responses.Sections;

namespace Application.Common.Interface
{
    public interface ISectionService
    {
        Task<IEnumerable<SectionDTO>> GetSectionsAsync(int takeNumberCategories, int limitNumberItems, CancellationToken cancellationToken = default);
    }
}
