using Domain.Common;

namespace Domain.Entities
{
    public class SubCategories : BaseEntity, IDatedModification
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Categories Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
