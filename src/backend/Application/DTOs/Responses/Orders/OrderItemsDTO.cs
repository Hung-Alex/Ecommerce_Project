using Domain.Entities.Orders;

namespace Application.DTOs.Responses.Orders
{  
    public record OrderItemsDTO : BaseDTO
    {
        public Guid ProductId { get; set; } 
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; } //inital Price 
        public string? UnitPrice { get; set; }
    }
}
