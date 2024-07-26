using Application.DTOs.Filters.Users;
using Application.DTOs.Responses.Users;
using Domain.Shared;
using MediatR;

namespace Application.Features.Users.Queries.GetUsers
{
    public record GetUsersQuery(UserFilter Filter) : IRequest<Result<IEnumerable<UserDTO>>>;
}
