using Domain.Common;
using Domain.Entities.Products;

namespace Domain.Entities.Coupons
{
    public class CouponProduct:BaseEntity,IDatedModification
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
