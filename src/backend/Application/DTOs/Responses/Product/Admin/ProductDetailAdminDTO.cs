using Application.DTOs.Responses.Images;

namespace Application.DTOs.Responses.Product.Admin
{
    public record ProductDetailAdminDTO:BaseDTO
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public Decimal Price { get; init; }
        public Decimal? OldPrice { get; set; }
        public string UrlSlug { get; init; }
        public int? Discount { get; init; }
        public Guid? BrandId { get; init; }
        public Guid? CategoryId { get; init; }
        public bool IsStock { get; set; }
        public virtual IEnumerable<ImageDTO> Images { get; init; } 
    }
}
