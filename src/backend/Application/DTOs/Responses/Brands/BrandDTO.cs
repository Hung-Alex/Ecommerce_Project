namespace Application.DTOs.Responses.Brands
{
    public record BrandDTO(string Name, string Description, string UrlSlug, string Image) : BaseDTO();

}
