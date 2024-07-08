
namespace Application.DTOs.Responses.Product.Variants
{
    public record VariantsDTO : BaseDTO
    {
        public string VariantName { get; init; }
        public string Description { get; init; }
        public int Quantity { get; init; }
        public decimal Price { get; init; }
    }
}
