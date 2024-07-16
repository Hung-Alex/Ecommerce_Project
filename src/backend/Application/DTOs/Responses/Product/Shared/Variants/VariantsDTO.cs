namespace Application.DTOs.Responses.Product.Shared.Variants
{
    public record VariantsDTO : BaseDTO
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
