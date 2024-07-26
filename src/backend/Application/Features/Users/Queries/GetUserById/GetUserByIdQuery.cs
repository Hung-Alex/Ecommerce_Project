using Application.DTOs.Responses.Users;
using Domain.Shared;
using MediatR;
namespace Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDetailDTO>>;
}
