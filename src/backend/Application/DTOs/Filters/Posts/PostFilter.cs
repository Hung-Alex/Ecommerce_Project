

namespace Application.DTOs.Filters.Posts
{
    public record PostFilter : SpecificationParams
    {
        public string? Title { get; set; }
    }
}

