using Domain.Common;
using Domain.Entities.Category;
using Domain.Entities.Products;
using Domain.Shared;

namespace Domain.Entities
{
    public class ProductSubCategory : BaseEntity, IAggregateRoot, IDatedModification
    {
        public Categories Category { get; set; }
        public Guid CategoryId { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
