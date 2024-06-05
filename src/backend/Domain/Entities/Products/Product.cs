using Domain.Entities.Brands;
using Domain.Entities.Carts;
using Domain.Entities.Category;
using Domain.Entities.Comments;
using Domain.Entities.Coupons;
using Domain.Entities.Images;
using Domain.Entities.Orders;
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
        public string ImageUrl { get; set; }
        public  Decimal Price { get; set; }
        public string? UnitPrice { get; set; }
        public  int? Discount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //mapping category
        public Guid CategoryId { get; set; }
        public Categories Category { get; set; }
        //mapping brand
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        //mapping  Images
        public ICollection<Image> Images { get; set; }
        //mapping  Comments
        public ICollection<Comment> Comments { get; set; }
        //mapping options
        public ICollection<ProductSkus> ProductSkus { get; set; }
        //mapping CartItemsMap
        public ICollection<CartItem> CartItemMaps { get; set; }
        //mapping ProductVouchersMap
        public ICollection<CouponProduct> ProductVouchersMaps { get; set; }
        //mapping Rattings
        public ICollection<Ratting> Rattings { get; set; }
        //mapping orderItemsMap
        public ICollection<OrderItems> OrderItemsMaps { get; set; }


    }
}
