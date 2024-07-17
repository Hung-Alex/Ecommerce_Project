using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Application.DTOs.Internal;
using Domain.Entities.Users;
using Domain.Shared;
using Google.Apis.Auth;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Infrastructure.Services.GoogleAuthen
{
    public class GoogleAuthenService : IGoogleAuthenService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSetting _jwtSetting;
        private readonly GoogleSettings _googleSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public GoogleAuthenService(IConfiguration configuration, UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
            _jwtSetting = configuration.GetSection("JwtSetting").Get<JwtSetting>() ?? throw new NotImplementedException("configuration not found");
            _googleSettings = configuration.GetSection("Google").Get<GoogleSettings>() ?? throw new NotImplementedException("configuration not found"); ;
        }

        public async Task<Result<Guid>> SignInByGoogleAsync(string IdToken, CancellationToken cancellationToken = default)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(IdToken, new ValidationSettings
                {
                    Audience = new List<string>() { _googleSettings.ClientId }
                });

                var userApplication = await _userManager.FindByEmailAsync(payload.Email);
                if (userApplication != null)
                {
                    return Result<Guid>.ResultSuccess(userApplication.Id);
                }
                var repoUserDomain = _unitOfWork.GetRepository<User>;
               // var userDomain=
                //var newUserInfo=new ApplicationUser() { Email=payload.Email ,};
              //  await _userManager.CreateAsync();


                return Result<Guid>.ResultSuccess(Guid.NewGuid());
            }
            catch (InvalidJwtException e)
            {
                return Result<Guid>.ResultFailures();
            }
        }
    }
}
