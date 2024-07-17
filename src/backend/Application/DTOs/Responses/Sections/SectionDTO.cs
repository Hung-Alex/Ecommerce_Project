using Application.DTOs.Responses.Product.Client;

namespace Application.DTOs.Responses.Sections
{
  
    public record SectionDTO
    {
        public CatetgorySection Category { get; init; }
        public IEnumerable<ProductDTO> products { get; init; }
    }
}
