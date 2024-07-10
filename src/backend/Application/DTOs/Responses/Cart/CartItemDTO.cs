namespace Application.DTOs.Responses.Cart
{
    public record CartItemDTO() : BaseDTO
    {
        public Guid ProductId { get; init; }
        public Guid? ProductSkusId { get; init; }
        public string ProductName { get; init; }
        public string Image { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
        public decimal Total { get { return Quantity * Price; } }
    }
}
