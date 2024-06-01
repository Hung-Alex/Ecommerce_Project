using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CartItemNotFoundException:Exception
    {
        public CartItemNotFoundException() { }
        public CartItemNotFoundException(string message) : base(message) { }
    }
}
