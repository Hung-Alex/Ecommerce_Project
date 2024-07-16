using Application.DTOs.Responses.Product.Shared.Variants;
using Domain.Entities.Brands;
using Domain.Entities.Carts;
using Domain.Entities.Category;
using Domain.Entities.Products;
using Domain.Entities.Rattings;
using Domain.Entities.Users;
using Domain.Entities.WishLists;

namespace Application.DTOs.Responses.Product.Admin
{
    public record ProductDetailAdminDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public string UrlSlug { get; set; }
        public int? Discount { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public virtual Categories Category { get; set; }
        public Guid CategoryId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        //public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<VariantsDTO> Variants { get; set; } 
       
       
    }
}
