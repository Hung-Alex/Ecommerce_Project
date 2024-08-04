namespace Application.DTOs.Filters.State
{
    public record StateFilter : SpecificationParams
    {
        public string? Type { get; set; }
        public string? Code { get; set; }
    }
}
