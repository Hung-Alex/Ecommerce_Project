using Application.Common.Exceptions;
using Application.Common.Interface.IdentityService;
using Application.DTOs.Internal.User;
using Azure.Core;
using Domain.Constants;
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
        public async Task<bool> AssignmentRoleForUserAsync(Guid userId, string role, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new NotFoundException($"{ErrorConstants.UserNotFoundWithID}{userId}");
            }
            var isExixted = await _userManager.IsInRoleAsync(user, role);
            if (isExixted)
            {
                throw new ConflictException(ErrorConstants.UserHaveBeenSameRole(role).Description);
            }
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }
        public async Task<Guid> CreateUserAsync(string email, string password, string userName, CancellationToken cancellationToken = default)
        {
            var user = new ApplicationUser() { UserName = userName, Email = email };
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
            if (user is null) throw new NotFoundException(ErrorConstants.UserNotFoundWithID(userId).Description);
            var result = await _userManager.RemoveAuthenticationTokenAsync(user, provider, tokenName);
            return result.Succeeded;
        }
        public async Task<string> GetRefreshTokenAsync(Guid userId, string provider, string tokenName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) throw new NotFoundException(ErrorConstants.UserNotFoundWithID(userId).Description);
            string token = await _userManager.GetAuthenticationTokenAsync(user, provider, tokenName);
            return token;
        }
        public async Task<UserDTO> GetUserAsync(string userName, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null) throw new NotFoundException(ErrorConstants.UserNotFoundWithName + userName);
            var roles = await _userManager.GetRolesAsync(user);
            return new UserDTO()
            {
                Id = user.Id
            ,
                Name = user.UserName
            ,
                Email = user.Email
            ,
                Role = roles
            };
        }
        public async Task<UserDTO> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user is null) return null;
            var roles = await _userManager.GetRolesAsync(user);
            return new UserDTO()
            {
                Id = user.Id
            ,
                Name = user.UserName
            ,
                Email = user.Email
            ,
                Role = roles
            };
        }
        public async Task<bool> SaveRefreshTokenAsync(Guid userId, string provider, string tokenName, string value)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) throw new NotFoundException(ErrorConstants.UserNotFoundWithID(userId).Description);
            var result = await _userManager.SetAuthenticationTokenAsync(user, provider, tokenName, value);
            return result.Succeeded;
        }
        public async Task<bool> SignInAsync(string userName, string password, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null) return false;
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded;
        }
    }
}
