using Application.Common.Interface;
using Application.DTOs.Internal.User;
using Domain.Constants;
using Domain.Shared;
using Microsoft.AspNetCore.Http;


namespace Infrastructure.Services.UserInHttpContext
{
    public class CurrentUserService : ICurrentUserService
    {

        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public Result<CurrentUser> GetCurrentUser()
        {
            var claim = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            var userId = claim.Value;
            var user = new CurrentUser() { Id = Guid.Parse(userId) };
            return Result<CurrentUser>.ResultSuccess(user);
        }
    }
}
