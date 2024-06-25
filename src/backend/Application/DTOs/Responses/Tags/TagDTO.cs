namespace Application.DTOs.Responses.Tags
{
    public record TagDTO:BaseDTO
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string UrlSlug { get; init; }
    }
}
