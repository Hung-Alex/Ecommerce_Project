namespace Application.DTOs.Internal
{
    public record JwtSetting(string SecretKey, string Issuer, string Audience, int ExpiredToken, int ExpiredRefreshToken);
}
