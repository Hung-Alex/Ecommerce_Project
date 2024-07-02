using Application.Common.Interface;
using Application.DTOs.Responses.Cart;
using Application.Features.Carts.Queries.GetItemInCart;
using Application.Features.Carts.Specification;
using Domain.Entities.Carts;
using Domain.Shared;
using MediatR;

namespace Application.Features.Carts.Queries.GetItemsInCart
{
    public sealed class GetItemsInCartQueryHandler : IRequestHandler<GetItemsInCartQuery, Result<IEnumerable<CartItemDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetItemsInCartQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<CartItemDTO>>> Handle(GetItemsInCartQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Cart>();
            var cart = await repo.FindOneAsync(new GetCartByUserIdSpecification(request.UserId));
            var cartItemDTO = cart.CartItems.Select(c => new CartItemDTO()
            {
                Id = c.Id,
                ProductName = c.Product.Name,
                ProductId = c.ProductId,
                ProductSkusId = c.ProductSkus.Id,
                Image = c.Product.Images.Select(x => x.Image.ImageUrl).FirstOrDefault() ?? "",
            });
            return Result<IEnumerable<CartItemDTO>>.ResultSuccess(cartItemDTO);
        }
    }
}
