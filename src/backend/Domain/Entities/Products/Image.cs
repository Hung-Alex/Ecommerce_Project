using Domain.Common;
using Domain.Entities.Products;
using Domain.Entities.Slides;
using Domain.Shared;


namespace Domain.Entities.Images
{
    public class Image : BaseEntity, IDatedModification, IAggregateRoot
    {
        public Image() : base() { }
        public string ImageUrl { get; set; }
        public string ImageExtension { get; set; }
        public string PublicId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public Slide Slide { get; set; }
        public Guid SlideId { get; set; }
    }
}
