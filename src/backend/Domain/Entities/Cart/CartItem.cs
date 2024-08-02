using Domain.Common;
using Domain.Entities.Products;


namespace Domain.Entities.Carts
{
    public class CartItem : BaseEntity, IDatedModification
    {
        public CartItem() : base() { }
        public CartItem(Guid cartId, Guid productId, int quantity) : base()
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            var other = obj as CartItem;
            return CartId == other.CartId
                && ProductId == other.ProductId;
        }
    }
}
