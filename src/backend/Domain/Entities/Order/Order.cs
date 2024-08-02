using static Domain.Enums.OrderEnum;
using Domain.Entities.Users;
using Domain.Common;
using Domain.Shared;
using Domain.Entities.Payments;

namespace Domain.Entities.Orders
{
    public class Order : BaseEntity, IDatedModification, IAggregateRoot, ISoftDelete
    {
        public Order() : base() { }
        public Order(ShipAddress shipAddress, string note, Guid userId,Guid PaymentId,Guid stausId) : base()
        {
            ShipAddress = shipAddress;
            Note = note;
            UserId = userId;
            PaymentId = PaymentId;
            StatusId = stausId;
        }
        public string ?CancelReason { get; set; }
        public Guid PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public Guid StatusId { get; set; }
        public Status Status { get; set; }
        public ShipAddress ShipAddress { get; set; }
        public string Note { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; private set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public void AddOrderItems(List<OrderItems> items)
        {
            if (items == null || items.Count == 0)
            {
                throw new ArgumentNullException(nameof(items));
            }
            OrderItems = items;
        }
    }
}
