
namespace Application.DTOs.Responses.Slides
{
    public record SlideDTO : BaseDTO
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public bool IsActive { get; init; }
        public IEnumerable<string> Images { get; init; }
    }
}
