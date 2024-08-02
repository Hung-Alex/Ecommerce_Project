using Application.DTOs.Internal.User;

namespace Application.DTOs.Responses.Auth
{
    public record AuthencationResponse
    {
        public UserDTO User { get; init; }
        public AuthencationResponse(string accessToken, string refreshToken, string typeToken, UserDTO user)
        {
            User = user;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TypeToken = typeToken;
        }
        public string TypeToken { get; private set; }
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
    }
}
