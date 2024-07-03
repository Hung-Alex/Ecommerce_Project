using Domain.Common;
using Domain.Entities.Posts;
using Domain.Entities.Users;
using Domain.Shared;

namespace Domain.Entities.Comments
{
    public class Comment : BaseEntity, IDatedModification, IAggregateRoot,ICreatedAndUpdatedBy
    {
        public Comment() : base() { }
        public Guid? ParentId { get; set; }
        public string Content { get; set; }
        public Comment Parent { get; set; }
        public ICollection<Comment> Replies { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}
