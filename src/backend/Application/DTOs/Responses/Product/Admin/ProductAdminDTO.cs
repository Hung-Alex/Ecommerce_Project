namespace Application.DTOs.Responses.Product.Admin
{
    public record ProductAdminDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlSlug { get; set; }
        public int? Discount { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
