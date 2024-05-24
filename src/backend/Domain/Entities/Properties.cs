using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Properties: BaseEntity, IDatedModification
    {
        private Properties() : base() { }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //mapping product
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
