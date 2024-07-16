using System.Text.Json.Serialization;

namespace Application.DTOs.Responses.Product
{
    public record ProductDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlSlug { get; set; }
        public int? Discount { get; set; }
        public Decimal Price { get; set; }
        public Guid BrandId { get; init; }
        public Guid CategoryId { get; init; }
        public string Image { get; set; }
    }
}
