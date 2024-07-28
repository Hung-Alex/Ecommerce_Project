using Domain.Common;
using Domain.Entities.Comments;
using Domain.Entities.Users;
using Domain.Shared;

namespace Domain.Entities.Posts
{
    public class Post : BaseEntity, IDatedModification, IAggregateRoot, ICreatedAndUpdatedBy
    {
        public Post() : base() { }
        public Post(string title, string urlSlug, string shortDescription, string description, string imageUrl, bool Published, int viewCount) : base()
        {
            Title = title ?? throw new ArgumentNullException();
            UrlSlug = urlSlug ?? throw new ArgumentNullException();
            ShortDescription = shortDescription ?? throw new ArgumentNullException();
            Description = description ?? throw new ArgumentNullException();
            ImageUrl = imageUrl ?? throw new ArgumentNullException();
            Published = Published;
        }
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Published { get; set; }
        public int ViewCount { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}
