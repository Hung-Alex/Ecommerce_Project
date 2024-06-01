using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CartNullException : Exception
    {
        public CartNullException() { }
        public CartNullException(string message) : base(String.Format("Cart is null :{0}",message)) { }
    }
}
