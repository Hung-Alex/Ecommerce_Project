using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Images : BaseEntity, IDatedModification
    {
        private Images() : base()
        {
           
        }
        public string ImageUrl {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //mapping product
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
