using Domain.Common;

namespace Domain.Entities
{
    public class Product : BaseEntity, IDatedModification
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
        //mapping  specifition
        public IList<Properties> Specifitions { get; set; }
        //mapping  Images
        public IList<Images> Images { get; set; }
        //mapping  Comments
        public IList<Comments> Comments { get; set; }
        //mapping options
        public IList<ProductSkus> ProductSkus { get; set; }
        //mapping CartItemsMap
        public IList<CartItem> CartItemMaps { get; set; }
        //mapping ProductVouchersMap
        public IList<CouponProduct> ProductVouchersMaps { get; set; }
        //mapping Rattings
        public IList<Ratting> Rattings { get; set; }
        //mapping orderItemsMap
        public IList<OrderItems> OrderItemsMaps { get; set; }


    }
}
