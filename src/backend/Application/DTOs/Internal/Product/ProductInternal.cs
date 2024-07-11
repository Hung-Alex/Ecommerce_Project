
namespace Application.DTOs.Internal.Product
{
    public record ProductInternal : BaseDTO
    {
        public string Name { get; set; }
        public string UnitPrice { get; set; }
        public int? Discount { get; set; }
        public Decimal Price { get; set; }
        public string UrlSlug { get; set; }
        public Guid BrandId { get; init; }
        public IEnumerable<string> Images { get; set; }
        public IEnumerable<Guid> CollectionId { get; init; }
    }
}
