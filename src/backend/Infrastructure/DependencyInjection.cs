using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Domain.Interface;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Repositories.GenericRepository;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.Auth;
using Infrastructure.Services.CloudinaryUpload;
using Infrastructure.Services.Identity;
using Infrastructure.Services.UserInHttpContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<ICurrentUserService, CurrentUserService>();


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
                    ClockSkew = TimeSpan.FromSeconds(30),
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
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                )
            );
            return services;
        }
    }
}
