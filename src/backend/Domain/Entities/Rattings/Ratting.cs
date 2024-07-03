using Domain.Common;
using Domain.Entities.Products;
using Domain.Entities.Users;
using Domain.Shared;

namespace Domain.Entities.Rattings
{
    public class Ratting : BaseEntity, IAggregateRoot, IDatedModification,ICreatedAndUpdatedBy
    {
        public Ratting() : base() { }
        public int Rate { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
