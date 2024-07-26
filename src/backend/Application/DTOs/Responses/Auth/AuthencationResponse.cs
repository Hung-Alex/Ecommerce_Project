using Application.DTOs.Internal.User;

namespace Application.DTOs.Responses.Auth
{
    public record AuthencationResponse
    {
        public record UserAuthentication(Guid userId,string Name,string? ImageUrl=null);
        public AuthencationResponse(string accessToken, string refreshToken, string typeToken, UserAuthentication user)
        {
            User = user;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TypeToken = typeToken;
        }
        public UserAuthentication User { get; set; }
        public string TypeToken { get; private set; }
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
    }
}
