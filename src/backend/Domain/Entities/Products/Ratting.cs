using Domain.Common;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ratting: BaseEntity, IDatedModification
    {
        private Ratting() : base() { }
        public int Rate {  get; set; }
        public string Description {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //mapping product
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
