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
        public Order(ShipAddress shipAddress, List<OrderItems> items, string note, Guid userId) : base()
        {
            ShipAddress = shipAddress;
            OrderItems = items;
            Note = note;
            UserId = userId;
        }
        public string CancelReason { get; set; }
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }
        public Guid StatusId { get; set; }
        public Status Status { get; set; }
        public ShipAddress ShipAddress { get; set; }
        public string Note { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
