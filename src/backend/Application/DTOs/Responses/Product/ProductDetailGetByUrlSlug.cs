using Application.DTOs.Responses.Product.ProductImage;
using Application.DTOs.Responses.Product.Variants;

namespace Application.DTOs.Responses.Product
{
    public record ProductDetailGetByUrlSlug : BaseDTO
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string? UnitPrice { get; init; }
        public int? Discount { get; init; }
        public string? WeightUnit { get; set; }
        public int? Weight { get; set; }
        public Guid BrandId { get; init; }
        public string BrandUrlSlug { get; init; }
        public Decimal Price { get; init; }
        public string UrlSlug { get; init; }
        public IEnumerable<ProductImageDTO> Images { get; init; }
        public IEnumerable<VariantsDTO> Variants { get; init; }
        public double? Rate {  get; init; }
        public int TotalRate {  get; init; }
    }
}
