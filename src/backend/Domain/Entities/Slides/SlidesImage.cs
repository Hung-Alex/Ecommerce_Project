using Domain.Common;
using Domain.Entities.Images;

namespace Domain.Entities.Slides
{
    public class SlidesImage : BaseEntity, IDatedModification
    {
        public SlidesImage() : base() { }
        public SlidesImage(Guid slideId, Guid imageId) : base()
        {
            SlideId = slideId;
            ImageId = imageId;
        }
        public Slide Slide { get; set; }
        public Guid SlideId { get; set; }
        public int OrderItem { get; set; }
        public Image Image { get; set; }
        public Guid ImageId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
