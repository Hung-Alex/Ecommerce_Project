
namespace Application.DTOs.Responses.Post
{
    public record PostDetailDTO : BaseDTO
    {
        public string Title { get; init; }
        public string UrlSlug { get; init; }
        public string ShortDescription { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public bool Published { get; init; }
        public int ViewCount { get; init; }
    }
}
