namespace Application.DTOs.Responses.Auth
{
    public record AuthencationResponse
    {
        public AuthencationResponse(string accessToken, string refreshToken, string typeToken, DateTime refresh, DateTime access)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TypeToken = typeToken;
            ExpiredTimeAccessToken = access;
            ExpiredTimeRefreshToken = access;
        }
        public string TypeToken { get; private set; }
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime ExpiredTimeAccessToken { get; private set; }
        public DateTime ExpiredTimeRefreshToken { get; private set; }
    }
}
