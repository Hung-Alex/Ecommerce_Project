using Domain.Common;
using Domain.Entities.Posts;
using Domain.Shared;

namespace Domain.Entities.Tags
{
    public class Tag : BaseEntity, IDatedModification, IAggregateRoot
    {
        public Tag() : base() { } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlSlug {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //mapping Post
        public IList<PostTags> PostTags { get; set; }
    }
}
