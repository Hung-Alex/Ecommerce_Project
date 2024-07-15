using Application.DTOs.Responses.Product.ProductImage;
using Application.DTOs.Responses.Product.Variants;

namespace Application.DTOs.Responses.Product
{
    public record ProductDetailsDTO : BaseDTO
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public int? Discount { get; init; }
        public Guid BrandId { get; init; }
        public Decimal Price { get; init; }
        public string UrlSlug { get; init; }
        public IEnumerable<ProductImageDTO> Images { get; init; }
        public IEnumerable<VariantsDTO> Variants { get; init; }
        public IEnumerable<Guid> CollectionId { get; init; }
    }
}
