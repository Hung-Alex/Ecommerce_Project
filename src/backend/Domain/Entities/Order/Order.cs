using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using static Domain.Enums.OrderEnum;
using Domain.Entities.Coupons;
using Domain.Entities.Payments;
using Domain.Entities.Users;

namespace Domain.Entities.Orders
{
    public class Order : BaseEntity, IDatedModification, IAggregateRoot
    {
        private Order() : base() { }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public  decimal Total { get; set; }
        public string ? Note { get; set; }
        public OrderStatus  OrderStatus { get; set; }//enum 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // mapping voucher
        public IList<Coupon> Vouchers { get;set; }
        //mapping Payment
        public virtual Payment Payment { get; set; }
        //mapping orderItemsMap
        public IList<OrderItems> OrderItemsMaps { get; set; }
        //mapping user
        public Guid UserId { get; set; }
        public User User { get; set; }



    }
}
