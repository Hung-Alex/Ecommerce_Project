using Domain.Shared;
using MediatR;

namespace Application.Features.Roles.Command.CreateRole
{
    public record CreateRoleCommand(string RoleName, IEnumerable<Guid> Permissions) : IRequest<Result<bool>>;
}
