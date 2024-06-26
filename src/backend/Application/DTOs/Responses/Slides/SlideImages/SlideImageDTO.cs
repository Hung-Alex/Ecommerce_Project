
namespace Application.DTOs.Responses.Slides.SlideImages
{
    public record SlideImageDTO : BaseDTO
    {
        public int Order {  get; set; }
        public string Image {  get; init; }
    }
}
