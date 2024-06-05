using Domain.Common;
using Domain.Entities.Images;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Image Image { get; set; }
        public Guid ImageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
