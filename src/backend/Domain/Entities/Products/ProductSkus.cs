using Domain.Common;
using Domain.Entities.Carts;
using Domain.Entities.Orders;

namespace Domain.Entities.Products
{
    public class ProductSkus : BaseEntity, IDatedModification, ISoftDelete
    {
        public ProductSkus() : base() { }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
