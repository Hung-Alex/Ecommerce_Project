using Application.DTOs.Internal.Authen;

namespace Application.Helper
{
    public static class JWTHelper
    {
       public static RefreshToken GenerateRefreshToken(DateTime exprired)
        {
            var random = new Random();
            string refreshToken = Guid.NewGuid().ToString() + random.Next((10 * random.Next((10 + 1) / 1)));
            return new RefreshToken()
            {
                Token = refreshToken,
                ExpriedTime = exprired,
            };
        }
    }
}
