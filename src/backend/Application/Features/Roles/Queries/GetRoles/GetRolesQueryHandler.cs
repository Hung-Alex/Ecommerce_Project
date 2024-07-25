using Application.Common.Interface.IdentityService;
using Application.DTOs.Responses.Role;
using Domain.Shared;
using MediatR;

namespace Application.Features.Roles.Queries.GetRoles
{
    public sealed class GetRolesQueryHandler(IRoleServivce roleServivce) : IRequestHandler<GetRolesQuery, Result<IEnumerable<RoleDTO>>>
    {
        public async Task<Result<IEnumerable<RoleDTO>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await roleServivce.GetAllRoleAsync();
            return Result<IEnumerable<RoleDTO>>.ResultSuccess(roles);
        }
    }
}
