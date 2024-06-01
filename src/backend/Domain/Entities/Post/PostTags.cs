using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PostTags:BaseEntity,IDatedModification
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid TagId {  get; set; }  
        public Tags Tag { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
