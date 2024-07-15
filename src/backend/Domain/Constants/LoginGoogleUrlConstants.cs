namespace Domain.Constants
{
    public static class LoginGoogleUrlConstants
    {
        public static string GetUrl(string ClientId, string scope, string RedirectUrl, string state) => $"https://accounts.google.com/o/oauth2/v2/auth?client_id={ClientId}&response_type=code&scope={scope}&redirect_uri={RedirectUrl}&state={state}";
    }
}
