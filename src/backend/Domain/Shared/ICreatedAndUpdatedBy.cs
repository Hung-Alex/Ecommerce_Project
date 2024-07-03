using Domain.Entities.Users;

namespace Domain.Shared
{
    public interface ICreatedAndUpdatedBy
    {
        public Guid UserId { get; set; }
        public User User { get; set; } 
    }
}
