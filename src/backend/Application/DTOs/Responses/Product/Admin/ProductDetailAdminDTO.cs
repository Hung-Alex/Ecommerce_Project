using Application.DTOs.Responses.Images;
using Application.DTOs.Responses.Product.Shared.Variants;

namespace Application.DTOs.Responses.Product.Admin
{
    public record ProductDetailAdminDTO:BaseDTO
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public Decimal Price { get; init; }
        public string UrlSlug { get; init; }
        public int? Discount { get; init; }
        public Guid BrandId { get; init; }
        public Guid CategoryId { get; init; }
        public virtual IEnumerable<ImageDTO> Images { get; init; } 
        public virtual IEnumerable<VariantsDTO> Variants { get; init; } 
    }
}
