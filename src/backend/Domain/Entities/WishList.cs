using Domain.Common;

namespace Domain.Entities
{
    public class WishList : BaseEntity, IDatedModification
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
