using Application.Common.Implementation;
using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Application.DTOs.Internal;
using Application.Helper;
using Domain.Constants;
using Infrastructure.Persistence.Identity;
using Infrastructure.Persistence.Persistence.Data;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Auth.Authorization;
using Infrastructure.Services.CloudinaryUpload;
using Infrastructure.Services.GoogleAuthen;
using Infrastructure.Services.Identity;
using Infrastructure.Services.UserInHttpContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Extensions.Services
{
    internal static class DependencyInjectionServicesExtension
    {
        internal static IServiceCollection AddServicesExtension(this IServiceCollection services)
        {
            services.AddScoped<IMedia, Media>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleServivce, RoleService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IGoogleAuthenService, GoogleAuthenService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            return services;
        }
        internal static IServiceCollection AddIdentityExtension(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<StoreDbContext>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = AccountConstants.MinimumLengthPassword;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            });
            return services;
        }
        internal static IServiceCollection AddAuthencationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSetting").Get<JwtSetting>();
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.FromMinutes(jwtSettings.ExpiredToken)
                };
                options.SaveToken = true;
                options.Events = new JwtBearerEvents();
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Kiểm tra nếu token có trong cookie
                        if (context.Request.Cookies.ContainsKey(UserToken.AccessTokenCookies))
                        {
                            context.Token = context.Request.Cookies[UserToken.AccessTokenCookies];
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            return services;
        }
        internal static IServiceCollection AddCORSExtension(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAll",
                policies
                => policies
                .WithOrigins("http://localhost:3000", "https://192.168.0.100:3000", "https://e253-2405-4802-a1d4-4680-d156-a0ff-5d1e-1134.ngrok-free.app")
                .AllowCredentials()
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod()
                )
            );
            return services;
        }
        internal static IServiceCollection AddAuthorizationExtension(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
            // Register our custom Authorization handler
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            // Overrides the DefaultAuthorizationPolicyProvider with our own
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            return services;
        }
    }
}
