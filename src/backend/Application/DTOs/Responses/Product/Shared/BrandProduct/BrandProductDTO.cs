namespace Application.DTOs.Responses.Product.Shared.BrandProduct
{
    public record BrandProductDTO : BaseDTO
    {
        public string Name { get; set; }
        public string UrlSlug { get; set; }
    }
}
