using Domain.Common;

namespace Domain.Entities
{
    public class Categories : BaseEntity, IDatedModification
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
