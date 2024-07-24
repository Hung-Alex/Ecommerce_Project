using Domain.Shared;
using MediatR;

namespace Application.Features.Roles.Command.UpdateRole
{
    public record UpdateRoleCommand(Guid RoleId, string RoleName, IEnumerable<Guid>? Permissions) : IRequest<Result<bool>>;
}
