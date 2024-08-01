using Domain.Common;
using Domain.Entities.Products;

namespace Domain.Entities.Orders
{
    public class OrderItems : BaseEntity, IDatedModification
    {
        public OrderItems() : base() { }
        public OrderItems(Guid orderId, Guid productId, int quantity, Decimal price, string? unitPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            UnitPrice = unitPrice;
        }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; } //inital Price 
        public string? UnitPrice { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
