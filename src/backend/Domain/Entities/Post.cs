using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post : BaseEntity, IDatedModification
    {
        private Post() : base() { }
        public string Title { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Meta { get; set; }
        public string ImageUrl { get; set; }
        public bool Pulished {  get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //mapping Comments
        public IList<Comments> Comments { get; set; }
        //mapping Tags
        public IList<PostTags> PostTags { get; set; }
    }
}
