using Domain.Entities.Orders;

namespace Application.DTOs.Responses.Orders
{
    public record OrderDTO : BaseDTO
    {
        public string Status { get; init; }
        public string? CancelReason { get; init; }
        public ShipAddress ShipAddress { get; init; }
        public string Note { get; init; }
        public IEnumerable<OrderItemsDTO> OrderItems { get; init; }
        public string PaymentStatus { get; init; }
    }
}
