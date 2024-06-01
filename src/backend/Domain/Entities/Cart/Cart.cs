using Domain.Common;
using Domain.Exceptions;
using Domain.Shared;


namespace Domain.Entities
{
    public class Cart : BaseEntity, IDatedModification, IAggregateRoot
    {
        public Cart() : base() { }
        public required ICollection<CartItem> CartItems { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CartItem CreateCartItem(Guid productId, Guid productSkusId, int quantity)
        {
            return new CartItem(Id, productId, productSkusId, quantity);
        }
        public void AddItems(CartItem cartItem)
        {
            if (cartItem == null) throw new ArgumentNullException();
            var existItem = CartItems.Where(x => x.Equals(cartItem)).FirstOrDefault();
            if (existItem != null)
            {
                if (cartItem.Quantity <= 0)
                {
                    CartItems.Remove(existItem);
                }
                else
                {
                    existItem.Quantity = cartItem.Quantity;
                }
            }
            else
            {
                CartItems.Add(cartItem);
            }
        }
        public void RemoveItem(Guid cartItemId)
        {
            if (CartItems is null || !CartItems.Any()) throw new CartNullException();
            var cartItem = CartItems.FirstOrDefault(c => c.CartId == cartItemId);
            if (cartItem is null) throw new CartItemNotFoundException();
            CartItems.Remove(cartItem);
        }
        public void UpdateQuantityItem(Guid cartItemId, int quantity)
        {
            if (CartItems is null || !CartItems.Any()) throw new CartNullException();
            var cartItem = CartItems.FirstOrDefault(c => c.CartId == cartItemId);
            if (cartItem is null) throw new CartItemNotFoundException();
            if (quantity <= 0)
            {
                CartItems.Remove(cartItem);
                return;
            }
            cartItem.Quantity = quantity;
        }
    }
}
