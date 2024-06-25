using Domain.Shared;
using MediatR;

namespace Application.Features.Tags.Commands.DeleteTag
{
    public record DeleteTagCommand(Guid Id) : IRequest<Result<bool>>;
}
