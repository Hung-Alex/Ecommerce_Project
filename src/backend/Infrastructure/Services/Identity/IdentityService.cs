using Application.Common.Exceptions;
using Application.Common.Interface.IdentityService;
using Application.DTOs.Internal.User;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Guid> CreateUserAsync(string email, string password, string userName, CancellationToken cancellationToken = default)
        {
            var user = new ApplicationUser() { UserName = userName, Email = email, City = "dalat" };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.GroupBy(x => x.Code, x => x.Description)
                .ToDictionary(group => group.Key, group => group.ToArray()));
            }
            return user.Id;
        }

        public async Task<bool> DeleteRefreshTokenAsync(Guid userId, string provider, string tokenName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) throw new NotFoundException();
            var result = await _userManager.RemoveAuthenticationTokenAsync(user, provider, tokenName);
            return result.Succeeded;
        }

        public async Task<string> GetRefreshTokenAsync(Guid userId, string provider, string tokenName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) throw new NotFoundException();
            string token = await _userManager.GetAuthenticationTokenAsync(user, provider, tokenName);
            return token;
        }

        public async Task<UserDTO> GetUserAsync(string userName, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null) throw new NotFoundException();
            var roles = await _userManager.GetRolesAsync(user);
            return new UserDTO()
            {
                Id = user.Id
            ,
                Name = user.UserName
            ,
                Email = user.Email
            ,
                City = user.City
            ,
                PostalCode = user.PostalCode
            ,
                Country = user.Country
            ,
                ImageUrl = user.ImageUrl
            ,
                Role = roles
            };
        }
        public async Task<UserDTO> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user is null) throw new NotFoundException();
            var roles = await _userManager.GetRolesAsync(user);
            return new UserDTO()
            {
                Id = user.Id
            ,
                Name = user.UserName
            ,
                Email = user.Email
            ,
                City = user.City
            ,
                PostalCode = user.PostalCode
            ,
                Country = user.Country
            ,
                ImageUrl = user.ImageUrl
            ,
                Role = roles
            };
        }

        public async Task<bool> SaveRefreshTokenAsync(Guid userId, string provider, string tokenName, string value)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) throw new NotFoundException();
            var result = await _userManager.SetAuthenticationTokenAsync(user, provider, tokenName, value);
            return result.Succeeded;
        }

        public async Task<bool> SignInAsync(string userName, string password, CancellationToken cancellationToken = default)
        {
            var user=await _userManager.FindByNameAsync(userName);
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded;
        }
    }
}
