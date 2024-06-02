using Domain.Common;
using Domain.Shared;

namespace Domain.Entities.WishLists
{
    public class WishList : BaseEntity, IDatedModification, IAggregateRoot
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
