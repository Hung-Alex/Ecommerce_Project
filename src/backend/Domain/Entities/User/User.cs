using Domain.Common;
using Domain.Shared;


namespace Domain.Entities
{
    public class User : BaseEntity, IDatedModification, IAggregateRoot
    {
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<WishList> WishList { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public Cart Cart { get; set; }
    }
}
