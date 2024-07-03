using Domain.Common;
using Domain.Entities.Users;
using Domain.Shared;

namespace Domain.Entities.Banners
{
    public class Banner : BaseEntity, IAggregateRoot, IDatedModification, ICreatedAndUpdatedBy
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LogoImageUrl { get; set; }
        public bool? Left { get; set; }
        public bool? Right { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}