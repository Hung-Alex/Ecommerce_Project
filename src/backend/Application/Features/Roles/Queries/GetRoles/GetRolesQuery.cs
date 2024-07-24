using Application.DTOs.Responses.Role;
using Domain.Shared;
using MediatR;

namespace Application.Features.Roles.Queries.GetRoles
{
    public record GetRolesQuery():IRequest<Result<IEnumerable<RoleDTO>>>;
}
