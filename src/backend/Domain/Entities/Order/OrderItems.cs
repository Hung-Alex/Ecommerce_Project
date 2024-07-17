using Domain.Common;
using Domain.Entities.Products;

namespace Domain.Entities.Orders
{
    public class OrderItems:BaseEntity,IDatedModification
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductSkusId { get; set; }
        public ProductSkus ProductSkus { get; set; }
        public int Quantity { get; set; }
        public Decimal Price {  get; set; } //inital Price 
        public string ? UnitPrice {  get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
