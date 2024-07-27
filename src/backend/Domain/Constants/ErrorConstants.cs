using Domain.Shared;

namespace Domain.Constants
{
    public static class ErrorConstants
    {
        public static class ProductError
        {
            public static Error ProductNotFoundWithSlug(string slug) => new Error("Product Not Found", $"Not found product with Url slug  {slug}");

            public static readonly Error VariantsDontHaveVariant = new Error("VariantsDontHaveVariant", "Variants Dont Have Variant");
        }
        public static class BrandError
        {
        }
        public static class UserError
        {
            public static Error PasswordNotMatch => new Error("User.PasswordNotMatch", "Password Not Match");
            public static Error UserNotFoundWithName(string name) => new Error("User.UserNotFoundWithName", $"Not found with name {name}");
            public static Error UserNotFoundWithID(Guid userId) => new Error("User.NotFound", $"Not found user with id {userId}");
            public static Error EmailIsInvaild => new Error("User.EmailIsInvaild", "Email is invaild");
            public static Error PhoneNumberIsInvaild => new Error("User.PhoneNumberIsInvaild", "Phone number is invaild");
            public static Error InvalidUserName => new Error("User.InvalidUserName", "InvalidUserName");
            public static Error MaximumLengthOfInvalidPassword(int maxLength) => new Error("User.InvalidPassword", $"Required Password must have maximum length {maxLength}");
            public static Error MinimumLengthOfInvalidPassword(int minLength) => new Error("User.InvalidPassword", $"Required Password must have minimum length {minLength}");
        }
        public static class ApplicationUserError
        {
            public static Error UserNotFoundWithName(string name) => new Error("User.UserNotFoundWithName", $"Not found with name {name}");
            public static Error ProductNotFoundWithSlug(string slug) => new Error("Product Not Found", $"Not found product with Url slug  {slug}");
            public static Error UserNotFoundWithID(Guid userId) => new Error("User.NotFound", $"Not found user with id {userId}");
        }
        public static class RoleError
        {
            public static Error UserHaveBeenSameRole(string role) => new Error("User.HaveBeenSameRole", $"User have been same role {role}");
            public static Error DuplicateRole => new Error("Duplicate.DuplicatePermission", "Duplicate Permission");
            public static Error HaveAnyRoleDontBelongApplication => new Error("HaveAnyPermissionDontBelongApplication", "Have Any Permission Dont Belong Application");
        }
        public static class PermissionError
        {
            public static Error DuplicatePermission => new Error("Duplicate.DuplicatePermission", "Duplicate Permission");
            public static Error HaveAnyPermissionDontBelongApplication => new Error("HaveAnyPermissionDontBelongApplication", "Have Any Permission Dont Belong Application");
        }
        public static class AuthenticationError
        {
            public static readonly Error AuthUsernamePasswordInvalid = new Error("Authencation.UsernamePasswordInvalid", "Username and Password Invalid");
            public static readonly Error AuthAccessTokenInvalid = new Error("Authencation.AccessTokenInvalid", "AccessToken invalid");
            public static readonly Error AuthRefreshTokenDoesNotMatchOrExpired
                = new Error("Authencation.RefreshTokenDoesNotMatchOrExpired"
                    , "Refresh token doesn't match with refresh token is saved or it was expired, you must be login again");
        }
        public static class WishListError
        {
            public static Error ProductDontHaveInWishlistWithId(Guid id) => new Error("Product.DontHaveInWishlistWithId", $"Product Dont Have In Wishlist With Id {id}");
            //WishList
            public static Error WishListProductIsExistedInWishListWithId(Guid ProductId) => new Error("WishList.Product already Existed In Wishlist", $"Product already existed in wishlist with id {ProductId}");
        }
        public static class CartError
        {
            public static readonly Error CartNotFound = new Error("Cart.CartNotFound", "Cart Not Found");
        }
        public static class UploadImageError
        {
            public static Error UploadImageOccursErrorWithFileName(string fileName) => new Error("UploadImageOccursErrorWithFileName", $"Upload Image Occurs Error With FileName {fileName}");
        }
        public static class BannerError
        {
        }
        public static class SlideError
        {
        }
        public static class RattingError
        {
        }
        public static class PostError
        {
        }
        public static class CommentError
        {
        }
        public static class LoginError
        {
            public static Error LoginIsNotSuccess(string userName) => new Error("Login", $"Login isn't Success with UserName {userName}");
            public static Error LoginIsNotSuccessWithGoogle => new Error("Login", $"Login isn't Success with Google ");
        }
        //comom
        public static Error UrlSlugIsExisted(string url) => new Error("Url Slug Is Existed", $"Url slug is existed {url}");
        public static Error NotFound(string url) => new Error("NotFound", $"Url slug is existed {url}");
        public static Error NotFoundWithId(Guid id) => new Error("NotFound", $"Not Found With Id {id}");
        public static Error UrlSlugInvalid => new Error("Url Slug Invalid", "Url slug is invalid");
    }
}
