using Application.DTOs.Responses.Permissions;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Permissions.Queries.GetPermissions
{
    public record GetPermissionsQuery() : IRequest<Result<IEnumerable<PermissionDTO>>>;
}
