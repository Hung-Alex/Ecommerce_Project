
namespace Application.DTOs.Filters.Banner
{
    public record BannerFilter : SpecificationParams
    {
        public string? Title { get; set; }
        public bool? IsVisiable { get; set; }
    }
}
