using Domain.Common;

namespace Application.DTOs.Responses.Brand
{
    public record BrandDTOs(string Name, string Description, string UrlSlug, string LogoImageUrl) : BaseDTO();
}
