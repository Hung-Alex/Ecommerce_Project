namespace Application.DTOs.Responses.Cart
{
    public record CartItemDTO() : BaseDTO
    {
        public Guid ProductId { get; init; }
        public Guid ProductSkusId { get; init; }
        public string ProductName { get; init; }
        public string Image { get; init; }
        public string Quantity { get; init; }
    }
}
