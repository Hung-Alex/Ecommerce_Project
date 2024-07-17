using Application.DTOs.Responses.Sections;

namespace Application.Common.Interface
{
    public interface ISectionService
    {
        Task<IEnumerable<SectionDTO>> GetSectionsAsync(int limitCategory = 5, int limitProduct = 5, CancellationToken cancellationToken = default);
    }
}
