using Domain.Common;
using Domain.Entities.Banners;
using Domain.Entities.Carts;
using Domain.Entities.Category;
using Domain.Entities.Comments;
using Domain.Entities.Orders;
using Domain.Entities.Posts;
using Domain.Entities.Products;
using Domain.Entities.Rattings;
using Domain.Entities.Slides;
using Domain.Entities.WishLists;
using Domain.Shared;
namespace Domain.Entities.Users
{
    public class User : BaseEntity, IAggregateRoot, IDatedModification
    {
        public User() : base() { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarImage { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public virtual ICollection<WishList> WishList { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual Cart Cart { get; set; }
        //updatedby and createdBy
        public virtual ICollection<Banner> UpdatedByBanner { get; set; }
        public virtual ICollection<Banner> CreatedByBanner { get; set; }
        public virtual ICollection<Post> UpdatedByPost { get; set; }
        public virtual ICollection<Post> CreatedByPost { get; set; }
        public virtual ICollection<Comment> UpdatedByComment { get; set; }
        public virtual ICollection<Comment> CreatedByComment { get; set; }
        public virtual ICollection<Categories> UpdatedByCategory { get; set; }
        public virtual ICollection<Categories> CreatedByCategory { get; set; }
        public virtual ICollection<Ratting> UpdatedByRatting { get; set; }
        public virtual ICollection<Ratting> CreatedByRatting { get; set; }
        public virtual ICollection<Slide> UpdatedBySlide { get; set; }
        public virtual ICollection<Slide> CreatedBySlide { get; set; }
        public virtual ICollection<Product> UpdatedByProduct { get; set; }
        public virtual ICollection<Product> CreatedByProduct { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
