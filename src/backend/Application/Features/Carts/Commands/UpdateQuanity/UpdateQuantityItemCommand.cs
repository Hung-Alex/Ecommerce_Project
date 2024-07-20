using Domain.Shared;
using MediatR;

namespace Application.Features.Carts.Commands.UpdateQuanity
{
    public record UpdateQuantityItemCommand(Guid UserId, Guid CartItemId, int Quantity) : IRequest<Result<bool>>;
}
