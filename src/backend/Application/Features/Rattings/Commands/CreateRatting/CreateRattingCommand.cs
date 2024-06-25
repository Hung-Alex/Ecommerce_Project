using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Rattings.Commands.CreateRatting
{
    public record CreateRattingCommand(int Rate, string Description,Guid ProductId) : IRequest<Result<bool>>, IValidatableRequest;
}
