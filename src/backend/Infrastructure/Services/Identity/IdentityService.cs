using Application.Common.Exceptions;
using Application.Common.Interface.IdentityService;
using Application.DTOs.Internal.User;
using Application.DTOs.Responses.ApplicationUsers;
using Application.DTOs.Responses.Role;
using Domain.Constants;
using Domain.Shared;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<bool> AssignmentRoleForUserAsync(Guid userId, string role, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new NotFoundException($"{ErrorConstants.ApplicationUserError.UserNotFoundWithID}{userId}");
            }
            var isExixted = await _userManager.IsInRoleAsync(user, role);
            if (isExixted)
            {
                throw new ConflictException(ErrorConstants.RoleError.UserHaveBeenSameRole(role).Description);
            }
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }
        public async Task<Guid> CreateUserAsync(string email, string password, string userName, Guid UserDomainId, CancellationToken cancellationToken = default)
        {
            var user = new ApplicationUser() { UserName = userName, Email = email, UserId = UserDomainId };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.GroupBy(x => x.Code, x => x.Description)
                .ToDictionary(group => group.Key, group => group.ToArray()));
            }
            return user.Id;
        }

        public async Task<Result<Guid>> CreateUserAsync(string email, string password, string userName, Guid UserDomainId, bool lockAccount, CancellationToken cancellationToken = default)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                UserId = UserDomainId,
                LockoutEnabled = true
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return Result<Guid>.ResultFailures(result.Errors.Select(x => new Error(x.Code, x.Description)));
            }
            if (lockAccount)
            {
                user.LockoutEnd = DateTimeOffset.MaxValue;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    return Result<Guid>.ResultFailures(updateResult.Errors.Select(x => new Error(x.Code, x.Description)));
                }
            }
            return Result<Guid>.ResultSuccess(user.Id);
        }

        public async Task<bool> DeleteRefreshTokenAsync(Guid userId, string provider, string tokenName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) throw new NotFoundException(ErrorConstants.ApplicationUserError.UserNotFoundWithID(userId).Description);
            var result = await _userManager.RemoveAuthenticationTokenAsync(user, provider, tokenName);
            return result.Succeeded;
        }

        public async Task<ApplicationUserDTO> GetApplicationUserByUserIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();
            if (user is null) return null;
            var rolesName = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.Where(x => rolesName.Contains(x.Name)).ToListAsync();
            return new ApplicationUserDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                UserId = user.UserId,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsLockout = user.LockoutEnabled,
                Roles = roles?.Select(x => new RoleDTO()
                {
                    Id = x.Id,
                    Name = x.Name
                })
            };
        }

        public async Task<string> GetRefreshTokenAsync(Guid userId, string provider, string tokenName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) throw new NotFoundException(ErrorConstants.ApplicationUserError.UserNotFoundWithID(userId).Description);
            string token = await _userManager.GetAuthenticationTokenAsync(user, provider, tokenName);
            return token;
        }
        public async Task<UserDTO> GetUserAsync(string userName, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(userName);
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

        public async Task<Result<bool>> LockAccountAsync(Guid userId, bool isLock, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
            if (user is null)
                return Result<bool>.ResultFailures(ErrorConstants.ApplicationUserError.UserNotFoundWithID(userId));

            if (isLock)
            {
                // lock account
                user.LockoutEnd = DateTimeOffset.MaxValue;
            }
            else
            {
                // Unlock account
                user.LockoutEnd = null;
            }
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return Result<bool>.ResultFailures(result.Errors.Select(x => new Error(x.Code, x.Description)));
            }
            return Result<bool>.ResultSuccess(true);
        }

        public async Task<bool> SaveRefreshTokenAsync(Guid userId, string provider, string tokenName, string value)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) throw new NotFoundException(ErrorConstants.ApplicationUserError.UserNotFoundWithID(userId).Description);
            var result = await _userManager.SetAuthenticationTokenAsync(user, provider, tokenName, value);
            return result.Succeeded;
        }
        public async Task<bool> SignInAsync(string userName, string password, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                return false; // Người dùng không tồn tại
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                return false; // Tài khoản đã bị khóa
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return true; // Đăng nhập thành công
            }

            return false; // Đăng nhập thất bại
        }

        public async Task<Result<Guid>> UpdateUserByUserIdAsync(Guid userId, string phoneNumber, bool isLock, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.Users.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if (user is null) return Result<Guid>.ResultFailures(ErrorConstants.ApplicationUserError.UserNotFoundWithID(userId));
            if (isLock)
            {
                // lock account
                user.LockoutEnd = DateTimeOffset.MaxValue;
            }
            else
            {
                // Unlock account
                user.LockoutEnd = null;
            }
            user.PhoneNumber = phoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return Result<Guid>.ResultFailures(result.Errors.Select(x => new Error(x.Code, x.Description)));
            }
            return Result<Guid>.ResultSuccess(user.Id);
        }
    }
}
