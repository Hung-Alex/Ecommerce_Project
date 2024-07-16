using Application.DTOs.Responses.Category;
using Application.DTOs.Responses.Product;

namespace Application.DTOs.Responses.Sections
{
    public record CategorySection : BaseDTO
    {
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Image {  get; set; }
    }

    public record SectionDTO
    {
        public CategorySection Category { get; init; }
        public IEnumerable<ProductDTO> products { get; init; }
    }
}
