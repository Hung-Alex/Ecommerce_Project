using Domain.Shared;
using MediatR;

namespace Application.Features.Posts.Commands.DeletePost
{
    public record DeletePostCommand(Guid Id) : IRequest<Result<bool>>;
}
