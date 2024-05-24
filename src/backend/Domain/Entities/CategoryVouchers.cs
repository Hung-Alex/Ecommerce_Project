using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoryVouchers:BaseEntity,IDatedModification
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid VoucherId { get; set; }
        public Voucher Voucher { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
