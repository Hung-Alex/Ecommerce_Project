using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tags : BaseEntity, IDatedModification
    {
        private Tags() : base() { } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlSlug {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        //mapping Post
        public IList<PostTags> PostTags { get; set; }
    }
}
