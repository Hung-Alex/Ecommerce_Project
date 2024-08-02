using Domain.Common;
using Domain.Entities.Orders;
using Domain.Entities.Payments;
using Domain.Entities.Users;
using Domain.Shared;

namespace Domain.Entities
{
    public class Status : BaseEntity, IAggregateRoot, ISoftDelete, IDatedModification, ICreatedAndUpdatedBy
    {
        public Status() : base() { }
        public string Type { get; set; }
        public string Display { get; set; }
        public string Code { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public User UpdatedByUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
