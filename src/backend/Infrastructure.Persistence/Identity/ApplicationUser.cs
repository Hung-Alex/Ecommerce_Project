using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
    }
}
