using Domain.Common;
using Domain.Shared;

namespace Domain.Entities
{
    public class Categories : BaseEntity, IDatedModification, IAggregateRoot
    {
        private Categories() : base() { }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public IList<Product> Products { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
