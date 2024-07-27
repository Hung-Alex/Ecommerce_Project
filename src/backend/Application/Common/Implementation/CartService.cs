using Application.Common.Interface;
using Application.Common.Interface.RepositoryExtension;
using Application.DTOs.Responses.Cart;

namespace Application.Common.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepositoryExtension _cartRepository;
        public CartService(ICartRepositoryExtension cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<CartDTO> GetCartAsync(Guid CartId, CancellationToken cancellationToken = default)
        {
            var cart = await _cartRepository.GetCartAsync(CartId, cancellationToken);
            return cart;
        }
    }
}
