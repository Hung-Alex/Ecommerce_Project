using Application.Common.Interface;
using Application.DTOs.Responses.Cart;
using Application.Features.Carts.Queries.GetItemInCart;
using Application.Features.Carts.Specification;
using Domain.Constants;
using Domain.Entities.Carts;
using Domain.Shared;
using MediatR;

namespace Application.Features.Carts.Queries.GetItemsInCart
{
    public sealed class GetItemsInCartQueryHandler : IRequestHandler<GetItemsInCartQuery, Result<CartDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;

        public GetItemsInCartQueryHandler(IUnitOfWork unitOfWork, ICartService cartService)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
        }
        public async Task<Result<CartDTO>> Handle(GetItemsInCartQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Cart>();
            var cart = await repo.FindOneAsync(new GetCartByUserIdSpecification(request.UserId));
            if (cart is null)
            {
                return Result<CartDTO>.ResultFailures(ErrorConstants.CartNotFound);
            }
            var cartDTO = await _cartService.GetCartAsync(cart.Id);
            return Result<CartDTO>.ResultSuccess(cartDTO);
        }
    }
}
