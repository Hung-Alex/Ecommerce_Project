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
            var user = new ApplicationUser() { UserName = userName, Email = email ,City="dalat"};
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return user.Id;
        }

        public async Task<UserDTO> GetUserAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return null;
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
            };
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user is null) return null;
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
            };
        }
    }
}
