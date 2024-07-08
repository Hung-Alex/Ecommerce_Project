namespace Application.DTOs.Responses.Product
{
    public record ProductDTO : BaseDTO
    {
        public string Name { get; set; }
        public string UnitPrice { get; set; }
        public int? Discount { get; set; }
        public Decimal Price { get; set; }
        public string UrlSlug{get; set;}    
        public IEnumerable<string> Images { get; set; }
    }
}
