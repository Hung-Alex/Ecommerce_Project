using Application.DTOs.Internal.User;
using Domain.Shared;


namespace Application.Common.Interface
{
    public interface ICurrentUserService
    {
        Result<CurrentUser> GetCurrentUser();
    }
}
