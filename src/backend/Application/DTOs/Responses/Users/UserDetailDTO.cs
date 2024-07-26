using Application.DTOs.Responses.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses.Users
{
    public record UserDetailDTO:BaseDTO
    {
        public string UserName { get; init; }//account login
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string FristName { get; init; }
        public string LastName { get; init; }
        public string City { get; init; }
        public string PostalCode { get; init; }
        public string Region { get; init; }
        public bool IsLockout { get; init; }// true is locked
        public string AvatarImage { get; init; }
        public IEnumerable<RoleDTO> Roles { get; init; }
    }
}
