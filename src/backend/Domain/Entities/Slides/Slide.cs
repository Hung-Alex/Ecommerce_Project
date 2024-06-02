using Domain.Common;
using Domain.Shared;

namespace Domain.Entities.Slides
{
    public class Slide: BaseEntity, IDatedModification, IAggregateRoot
    {
        private Slide() : base() { }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; } //hide or show banner at homepage
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
