
namespace Application.DTOs.Responses.Product.ProductImage
{
    public record ProductImageDTO : BaseDTO
    {
        public string Image { get; set; }
        public int Order { get; set; }
    }
}
