namespace Application.DTOs.Responses.Banners
{
    public record BannerDTO : BaseDTO
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string LogoImageUrl { get; init; }
        public bool? Left { get; init; }
        public bool? Right { get; init; }
    }
}
