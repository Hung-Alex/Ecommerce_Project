namespace Application.DTOs.Filters.Slides
{
    public record SlideFilter : SpecificationParams
    {
        public string? Title { get; set; }
    }
}
