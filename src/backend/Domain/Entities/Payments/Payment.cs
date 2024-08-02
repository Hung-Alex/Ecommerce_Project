using Domain.Common;
using Domain.Entities.Orders;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.Shared;

namespace Domain.Entities.Payments
{
    public class Payment : BaseEntity, IDatedModification, IAggregateRoot, ISoftDelete, ICreatedAndUpdatedBy
    {
        public Payment() : base() { }
        public Payment(decimal amount, PaymentMethod paymentMethod, DateTimeOffset transactionDate, decimal fee, Guid statusId) : base()
        {
            Amount = amount;
            PaymentMethod = paymentMethod;
            TransactionDate = transactionDate;
            Fee = fee;
            StatusId = statusId;
        }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? TransactionId { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public decimal Fee { get; set; }
        public Guid StatusId { get; set; }
        public virtual Status Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public Guid? CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public User UpdatedByUser { get; set; }
    }
}
