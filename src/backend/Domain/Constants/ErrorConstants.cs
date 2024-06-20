using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public static class ErrorConstants
    {
        public const string UrlSlugIsExisted = "Url slug is existed ";
        public const string NotFound = "Not found with id ";
        //Users
        public const string UserNotFoundWithID = "Not found user with id ";
        public const string UserNotFoundWithName = "Not found user with name ";
        public const string UserExistedWithName = "User existed  with name ";
        public const string UserHaveBeenSameRole = "User've been same role";
        //authencation
        public const string AuthUsernamePasswordInvalid = "Username or password is invalid";
        public const string AuthAccessTokenInvalid = "AccessToken is invalid";
        public const string AuthRefreshTokenDoesNotMatchOrExpired = "Refresh token doesn't match with refresh token is saved or it was expired, you must be login again";
    }
}
