using Domain.Common;
using Domain.Entities.Orders;
using Domain.Shared;

namespace Domain.Entities.Payments
{
    public class Payment : BaseEntity, IDatedModification, IAggregateRoot, ISoftDelete
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public string PaymentCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Fee { get; set; }
        public Guid StatusId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
