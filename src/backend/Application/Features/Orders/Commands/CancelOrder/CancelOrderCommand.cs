using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Orders.Commands.CancelOrder
{
    public record CancelOrderCommand(Guid OrderId, string CancelReason) : IRequest<Result<bool>>, IValidatableRequest;
}
