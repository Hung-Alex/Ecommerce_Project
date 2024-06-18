using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Internal.Authen
{
    public record RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpriedTime { get; set; }
    }
}
