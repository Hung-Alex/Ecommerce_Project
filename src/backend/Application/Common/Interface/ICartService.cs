using Application.DTOs.Responses.Cart;

namespace Application.Common.Interface
{
    public interface ICartService
    {
        Task<CartDTO> GetCartAsync(Guid CartId, CancellationToken cancellationToken = default);
    }
}
