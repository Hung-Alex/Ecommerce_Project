using Domain.Common;
using Domain.Entities.Orders;
using Domain.Shared;
using System.Collections.ObjectModel;


namespace Domain.Entities.Coupons
{
    public class Coupon : BaseEntity, IDatedModification, IAggregateRoot
    {
        private Coupon() : base() { }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int DiscountValue { get; set; }
        public string DiscountType { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponEndDate { get; set; }
        public int UsedTime {  get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        private Collection<CouponProduct> _couponProducts=new Collection<CouponProduct>();
        public IReadOnlyCollection<CouponProduct> CouponProducts => _couponProducts.AsReadOnly();
        //mapping Order
        public virtual ICollection<Order> Orders { get; set; }
        public DateTime CreatedAt {  get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
