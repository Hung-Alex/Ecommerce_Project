namespace Application.DTOs.Filters.Categories
{
    public record CategoryFilter : SpecificationParams
    {
        public string? Name { get; set; }
        public int? Mode { get; set; } = 0;

    }
}
