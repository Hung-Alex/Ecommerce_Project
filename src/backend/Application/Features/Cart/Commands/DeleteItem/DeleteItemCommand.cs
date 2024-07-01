using Domain.Shared;
using MediatR;

namespace Application.Features.Carts.Commands.DeleteItem
{
    public record DeleteItemCommand(Guid UserId,Guid CarItemId):IRequest<Result<bool>>;
}
