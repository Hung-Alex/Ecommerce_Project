using Domain.Common;
using Domain.Entities.Images;

namespace Domain.Entities.Slides
{
    public class SlidesImage : BaseEntity, IDatedModification
    {
        public SlidesImage(Guid slideId, Guid imageId)
        {
            SlideId = slideId;
            ImageId = imageId;
        }
        public Slide Slide { get; set; }
        public Guid SlideId { get; set; }
        public int OrderItem { get; set; }
        public Image Image { get; set; }
        public Guid ImageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
