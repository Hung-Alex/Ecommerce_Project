using Domain.Common;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Coupons
{
    public class CouponProduct:BaseEntity,IDatedModification
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid VoucherId { get; set; }
        public Coupon Voucher { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
