using Application.DTOs.Responses.Role;
using Domain.Shared;
using MediatR;

namespace Application.Features.Roles.Queries.GetRoleById
{
    public record GetRoleByIdQuery(Guid roleId):IRequest<Result<RoleDetail>>;
}
