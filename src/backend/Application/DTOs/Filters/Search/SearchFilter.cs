
namespace Application.DTOs.Filters.Search
{
    public record SearchFilter : SpecificationParams
    {
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public string? ProductName { get; init; }
        public Guid? BrandId { get; init; }
        public Guid? CategoryId { get; init; }
        public string? UrlSlugCategory { get; init;}
        public string? UrlSlugBrand { get; init;}
    }
}
