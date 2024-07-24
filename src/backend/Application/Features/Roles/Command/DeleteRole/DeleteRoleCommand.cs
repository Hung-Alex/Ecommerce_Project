using Domain.Shared;
using MediatR;

namespace Application.Features.Roles.Command.DeleteRole
{
    public record DeleteRoleCommand(Guid RoleId) : IRequest<Result<bool>>;
}
