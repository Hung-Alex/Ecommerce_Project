using Domain.Common;
using Domain.Entities.Category;

namespace Domain.Entities.SubCategories
{
    public class SubCategory : BaseEntity, IAggregateRoot, IDatedModification
    {
        public SubCategory(string name, string description, string urlSlug) : base()
        {
            Name = name;
            Description = description;
            UrlSlug = urlSlug;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlSlug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Categories Category { get; set; }
        public Guid CategoryId { get; set; }

    }
}
