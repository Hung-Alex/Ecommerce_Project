using Domain.Common;
using Domain.Entities.Posts;
using Domain.Shared;

namespace Domain.Entities.Comments
{
    public class Comment : BaseEntity, IDatedModification, IAggregateRoot
    {
        private Comment() : base() { }
        public string Content {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
