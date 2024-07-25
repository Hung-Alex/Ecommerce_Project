namespace Application.DTOs.Responses.Brands
{
    public record BrandDTO(string Name, string Description, string UrlSlug) : BaseDTO()
    {
       public string Image { get; init; }
    };

}
