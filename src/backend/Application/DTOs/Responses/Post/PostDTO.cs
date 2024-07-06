
namespace Application.DTOs.Responses.Post
{
    public record PostDTO : BaseDTO
    {
        public string Title { get; init; }
        public string ShortDescription { get; init; }
        public string Image { get; init; }
        public string UrlSlug { get; init; }
        public int ViewCount { get; init; }
    }
}
