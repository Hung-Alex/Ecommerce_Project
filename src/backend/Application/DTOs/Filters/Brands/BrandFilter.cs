namespace Application.DTOs.Filters.Brands
{
    public record BrandFilter : SpecificationParams
    {
        public string? Name { get; set; }
    }
}
