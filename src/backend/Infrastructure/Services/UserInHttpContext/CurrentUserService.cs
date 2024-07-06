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
            CurrentUser result = null;
            if (_contextAccessor.HttpContext.User is null)
            {
                return Result<CurrentUser>.ResultSuccess(null);
            }
            var claim = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimUser.UserId);
            if (claim is null)
            {
                return Result<CurrentUser>.ResultSuccess(null);
            }
            var user = new CurrentUser() { Id = Guid.Parse(claim.Value) };
            return Result<CurrentUser>.ResultSuccess(user);
        }
    }
}
