using Domain.Shared;
using System.Diagnostics;

namespace Domain.Constants
{
    public static class ErrorConstants
    {

        public static Error UrlSlugIsExisted(string url) => new Error("Url Slug Is Existed", $"Url slug is existed {url}");
        public static Error NotFound(string url) => new Error("NotFound", $"Url slug is existed {url}");
        public static Error NotFoundWithId(Guid id) => new Error("NotFound", $"Not Found With Id {id}");

        //Users
        public static Error UserNotFoundWithID(Guid userId) => new Error("User.NotFound", $"Not found user with id {userId}");
        public static Error UserNotFoundWithName(string name) => new Error("User.UserNotFoundWithName", $"Not found with name {name}");
        public static Error UserExistedWithName(string name) => new Error("User.ExistedWithName", $"User existed with name {name}");
        public static Error UserHaveBeenSameRole(string role) => new Error("User.HaveBeenSameRole", $"User have been same role {role}");
        //authencation
        public static readonly Error AuthUsernamePasswordInvalid = new Error("Authencation.UsernamePasswordInvalid", "Username and Password Invalid");
        public static readonly Error AuthAccessTokenInvalid = new Error("Authencation.AccessTokenInvalid", "AccessToken invalid");
        public static readonly Error AuthRefreshTokenDoesNotMatchOrExpired
            = new Error("Authencation.RefreshTokenDoesNotMatchOrExpired"
                , "Refresh token doesn't match with refresh token is saved or it was expired, you must be login again");
        //ProductImage
        public static Error ProductImageDontHaveImageWithId(Guid id) => new Error("ProductImage.DontHaveImageWithId", $"Product dont have image with id {id}");
        //WishList
        public static Error WishListDontHaveProductInWishListWithId(Guid ProductId) => new Error("WishList.Not Found Producy In Wishlist", $"Dont have product in wishlist with id {ProductId}");
        public static Error WishListProductIsExistedInWishListWithId(Guid ProductId) => new Error("WishList.Product already Existed In Wishlist", $"Product already existed in wishlist with id {ProductId}");
        public static Error UploadImageOccursErrorWithFileName(string fileName) => new Error("UploadImageOccursErrorWithFileName", $"Upload Image Occurs Error With FileName {fileName}");
    }
}
