using Domain.Common;
using Domain.Shared;

namespace Domain.Entities.Banner
{
    public class Banner : BaseEntity, IAggregateRoot, IDatedModification
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LogoImageUrl { get; set; }
        public bool? Left { get; set; }
        public bool? Right { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}