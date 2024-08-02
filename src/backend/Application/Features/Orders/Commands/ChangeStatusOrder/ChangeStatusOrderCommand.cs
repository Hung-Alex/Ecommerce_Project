using Domain.Shared;
using MediatR;

namespace Application.Features.Orders.Commands.ChangeStatusOrder
{
    public record ChangeStatusOrderCommand(Guid OrderId, Guid StatusId) : IRequest<Result<bool>>;
}
