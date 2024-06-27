using Application.DTOs.Responses.Slides.SlideImages;

namespace Application.DTOs.Responses.Slides
{
    public record SlideDTO : BaseDTO
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public int Order { get; init; }
        public bool? Status { get; init; }
        public IEnumerable<SlideImageDTO> Images { get; init; }
    }
}
