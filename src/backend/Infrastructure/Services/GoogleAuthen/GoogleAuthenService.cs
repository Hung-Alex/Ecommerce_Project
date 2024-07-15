using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Application.DTOs.Internal;
using Domain.Entities.Users;
using Domain.Shared;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.GoogleAuthen
{
    public class GoogleAuthenService : IGoogleAuthenService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSetting _jwtSetting;
        private readonly GoogleSettings _googleSettings;
        private readonly UserManager<User> _userManager;
        public GoogleAuthenService(IConfiguration configuration, IConfigurationSection jwtSettings, IConfigurationSection goolgeSettings, UserManager<User> userManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtSetting = configuration.GetSection("JwtSetting").Get<JwtSetting>() ?? throw new NotImplementedException("configuration not found");
            _googleSettings = configuration.GetSection("Google").Get<GoogleSettings>() ?? throw new NotImplementedException("configuration not found"); ;
        }

        public async Task<Result<Guid>> SignInByGoogleAsync(string IdToken, CancellationToken cancellationToken = default)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(IdToken);
            return null;
        }
    }
}
