using Domain.Common;
using Domain.Entities.Carts;
using Domain.Entities.Orders;
using Domain.Entities.WishLists;
using Domain.Shared;


namespace Domain.Entities.Users
{
    public interface IUser : IDatedModification, IAggregateRoot
    {
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }//base 64
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<WishList> WishList { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public Cart Cart { get; set; }
    }
}
