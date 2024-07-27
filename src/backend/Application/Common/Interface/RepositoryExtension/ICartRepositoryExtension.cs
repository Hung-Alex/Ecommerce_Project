using Application.DTOs.Responses.Cart;
using Domain.Entities.Carts;
using Domain.Interface;

namespace Application.Common.Interface.RepositoryExtension
{
    public interface ICartRepositoryExtension : IRepository<Cart>
    {
        Task<CartDTO> GetCartAsync(Guid CartId, CancellationToken cancellationToken = default);
    }
}
