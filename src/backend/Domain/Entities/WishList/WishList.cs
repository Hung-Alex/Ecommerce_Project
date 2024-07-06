using Domain.Common;
using Domain.Entities.Products;
using Domain.Entities.Users;
using Domain.Shared;

namespace Domain.Entities.WishLists
{
    public class WishList : BaseEntity, IDatedModification, IAggregateRoot
    {
        public WishList() { }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
