using Domain.Entities.Images;
using Domain.Common;

namespace Domain.Entities.Products
{
    public class ProductImages : BaseEntity, IDatedModification
    {
        public ProductImages() : base() { }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public Image Image { get; set; }
        public Guid ImageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

