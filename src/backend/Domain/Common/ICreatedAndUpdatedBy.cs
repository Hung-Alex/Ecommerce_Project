using Domain.Entities.Users;

namespace Domain.Common
{
    public interface ICreatedAndUpdatedBy
    {
        public Guid? CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public User UpdatedByUser { get; set; }
    }
}
