using Domain.Common;
using Domain.Entities.Products;
using Domain.Entities.SubCategories;
using Domain.Shared;

namespace Domain.Entities
{
    public class ProductSubCategory : BaseEntity, IAggregateRoot, IDatedModification
    {
        public SubCategory SubCategory { get; set; }
        public Guid SubCategoryId { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
