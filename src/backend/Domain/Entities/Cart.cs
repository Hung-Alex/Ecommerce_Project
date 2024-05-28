using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart : BaseEntity,IDatedModification
    {
        private Cart() : base() { }
        //mapping CartItemsMap
        public IList<CartItem> CartItems { get; set; }
        //mapping user
        public Guid UserId { get ; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
