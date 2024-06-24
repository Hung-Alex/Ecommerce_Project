using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal.Authen;
using Azure.Core;
using Domain.Constants;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services.Auth
{
    public class JwtProvider : IJwtProvider
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public JwtProvider(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration ?? throw new NotImplementedException("configuration not found");
        }
        public async Task<string> GenerateTokenAsync(Guid userId)
        {
            var jwtSettings = _configuration.GetSection("JwtSetting");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expiredTokenMinutes = jwtSettings.GetValue<int>("ExpiredToken");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) { throw new NotFoundException(); }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            };
            if (roles.Count() > 0)
            {
                claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            }
            // Tạo token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(expiredTokenMinutes),
                signingCredentials: signingCredentials);

            var encode = new JwtSecurityTokenHandler().WriteToken(token);
            return encode;
        }

        public async Task<IEnumerable<Claim>> GetClaimsFromTokenAsync(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameter = GetValidationParameter();
            SecurityToken validateToken;
            IPrincipal principals = tokenHandler.ValidateToken(token, validationParameter, out validateToken);
            var jwt = (JwtSecurityToken)validateToken;
            var securityToken = tokenHandler.ReadJwtToken(token);
            return securityToken.Claims.ToList();
        }

        public async Task<bool> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                return false;
            }
            var getRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, UserToken.Provider, UserToken.RefreshToken);
            var userToken = JsonSerializer.Deserialize<RefreshToken>(getRefreshToken);
            if (getRefreshToken is null || !(refreshToken == userToken.Token && userToken.ExpriedTime >= DateTime.Now))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameter = GetValidationParameter();
                SecurityToken validateToken;
                IPrincipal principals = tokenHandler.ValidateToken(token, validationParameter, out validateToken);
                var jwt = (JwtSecurityToken)validateToken;
                return true;
            }
            catch (SecurityTokenValidationException ex)
            {
                return false;
            }
        }
        private TokenValidationParameters GetValidationParameter()
        {
            var jwtSettings = _configuration.GetSection("JwtSetting");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expiredTokenMinutes = jwtSettings.GetValue<int>("ExpiredToken");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var parameters = new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidIssuer = issuer,
                IssuerSigningKey = key
            };
            return parameters;
        }
    }
}
