namespace Application.DTOs.Responses.Cart
{
    public record CartDTO : BaseDTO
    {
        public IEnumerable<CartItemDTO> Items { get; set; }
        public int Quantity { get { return Items.Count(); } }
        public decimal Total { get { return Items.Sum(x => x.Total); } }
    }
}
