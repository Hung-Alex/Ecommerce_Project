using System.Security.Claims;

namespace Application.DTOs.Responses.Auth
{
    public record AuthencationResponse
    {
        public AuthencationResponse(string accessToken, string refreshToken, string typeToken, Guid userId)
        {
            UserId = userId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TypeToken = typeToken;
        }
        public Guid UserId { get; set; }
        public string TypeToken { get; private set; }
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
    }
}
