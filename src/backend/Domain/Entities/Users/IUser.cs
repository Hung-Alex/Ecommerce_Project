using Domain.Common;
using Domain.Entities.Carts;
using Domain.Entities.Orders;
using Domain.Entities.WishLists;
using Domain.Shared;


namespace Domain.Entities.Users
{
    public interface IUser : IDatedModification, IAggregateRoot
    {
        string City { get; set; }
        string Region { get; set; }
        string PostalCode { get; set; }
        string Country { get; set; }
        string ImageUrl { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        ICollection<WishList> WishList { get; set; }
        ICollection<Order> Orders { get; set; }
        ICollection<Address> Addresses { get; set; }
        Cart Cart { get; set; }
    }
}
