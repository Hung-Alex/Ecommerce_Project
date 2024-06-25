namespace Application.DTOs.Filters.Tags
{
    public record TagFilter : SpecificationParams
    {
        public string? Name { get; set; }
    }
}
