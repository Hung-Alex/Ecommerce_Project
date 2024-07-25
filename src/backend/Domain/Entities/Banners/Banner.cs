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
        public string PublicIdImage { get; set; }//base 64
        public bool IsVisible { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}