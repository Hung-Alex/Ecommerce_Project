using Domain.Entities.Carts;
using Domain.Entities.Coupons;
using Domain.Entities.Rattings;
using Domain.Common;
using Domain.Shared;
using Domain.Entities.WishLists;
using Domain.Entities.Users;
using Domain.Entities.Brands;
namespace Domain.Entities.Products
{
    public class Product : BaseEntity, IDatedModification, IAggregateRoot, ICreatedAndUpdatedBy
    {
        public Product() : base() { }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public string UrlSlug { get; set; }
        public string? UnitPrice { get; set; }
        public string? WeightUnit { get; set; }
        public int? Weight { get; set; }
        public int? Discount { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public virtual ICollection<ProductImages> Images { get; set; } = new List<ProductImages>();
        public virtual ICollection<ProductSkus> ProductSkus { get; set; } = new List<ProductSkus>();
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<CouponProduct> ProductCoupons { get; set; }
        public virtual ICollection<Ratting> Rattings { get; set; }
        public virtual ICollection<ProductSubCategory> ProductSubCategories { get; set; } = new List<ProductSubCategory>();
        public virtual ICollection<WishList> WishLists { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}
