using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity, IDatedModification
    {
        private Category() : base() { }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public IList<Product> Products { get; set; }
        //mapping categoryvoucher map
        public IList<CategoryVouchers> CategoryVouchersMaps { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
