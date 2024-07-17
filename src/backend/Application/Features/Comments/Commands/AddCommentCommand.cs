using Domain.Shared;
using MediatR;

namespace Application.Features.Comments.Commands
{
    public record AddCommentCommand(Guid PostId, string Content) : IRequest<Result<bool>>;
}
