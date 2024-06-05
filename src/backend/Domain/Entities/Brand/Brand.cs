
using Domain.Common;
using Domain.Shared;
using Domain.Entities.Products;

namespace Domain.Entities.Brands
{
    public class Brand : BaseEntity, IDatedModification,IAggregateRoot
    {
        private Brand() : base() { }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string LogoImageUrl { get; set; } //upload Base 64
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IList<Product> Products { get; set; }
    }
}
