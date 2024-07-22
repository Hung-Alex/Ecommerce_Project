using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Domain.Interface;
using Google;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Repositories.GenericRepository;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Carts;
using Infrastructure.Services.CloudinaryUpload;
using Infrastructure.Services.GoogleAuthen;
using Infrastructure.Services.Identity;
using Infrastructure.Services.Section;
using Infrastructure.Services.UserInHttpContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("EcommerceDB")));
            // Register UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //register Identity service
            // Register Repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            // Register Services
            services.AddScoped<IMedia, Media>();
            services.AddScoped<IPermissionService, PermissionService>();
           
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IGoogleAuthenService, GoogleAuthenService>();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<StoreDbContext>()                
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            var jwtSettings = configuration.GetSection("JwtSetting");
            services.AddHttpContextAccessor();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtProvider, JwtProvider>();
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
                    ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                    ValidAudience = jwtSettings.GetValue<string>("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("SecretKey"))),
                    ClockSkew = TimeSpan.FromMinutes(int.Parse(jwtSettings.GetValue<string>("ExpiredToken"))),
                };
                options.SaveToken = true;
                options.Events = new JwtBearerEvents();
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                        {
                            // Kiểm tra nếu token có trong cookie
                            if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                            {
                                context.Token = context.Request.Cookies["X-Access-Token"];
                            }
                            return Task.CompletedTask;
                        }
                };
            });
            //set cors
            services.AddCors(options => options.AddPolicy("AllowAll",
                policies
                => policies
                .WithOrigins("http://localhost:3000")
                .AllowCredentials()
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod()
                )
            );
            services.AddAuthorization(options =>
            {
                // One static policy - All users must be authenticated
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
