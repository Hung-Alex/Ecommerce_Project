using Application.DTOs.Responses.Product.Shared.BrandProduct;
using Application.DTOs.Responses.Product.Shared.CategoryProduct;
using Application.DTOs.Responses.Product.Shared.Variants;

namespace Application.DTOs.Responses.Product.Client
{
    public record ProductGetBySlugDTO : BaseDTO
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string UrlSlug { get; init; }
        public int? Discount { get; init; }
        public decimal Price { get; init; }
        public double Rate { get; init; }
        public int TotalRate { get; init; }
        public CategoryProductDTO Category { get; init; }
        public BrandProductDTO Brand { get; init; }
        public IEnumerable<VariantsDTO> Variants { get; init; }
        public IEnumerable<string> Images { get; set; }
    }
}
