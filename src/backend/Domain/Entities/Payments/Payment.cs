using Domain.Common;
using Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Payments
{
    public class Payment : BaseEntity, IDatedModification, IAggregateRoot
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
