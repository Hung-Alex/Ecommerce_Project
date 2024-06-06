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
        public IUser User { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
