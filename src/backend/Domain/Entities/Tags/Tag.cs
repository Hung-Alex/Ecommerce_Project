using Domain.Common;
using Domain.Entities.Posts;
using Domain.Entities.Users;
using Domain.Shared;

namespace Domain.Entities.Tags
{
    public class Tag : BaseEntity, IDatedModification, IAggregateRoot, ICreatedAndUpdatedBy
    {
        public Tag() : base() { }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlSlug { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        //mapping Post
        public IList<PostTags> PostTags { get; set; }
        public Guid ? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid ? UpdatedByUserId { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}
