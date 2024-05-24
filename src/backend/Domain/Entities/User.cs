using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    public class User:IdentityUser<Guid>
    {
        public string Address { get; set; }
        public string? City { get; set; }
        public string ?Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string ? ImageUrl {  get; set; }
        //mapping cart
        public Cart Cart { get; set; }
        //mapping order 
        public IList<Order> Orders { get; set; }

    }
}
