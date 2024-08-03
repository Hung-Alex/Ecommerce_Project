

namespace Application.DTOs.Filters.Orders
{
    public record OrderFilter : SpecificationParams
    {
        public string? Search { get; init; }//người đặt      
        public string? ShipAddress { get; init; }//Địa chỉ Ship tới
        public Guid? Status { get; init; }

    }
}
