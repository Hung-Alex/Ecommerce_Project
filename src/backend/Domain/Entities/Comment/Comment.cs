using Domain.Common;
using Domain.Entities.Posts;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Comments
{
    public class Comment : BaseEntity, IDatedModification, IAggregateRoot
    {
        private Comment() : base() { }
        
       
        public string Message {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //mapping product
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        //mapping Post
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
