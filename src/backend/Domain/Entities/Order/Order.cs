using static Domain.Enums.OrderEnum;
using Domain.Entities.Coupons;
using Domain.Entities.Users;
using Domain.Common;
using Domain.Shared;

namespace Domain.Entities.Orders
{
    public class Order : BaseEntity, IDatedModification, IAggregateRoot
    {
        public Order():base() { }
        public Order(ShipAddress shipAddress,List<OrderItems> items,string note,Guid userId) : base()
        {
            ShipAddress = shipAddress;
            OrderItems = items;
            Note = note;
            UserId = userId;
        }
        public ShipAddress ShipAddress { get; set; }
        public string Note { get; set; }
        public OrderStatus OrderStatus { get; set; }//enum 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // mapping voucher
        public IList<Coupon> Coupons { get; set; }      
        //mapping orderItemsMap
        public IList<OrderItems> OrderItems { get; set; }
        //mapping user
        public Guid UserId { get; set; }
        public IUser User { get; set; }
    }
}
