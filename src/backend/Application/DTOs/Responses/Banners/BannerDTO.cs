using static Domain.Enums.BannerEnum;

namespace Application.DTOs.Responses.Banners
{
    public record BannerDTO : BaseDTO
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string LogoImageUrl { get; init; }
        public LocationBanner? Location {  get; init; } 
    }
}
