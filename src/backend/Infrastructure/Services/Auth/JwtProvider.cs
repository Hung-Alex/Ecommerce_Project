﻿using Application.Common.Exceptions;
using Application.Common.Interface;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Auth
{
    public class JwtProvider : IJwtProvider
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public JwtProvider(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration ?? throw new NotImplementedException("jwt configuration not found");
        }
        public async Task<string> GenerateTokenAsync(Guid userId)
        {
            // Lấy JwtSetting từ cấu hình
            var jwtSettings = _configuration.GetSection("JwtSetting");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expiredTokenMinutes = jwtSettings.GetValue<int>("ExpiredToken");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            // Tạo signing credentials
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
                expires: DateTime.Now.AddMinutes(expiredTokenMinutes),
                signingCredentials: signingCredentials);

            var encode = new JwtSecurityTokenHandler().WriteToken(token);
            return encode;
        }
    }
}