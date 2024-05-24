using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comments : BaseEntity, IDatedModification
    {
        private Comments() : base() { }
        
       
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
