using Domain.Common;
using Domain.Entities.Products;
using Domain.Entities.Users;
using Domain.Shared;

namespace Domain.Entities.Brands
{
    public class Brand : BaseEntity, IDatedModification, ICreatedAndUpdatedBy,IAggregateRoot
    {
        public Brand() { }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public User UpdatedByUser { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
