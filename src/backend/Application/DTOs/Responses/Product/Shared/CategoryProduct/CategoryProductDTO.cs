namespace Application.DTOs.Responses.Product.Shared.CategoryProduct
{
    public record CategoryProductDTO : BaseDTO
    {
        public string Name { get; set; }
        public string UrlSlug { get; set; }
    }
}
