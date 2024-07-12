using Application.Common.Interface;
using Application.DTOs.Responses.Cart;
using Domain.Entities.Carts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly StoreDbContext _context;
        public CartService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<CartDTO> GetCartAsync(Guid CartId, CancellationToken cancellationToken = default)
        {
            var query = from c in _context.Carts
                        where c.Id == CartId
                        select new CartDTO
                        {
                            Id = c.Id,
                            Items = (from cartItem in _context.CartItems
                                     join product in _context.Products on cartItem.ProductId equals product.Id
                                     join variant in _context.ProductSkus on cartItem.ProductSkusId equals variant.Id into variants
                                     from variant in variants.DefaultIfEmpty()
                                     where cartItem.CartId == c.Id
                                     select new CartItemDTO
                                     {
                                         Id = cartItem.Id,
                                         ProductId = product.Id,
                                         ProductSkusId = variant != null ? variant.Id : (Guid?)null,
                                         ProductName = product.Name,
                                         VariantName = variant != null ? variant.Name : null,
                                         Price = variant != null ? variant.Price : product.Price,
                                         Quantity = cartItem.Quantity,
                                         Image = _context.ProductImages
                                                 .Include(x => x.Image)
                                                 .Where(x => product.Id == x.ProductId)
                                                 .Select(x => x.Image.ImageUrl)
                                                 .FirstOrDefault()
                                     }).ToList()
                        };

            var result = await query.FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}
