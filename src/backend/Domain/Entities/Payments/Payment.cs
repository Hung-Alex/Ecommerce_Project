using Domain.Common;
using Domain.Entities.Orders;
using Domain.Shared;

namespace Domain.Entities.Payments
{
    public class Payment : BaseEntity, IDatedModification, IAggregateRoot
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
