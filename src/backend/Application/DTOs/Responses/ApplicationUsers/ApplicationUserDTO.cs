
using Application.DTOs.Responses.Role;

namespace Application.DTOs.Responses.ApplicationUsers
{
    public record ApplicationUserDTO() : BaseDTO
    {
        public string UserName { get; init; }//account login
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public bool IsLockout { get; init; }// true is locked
        public Guid UserId { get; init; }
        public IEnumerable<RoleDTO> Roles { get; init; }
    }
}
