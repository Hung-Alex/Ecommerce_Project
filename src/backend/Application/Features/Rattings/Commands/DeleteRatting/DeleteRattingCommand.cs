using Domain.Shared;
using MediatR;

namespace Application.Features.Rattings.Commands.DeleteRatting
{
    public record DeleteRattingCommand(Guid Id) : IRequest<Result<bool>>;
}
