using Application.DTOs.Responses.Cart;
using Domain.Shared;
using MediatR;

namespace Application.Features.Carts.Queries.GetItemInCart
{
    public record GetItemsInCartQuery(Guid UserId):IRequest<Result<CartDTO>>;
}
