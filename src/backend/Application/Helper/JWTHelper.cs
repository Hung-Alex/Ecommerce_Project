using Application.DTOs.Internal.Authen;
using Application.DTOs.Internal.User;
using System.Text.Json;

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

        public static DateTime GetExpiresAccessToken(double time)
        {
            return DateTime.Now.AddMinutes(time);
        }
        public static DateTime GetExpiresRefreshToken(double time)
        {
            return DateTime.Now.AddDays(time);
        }
    }
}
