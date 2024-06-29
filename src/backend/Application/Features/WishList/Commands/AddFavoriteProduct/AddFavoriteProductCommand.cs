using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.WishsList.Commands.CreateFavoriteProduct
{
    public record AddFavoriteProductCommand(Guid UserId,Guid ProductId) : IRequest<Result<bool>>, IValidatableRequest;
}
