using Domain.Entities.Carts;
using Domain.Entities.Rattings;
using Domain.Common;
using Domain.Shared;
using Domain.Entities.WishLists;
using Domain.Entities.Users;
using Domain.Entities.Brands;
using Domain.Entities.Category;
namespace Domain.Entities.Products
{
    public class Product : BaseEntity, IDatedModification, IAggregateRoot, ICreatedAndUpdatedBy, ISoftDelete
    {
        public Product() : base() { }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public string UrlSlug { get; set; }
        public int? Discount { get; set; }
        public Guid? BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Categories Category { get; set; }
        public Guid? CategoryId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public virtual IEnumerable<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<ProductSkus> ProductSkus { get; set; } = new List<ProductSkus>();
        public virtual IEnumerable<CartItem> CartItems { get; set; }
        public virtual IEnumerable<Ratting> Rattings { get; set; }
        public virtual IEnumerable<WishList> WishLists { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public virtual User UpdatedByUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
