using Application.Common.Interface;
using Application.DTOs.Responses.Permissions;
using Domain.Shared;
using MediatR;

namespace Application.Features.Permissions.Queries.GetPermissions
{
    public sealed class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, Result<IEnumerable<PermissionDTO>>>
    {
        private readonly IPermissionService _permissionService;
        public GetPermissionsQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public async Task<Result<IEnumerable<PermissionDTO>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            return Result<IEnumerable<PermissionDTO>>.ResultSuccess(await _permissionService.GetAllPermissionAsync());
        }
    }
}
