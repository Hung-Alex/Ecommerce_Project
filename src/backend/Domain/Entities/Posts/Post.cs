using Domain.Common;
using Domain.Entities.Comments;
using System.Collections.ObjectModel;
using Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Posts
{
    public class Post : BaseEntity, IDatedModification, IAggregateRoot
    {
        public Post(string title, string name, string urlSlug, string shortDescription, string description, string meta, string imageUrl, bool? pulished, int viewCount) : base()
        {
            Title = title ?? throw new ArgumentNullException();
            Name = name ?? throw new ArgumentNullException();
            UrlSlug = urlSlug ?? throw new ArgumentNullException();
            ShortDescription = shortDescription ?? throw new ArgumentNullException();
            Description = description ?? throw new ArgumentNullException();
            Meta = meta ?? throw new ArgumentNullException();
            ImageUrl = imageUrl ?? throw new ArgumentNullException();
            Pulished = pulished ?? throw new ArgumentNullException();
        }
        public string Title { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Meta { get; set; }
        public string ImageUrl { get; set; }
        public bool? Pulished { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostTags> PostTags { get; set; }
    }
}
