using Domain.Common;
using Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IList<CouponProduct> CouponProducts { get; set; }
        //mapping Order
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime CreatedAt {  get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
