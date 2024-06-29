using Domain.Shared;
using MediatR;

namespace Application.Features.WishsList.Commands.DeleteFavoriteProduct
{
    public record DeleteFavoriteProductCommand(Guid ProductId, Guid UserId) : IRequest<Result<bool>>;
}
