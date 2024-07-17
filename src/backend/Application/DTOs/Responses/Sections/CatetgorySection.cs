namespace Application.DTOs.Responses.Sections
{
    public record CatetgorySection:BaseDTO
    {
        public string Name { get; init; }
        public string UrlSlug {  get; init; }
        public int TotalItems {  get; init; }
    }
}
