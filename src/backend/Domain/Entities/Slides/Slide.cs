using Domain.Common;
using Domain.Entities.Users;
using Domain.Shared;

namespace Domain.Entities.Slides
{
    public class Slide : BaseEntity, IDatedModification, IAggregateRoot, ICreatedAndUpdatedBy
    {
        public Slide(string title, string description, bool isActive)
        {
            Title = title ?? throw new ArgumentNullException(); ;
            Description = description ?? throw new ArgumentNullException();
            IsActive = isActive;
        }
        public Slide() : base() { }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}
