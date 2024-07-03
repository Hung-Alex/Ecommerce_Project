using Domain.Common;
using Domain.Entities.Users;
using Domain.Shared;

namespace Domain.Entities.Slides
{
    public class Slide : BaseEntity, IDatedModification, IAggregateRoot, ICreatedAndUpdatedBy
    {
        public Slide(string title, string description, bool? status, int order)
        {
            Title = title ?? throw new ArgumentNullException(); ;
            Description = description ?? throw new ArgumentNullException();
            Status = status ?? throw new ArgumentNullException();
            Order = order;
        }
        private Slide() : base() { }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool? Status { get; set; }
        public virtual ICollection<SlidesImage> SlidesImages { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
