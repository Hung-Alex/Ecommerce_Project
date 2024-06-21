namespace Application.DTOs.Filters.Product
{
    public record ProductFilter : SpecificationParams
    {
        public string? Name { get; set; }
    }
}
