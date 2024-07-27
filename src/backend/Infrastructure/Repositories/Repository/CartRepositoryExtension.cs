using Application.Common.Interface.RepositoryExtension;
using Application.DTOs.Responses.Cart;
using Domain.Entities.Carts;
using Infrastructure.Data;
using Infrastructure.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Repository
{
    public class CartRepositoryExtension : BaseRepository<Cart>, ICartRepositoryExtension
    {
        public CartRepositoryExtension(StoreDbContext context) : base(context) { }

        public async Task<CartDTO> GetCartAsync(Guid CartId, CancellationToken cancellationToken = default)
        {
            var query = from c in _context.Carts
                        where c.Id == CartId
                        select new CartDTO
                        {
                            Id = c.Id,
                            Items = (from cartItem in _context.CartItems.Include(x => x.ProductSkus)
                                     join product in _context.Products on cartItem.ProductId equals product.Id
                                     where cartItem.CartId == c.Id
                                     select new CartItemDTO
                                     {
                                         Id = cartItem.Id,
                                         ProductId = product.Id,
                                         ProductSkusId = cartItem.ProductSkus != null ? cartItem.ProductSkus.Id : (Guid?)null,
                                         ProductName = product.Name,
                                         VariantName = cartItem.ProductSkus != null ? cartItem.ProductSkus.Name : null,
                                         Price = product.Price,
                                         Quantity = cartItem.Quantity,
                                         Image = _context.Images
                                                 .Where(x => product.Id == x.ProductId)
                                                 .Select(x => x.ImageUrl)
                                                 .FirstOrDefault()
                                     }).ToList()
                        };

            var result = await query.FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}
