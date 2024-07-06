using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Application.DTOs.Internal.Authen;
using Domain.Constants;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly JwtSetting _jwtSetting;
        public JwtProvider(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration ?? throw new NotImplementedException("configuration not found");
            _jwtSetting = configuration.GetSection("JwtSetting").Get<JwtSetting>() ?? throw new NotImplementedException("configuration not found"); ;
        }
        public async Task<string> GenerateTokenAsync(Guid userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var user = await _userManager.Users.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) { throw new NotFoundException(); }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimUser.ApplicationUserId,user.Id.ToString()),
                new Claim(ClaimUser.UserId, user.User.Id.ToString())
            };
            if (roles.Count() > 0)
            {
                claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            }
            // Tạo token
            var token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(_jwtSetting.ExpiredToken),
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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));
            var parameters = new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = _jwtSetting.Audience,
                ValidIssuer = _jwtSetting.Issuer,
                IssuerSigningKey = key
            };
            return parameters;
        }
    }
}
