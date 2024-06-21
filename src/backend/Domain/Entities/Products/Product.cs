using Domain.Entities.Brands;
using Domain.Entities.Carts;
using Domain.Entities.Coupons;
using Domain.Entities.Rattings;
using Domain.Common;
using Domain.Shared;
namespace Domain.Entities.Products
{
    public class Product : BaseEntity, IDatedModification, IAggregateRoot
    {
        private Product() : base() { }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public string UnitPrice { get; set; }
        public int? Discount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public virtual ICollection<ProductImages> Images { get; set; }
        public virtual ICollection<ProductSkus> ProductSkus { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<CouponProduct> ProductCoupons { get; set; }
        public virtual ICollection<Ratting> Rattings { get; set; }
        public virtual ICollection<ProductSubCategory> ProductSubCategories { get; set; }
    }
}
