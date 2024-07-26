using Application.DTOs.Responses.Permissions;

namespace Application.DTOs.Responses.Role
{
    public record RoleDetail : BaseDTO
    {
        public string Name { get; set; }
        public IEnumerable<PermissionDTO> Permissions { get; set; }
    }
}
