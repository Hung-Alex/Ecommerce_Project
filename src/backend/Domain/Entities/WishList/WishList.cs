using Domain.Common;
using Domain.Shared;

namespace Domain.Entities
{
    public class WishList : BaseEntity, IDatedModification, IAggregateRoot
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
