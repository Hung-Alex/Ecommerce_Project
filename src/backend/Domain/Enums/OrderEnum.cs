using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public static class OrderEnum
    {
        public enum OrderStatus
        {
            Unknown = 0,
            Pending,
            Processing,
            Shipped,
            Delivered,
            Cancelled
        }
    }
}
