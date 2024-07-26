using Application.Common.Interface.IdentityService;
using Application.DTOs.Responses.Role;
using Domain.Constants;
using Domain.Shared;
using MediatR;

namespace Application.Features.Roles.Queries.GetRoleById
{
    public sealed class GetRoleByIdQueryHandler(IRoleServivce roleServivce) : IRequestHandler<GetRoleByIdQuery, Result<RoleDetail>>
    {
        public async Task<Result<RoleDetail>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await roleServivce.GetRoleByIdAsync(request.roleId);
            if (role is null)
            {
                return Result<RoleDetail>.ResultFailures(ErrorConstants.NotFoundWithId(request.roleId));
            }
            return Result<RoleDetail>.ResultSuccess(role);
        }
    }
}
