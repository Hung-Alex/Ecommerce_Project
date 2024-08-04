
namespace Application.DTOs.Filters.Orders
{
    public record UserOrderFilter:SpecificationParams
    {
        public Guid? Status { get; init; }
    }
}
